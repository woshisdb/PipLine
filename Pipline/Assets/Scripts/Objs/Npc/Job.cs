using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayWork
{
    public Act preAct;
    public Act endAct;
    /// <summary>
    /// 前驱活动
    /// </summary>
    public Act GetPreAct(NpcObj npc)
    {
        preAct.npc = npc;
        return preAct;
    }
    /// <summary>
    /// 后继活动
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
    /// 指针
    /// </summary>
    public int ps;
    /// <summary>
    /// 工作的名称
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
