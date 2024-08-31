using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DayWork
{
    public Act preAct;
    public Act endAct;
    /// <summary>
    /// ǰ���
    /// </summary>
    public Act GetPreAct()
    {
        return preAct;
    }
    /// <summary>
    /// ��̻
    /// </summary>
    public Act GetEndAct()
    {
        return endAct;
    }

    public int GetWasterTime()
    {
        return preAct.WasterTime();
    }
}

public class Job
{
    /// <summary>
    /// ָ��
    /// </summary>
    public int ps;
    /// <summary>
    /// ����������
    /// </summary>
    public string name;
    public List<DayWork> dayWorks;
    public NpcObj npc;
    public Job(NpcObj npc)
    {
        ps = 0;
        this.npc = npc;
        dayWorks = new List<DayWork>();
    }
    public void SetDayJob()
    {
        var dayWork = dayWorks[ps];
        npc.befAct=dayWork.GetPreAct();
        npc.endAct = dayWork.GetEndAct();
        ps = (ps + 1) % dayWorks.Count;
    }
}
