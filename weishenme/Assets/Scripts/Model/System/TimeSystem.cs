using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : Singleton<TimeSystem>
{
    /// <summary>
    /// ��ǰ�ǵڼ���
    /// </summary>
    public int nowDay;
    public int getNow()
    {
        return nowDay;
    }
    public int dayTime;
}
