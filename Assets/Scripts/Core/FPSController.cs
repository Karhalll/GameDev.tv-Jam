using UnityEngine;

namespace GameDevJam.Core
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] int targetFrameRate = 144;

        private void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
