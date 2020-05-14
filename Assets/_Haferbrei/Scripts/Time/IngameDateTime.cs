//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
public class IngameDateTime : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Settings")] private float yearsPerHour = 30f;
    [SerializeField, BoxGroup("Settings")] private int startYear, startMonth, startDay;
    
    [Saveable] private static DateTime ingameDateTime;
    public static DateTime Now => ingameDateTime;
    public static bool isRunning = false;
    
    private const double oneYearInSeconds = 31536000.0;
    
    //--- Events ---
    public static Action OnNewYear;
    public static Action OnNewMonth;
    public static Action OnNewDay;
    //---
    
    //--- values for comparison ---
    private int yearBefore;
    private int monthBefore;
    private int dayBefore;
    //--- ---
    
    //-- zeigt das Datum im Inspector an ---
    #region Inspector
    #if UNITY_EDITOR
    [ShowInInspector, BoxGroup("Info")] private int year => Now.Year;
    [ShowInInspector, BoxGroup("Info")] private int month => Now.Month;
    [ShowInInspector, BoxGroup("Info")] private int day => Now.Day;
    #endif
    #endregion
    //-- --
    
    public void InitSelf()
    {
        isRunning = true;
        
        if (!(ingameDateTime == DateTime.MinValue)) return;

        ingameDateTime = new DateTime(startYear, startMonth, startDay);
    }
    
    private void Update()
    {
        float timeToAdd = Time.deltaTime * GameTime.TimeScale;
        ingameDateTime = ingameDateTime.Add(RealtimeSecondsToIngameTime(timeToAdd));

        //fire changed-events
        if (yearBefore != Now.Year) OnNewYear?.Invoke();
        if (monthBefore != Now.Month) OnNewMonth?.Invoke();
        if (dayBefore != Now.Day) OnNewDay?.Invoke();
        
        //set values for comparison
        yearBefore = Now.Year;
        monthBefore = Now.Month;
        dayBefore = Now.Day;
    }

    private TimeSpan RealtimeSecondsToIngameTime(float _seconds)
    {
        double yearsPerSecond = yearsPerHour / 3600.0;
        double ingameSecondsPerRealtimeSeconds = oneYearInSeconds * yearsPerSecond;
        
        return TimeSpan.FromSeconds(_seconds * ingameSecondsPerRealtimeSeconds);
    }
}
}