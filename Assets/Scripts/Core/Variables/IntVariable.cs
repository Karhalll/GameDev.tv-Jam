using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.Core.Variables
{
    [CreateAssetMenu]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] int Value = 0;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVariable value)
        {
            Value = value.Value;
        }

        public int GetValue()
        {
            return Value;
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(IntVariable amount)
        {
            Value += amount.Value;
        }
    }
}