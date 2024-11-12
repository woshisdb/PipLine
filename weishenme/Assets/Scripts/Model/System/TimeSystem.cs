using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : Singleton<TimeSystem>
{
    /// <summary>
    /// 当前是第几天
    /// </summary>
    public int nowDay;
    public int getNow()
    {
        return nowDay;
    }
    public int dayTime;
}
