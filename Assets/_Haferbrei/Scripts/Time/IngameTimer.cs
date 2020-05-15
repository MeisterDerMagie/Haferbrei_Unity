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
    [Saveable] private TimeSpan durationInDays; //in days

    [Saveable] private DateTime originalStartDate;
    [Saveable] private DateTime internalStartDate; //wenn sich die Duration ändert, muss auch das StartDatum basierend auf dem Progress angepasst werden
    [Saveable] private DateTime endDate;
    [Saveable] private DateTime pauseDate;

    [ProgressBar(0f, 1f)][ShowInInspector][ReadOnly][HideLabel]
    [Saveable] private float progress;

    [Saveable] private float updateFrequency; //in seconds
    [Saveable] private bool isRunning;
    [Saveable] private bool isSetUp;
    private CoroutineHandle coroutineHandle;

    //public
    public TimeSpan DurationInDays => durationInDays;
    public DateTime StartDate => originalStartDate;
    public DateTime EndDate => endDate;
    public float Progress => progress;
    public bool IsRunning => isRunning;

    //Events
    public Action onTimerEnded = delegate {  };
    
    //-- Constructors --
    public IngameTimer(){}
    //-- --

    public void RunTimer(float _durationInRealtimeSeconds, float _updateFrequency)
    { 
        var calculatedDurationInDays = IngameDateTime.RealtimeSecondsToIngameTimeSpan(_durationInRealtimeSeconds);
        RunTimer(calculatedDurationInDays, _updateFrequency);
    }

    public void RunTimer(TimeSpan _durationInDays, float _updateFrequency)
    {
        if (CheckIfAlreadyRunning()) return;
        
        durationInDays = _durationInDays;
        updateFrequency = _updateFrequency;

        progress = 0f;
        
        originalStartDate = IngameDateTime.Now;
        CalculateStartAndEndDate();

        isSetUp = true;
        
        Resume();
    }

    public void CancelTimer()
    {
        //kill coroutine
        Timing.KillCoroutines(coroutineHandle);
        
        //unregister from manager
        IngameTimer_RunningTimers.UnregisterTimer(this);
        
        //reset values
        durationInDays = TimeSpan.Zero;
        originalStartDate = DateTime.MinValue;
        internalStartDate = DateTime.MinValue;
        endDate           = DateTime.MinValue;
        pauseDate         = DateTime.MinValue;
        progress = 0f;
        updateFrequency = -1f;
        isRunning = false;
        isSetUp = false;
    }

    public void Pause()
    {
        if (!isRunning) return; 
        
        //kill coroutine
        Timing.KillCoroutines(coroutineHandle);
        
        //unregister from manager
        IngameTimer_RunningTimers.UnregisterTimer(this);

        //set pause date
        pauseDate = IngameDateTime.Now;
        
        isRunning = false;
    }

    
    private IEnumerator<float> _Timer()
    {
        while (progress < 1f)
        {
            if(IngameDateTime.isRunning) CalculateProgress();

            yield return Timing.WaitForSeconds(updateFrequency);
        }

        IngameTimer_RunningTimers.UnregisterTimer(this);
        onTimerEnded?.Invoke();
        isRunning = false;
    }

    public void Resume()
    {
        if (isRunning) return;
        if (progress >= 1f) return;
        
        if (!isSetUp)
        {
            Debug.LogError("Can't resume timer because it's not set up. Make sure to start it by calling RunTimer() after canceling it.");
            return;
        }

        if (pauseDate != DateTime.MinValue)
        {
            var pauseDuration = IngameDateTime.Now.Subtract(pauseDate);
            ShiftTimer(pauseDuration);
            pauseDate = DateTime.MinValue;
        }
        
        RegisterAndRunCoroutine();
    }

    public void ResumeAfterLoadingSaveFile() => RegisterAndRunCoroutine();

    private void RegisterAndRunCoroutine()
    {
        IngameTimer_RunningTimers.RegisterTimer(this);
        coroutineHandle = Timing.RunCoroutine(_Timer());
        isRunning = true;
    }
    
    public void SetDuration(TimeSpan _newDuration)
    {
        if (_newDuration == durationInDays) return;

        //lastDurationInDays = durationInDays;
        durationInDays = _newDuration;
        
        CalculateStartAndEndDate();
    }

    private void CalculateStartAndEndDate()
    {
        //calculate the internal ("pseudo") startDate when the duration has changed
        var elapsed = TimeSpan.FromDays(progress * durationInDays.TotalDays);
        internalStartDate = IngameDateTime.Now.Subtract(elapsed);
        //calculate the new endDate
        endDate = internalStartDate.Add(durationInDays);
    }
    
    private void CalculateProgress()
    {
        float elapsedDays = IngameDateTime.Now.Subtract(internalStartDate).Days;

        double progressUnclamped = 1.0 / durationInDays.TotalDays * elapsedDays;
        progress = Mathf.Clamp((float)progressUnclamped, 0f, 1f);
    }

    //shift the whole timer to an earlier or later time
    private void ShiftTimer(TimeSpan _shiftSpan)
    {
        internalStartDate = internalStartDate.Add(_shiftSpan);
        endDate = endDate.Add(_shiftSpan);
    }

    public bool CheckIfAlreadyRunning()
    {
        if (isRunning) Debug.LogError("Timer is already running. Make sure to cancel it or wait for end before running it again.");
        return isRunning;
    }
    
    #if UNITY_EDITOR
    // der Inspector updated nicht richtig, das hier löst das Problem
    [OnInspectorGUI]
    private void OnInspectorGUI() => Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
    #endif
}
}