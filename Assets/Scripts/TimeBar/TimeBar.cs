using UnityEngine;
using UnityEngine.UI;

namespace GameDevJam.TimeBar
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] float maxTimeToManip = 1000f;
        [SerializeField] int maxCystals = 5;
        [SerializeField] int crystalsOwned = 2;

        [SerializeField] Slider activeBarFill = null;
        [SerializeField] Slider movingSlider = null;

        [SerializeField] Transform phaseStamps = null;
        [SerializeField] GameObject phaseStampPref = null;
        [SerializeField] TimePhases objectsTimePhases = null;

        float currenTimeToManip; 

        private void Awake() 
        {
            currenTimeToManip = (maxTimeToManip / maxCystals) * crystalsOwned;
        }

        private void Start() 
        {
            activeBarFill.maxValue = maxTimeToManip;
            activeBarFill.value = currenTimeToManip;

            movingSlider.maxValue = currenTimeToManip;

            SpawnPhaseStamps();
        }

        private void SpawnPhaseStamps()
        {
            DestoryChildrenIn(phaseStamps);
            TimePhases.Phase[] phases = objectsTimePhases.GetPhases();

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

            if (placement > currenTimeToManip)
            {
                ColorBlock newColorBlock = phaseStampSlider.colors;
                newColorBlock.disabledColor = phaseStampSlider.colors.normalColor;
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
    }
}
