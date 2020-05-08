using UnityEngine;
using UnityEngine.UI;

using GameDevJam.Core.Variables;

namespace GameDevJam.Interactable
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] float maxTimeToManip = 1000f;
        [SerializeField] IntVariable maxCystals = null;
        [SerializeField] IntVariable crystalsOwned = null;

        [SerializeField] public float stopingTolerance = 50f;

        [SerializeField] Slider activeBarFill = null;
        [SerializeField] public MovingSlider movingSlider = null;

        [SerializeField] Transform phaseStamps = null;
        [SerializeField] GameObject phaseStampPref = null;

        TimePhases objectsTimePhases = null;

        float currenTimeToManip; 

        private void Awake() 
        {
            currenTimeToManip = (maxTimeToManip / maxCystals.GetValue()) * crystalsOwned.GetValue();
        }

        private void Start() 
        {
            activeBarFill.maxValue = maxTimeToManip;
            activeBarFill.value = currenTimeToManip;

            movingSlider.GetComponent<Slider>().maxValue = currenTimeToManip;

            SpawnPhaseStamps();
        }

        private void SpawnPhaseStamps()
        {
            DestoryChildrenIn(phaseStamps);
            var phases = objectsTimePhases.GetPhases();

            foreach(var phase in phases)
            {
                SpawnPhaseStamp(phase.timeStamp);
            }
        }

        private void SpawnPhaseStamp(float placement)
        {
            GameObject phaseStamp = Instantiate(phaseStampPref, transform.position, Quaternion.identity, phaseStamps);
            Slider phaseStampSlider = phaseStamp.GetComponent<Slider>();
            phaseStampSlider.maxValue = maxTimeToManip;
            phaseStampSlider.value = placement;

            float tolerance = Mathf.Abs(placement - movingSlider.GetStartingPos());
            if (placement > currenTimeToManip)
            {
                ColorBlock newColorBlock = phaseStampSlider.colors;
                newColorBlock.disabledColor = phaseStampSlider.colors.normalColor;
                phaseStampSlider.colors = newColorBlock;
            }
            else if (tolerance <= 1f)
            {
                ColorBlock newColorBlock = phaseStampSlider.colors;
                newColorBlock.disabledColor = phaseStampSlider.colors.highlightedColor;
                phaseStampSlider.colors = newColorBlock;
            }
        }

        private void DestoryChildrenIn(Transform parent)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child == parent) { continue; }
                Destroy(child.gameObject);
            }
        }

        public void SetTimePhases(TimePhases timePhases)
        {
            objectsTimePhases = timePhases;
        }

        public void SetSliderStartingPos(float position)
        {
            movingSlider.SetStartingPos(position);
        }

        public void StartSliding()
        {
            movingSlider.enabled = true;
        }

        public void StopSliding()
        {
            movingSlider.enabled = false;
        }
    }
}
