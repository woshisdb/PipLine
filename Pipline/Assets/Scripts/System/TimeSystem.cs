using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem
{
    public int dayTime = 24;
    public int time;
    public TimeSystem()
    {
        time = 0;
    }
    public int GetDay()
    {
        return time/dayTime;
    }
    public int GetTime()
    {
        return time % dayTime;
    }
    public string GetTimeStr()
    {
        return GetDay() + ":" + GetTime();
    }
    public void Update()
    {
        time++;
    }
}
