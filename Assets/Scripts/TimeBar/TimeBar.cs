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
        }
    }
}
