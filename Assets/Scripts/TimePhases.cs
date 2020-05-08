using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDevJam.Interactable;

[CreateAssetMenu]
public class TimePhases : ScriptableObject
{
    [SerializeField] TimePhase[] timePhases = null;

    [Serializable]
    public class TimePhase
    {
        [Range(0, 1000)]
        public int timeStamp = 0;
        public Phase phase = null;
    }

    private List<TimePhase> phaseList = new List<TimePhase>();

    private void OnEnable() 
    {
        CreateList();
    }

    private void CreateList()
    {
        phaseList.Clear(); // neresi muj problem
        foreach (var phase in timePhases)
        {
            phaseList.Add(phase);
        }
    }

    public List<TimePhase> GetPhases()
    {
        return phaseList;
    }
}
