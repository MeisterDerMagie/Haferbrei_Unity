using System;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{

[Serializable]
[InlineProperty]
[SaveableClass]
public class IngameTimer
{
    [Saveable] private float durationInDays; //in days

    [Saveable] private DateTime originalStartDate;
    [Saveable] private DateTime internalStartDate; //wenn sich die Duration ändert, muss auch das StartDatum basierend auf dem Progress angepasst werden
    [Saveable] private DateTime endDate;

    [ProgressBar(0f, 1f)][ShowInInspector][ReadOnly][HideLabel]
    [Saveable] private float progress;

    [Saveable] private float updateFrequency; //in seconds

    //public
    public float DurationInDays => durationInDays;
    public DateTime StartDate => originalStartDate;
    public DateTime EndDate => endDate;
    public float Progress => progress;

    //Events
    public Action onTimerEnded = delegate {  };
    
    //-- Constructors --
    public IngameTimer(){}
    
    public IngameTimer(float _durationInDays, float _updateFrequency, float _initialProgress = 0f, bool _immediateStart = true)
    {
        durationInDays = _durationInDays;
        updateFrequency = _updateFrequency;

        progress = _initialProgress;
        
        originalStartDate = IngameDateTime.Now;
        CalculateStartAndEndDate();
        
        if(_immediateStart) Start();
    }
    //-- --

    
    private IEnumerator<float> _Timer()
    {
        while (progress < 1f)
        {
            if(IngameDateTime.isRunning) CalculateProgress();

            yield return Timing.WaitForSeconds(updateFrequency);
        }

        IngameTimer_StartTimersAfterLoading.UnregisterTimer(this);
        onTimerEnded?.Invoke();
    }

    public void Start()
    {
        IngameTimer_StartTimersAfterLoading.RegisterTimer(this);
        Timing.RunCoroutine(_Timer());
    }
    
    
    public void SetDuration(float _newDuration)
    {
        if (_newDuration == durationInDays) return;

        //lastDurationInDays = durationInDays;
        durationInDays = _newDuration;
        
        //adjust the internal ("pseudo") startDate when the duration has changed
        var elapsed = TimeSpan.FromDays(progress * durationInDays);
        internalStartDate = IngameDateTime.Now.Subtract(elapsed);
        endDate = internalStartDate.Add(TimeSpan.FromDays(durationInDays));
    }

    private void CalculateStartAndEndDate()
    {
        //calculate the internal ("pseudo") startDate when the duration has changed
        var elapsed = TimeSpan.FromDays(progress * durationInDays);
        internalStartDate = IngameDateTime.Now.Subtract(elapsed);
        //calculate the new endDate
        endDate = internalStartDate.Add(TimeSpan.FromDays(durationInDays));
    }
    
    private void CalculateProgress()
    {
        float elapsedDays = IngameDateTime.Now.Subtract(internalStartDate).Days;

        float progressUnclamped = 1f / durationInDays * elapsedDays;
        progress = Mathf.Clamp(progressUnclamped, 0f, 1f);
    }
    
    #if UNITY_EDITOR
    // der Inspector updated nicht richtig, das hier löst das Problem
    [OnInspectorGUI]
    private void OnInspectorGUI() => Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
    #endif
}
}