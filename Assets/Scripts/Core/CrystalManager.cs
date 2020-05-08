using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using GameDevJam.Core.Variables;

namespace GameDevJam.Core
{
    public class CrystalManager : MonoBehaviour
    {
        [SerializeField] IntVariable maxCrystals = null;
        [SerializeField] IntVariable startingCrystals = null;
        [SerializeField] IntVariable currentCrystals = null;

        [SerializeField] UnityEvent crystalChange = null;

        private void Awake() 
        {
            currentCrystals.SetValue(startingCrystals.GetValue());
        }

        public void AddCrystal()
        {
            if (currentCrystals.GetValue() < maxCrystals.GetValue())
            {
                currentCrystals.SetValue(currentCrystals.GetValue() + 1);
            }
            else
            {
                currentCrystals.SetValue(maxCrystals.GetValue());
            }

            CrystalChange();
        }

        private void CrystalChange()
        {
            crystalChange.Invoke();
        }
    }
}
