using UnityEngine;

namespace GameDevJam.Core
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] int targetFrameRate = 144;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
