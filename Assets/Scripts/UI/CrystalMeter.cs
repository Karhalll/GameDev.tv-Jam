using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameDevJam.Core.Variables;

namespace GameDevJam.UI
{
    public class CrystalMeter : MonoBehaviour
    {
        [SerializeField] Text crystalText = null;
        [SerializeField] IntVariable maxCrystals = null;
        [SerializeField] IntVariable currentCrystals = null;

        private void Start() 
        {
            UpdateCrystalUI();
        }

        public void UpdateCrystalUI()
        {
            string curCrys = currentCrystals.GetValue().ToString("#0");
            string maxCrys = maxCrystals.GetValue().ToString("##");

            crystalText.text = curCrys + "/" + maxCrys;
        }
    }

}