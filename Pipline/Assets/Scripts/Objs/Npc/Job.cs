using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayWork
{
    public Act preAct;
    public Act endAct;
    /// <summary>
    /// ǰ���
    /// </summary>
    public Act GetPreAct(NpcObj npc)
    {
        preAct.npc = npc;
        return preAct;
    }
    /// <summary>
    /// ��̻
    /// </summary>
    public Act GetEndAct(NpcObj npc)
    {
        endAct.npc = npc;
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
    public Job()
    {
        ps = 0;
        this.npc=null;
        dayWorks = new List<DayWork>();
    }
    public void Init(NpcObj npc)
    {
        this.npc=npc;
    }
    public void SetDayJob()
    {
        var dayWork = dayWorks[ps];
        npc.befAct=dayWork.GetPreAct(npc);
        npc.endAct = dayWork.GetEndAct(npc);
        ps = (ps + 1) % dayWorks.Count;
    }
}
