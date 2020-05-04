using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimePhases : ScriptableObject
{
    [SerializeField] Phase[] phases = null;

    [Serializable]
    public class Phase
    {
        [SerializeField] public Sprite phaseSprite;
        [Range(0, 1000)]
        [SerializeField] int timeStamp;
    }

    public Phase[] GetPhases()
    {
        return phases;
    }
}
