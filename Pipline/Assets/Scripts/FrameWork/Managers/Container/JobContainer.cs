using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工作的集合
/// </summary>
public class NPCJobSet : NPCItem
{
    public NPCJobSet():base()
    {
        
    }
}

public class RewardAct : Act
{
    public RewardAct(Job job) : base(job)
    {

    }

    public override IEnumerator Run()
    {
        foreach(var x in job.npcs)
        {

        }
        yield return null;
    }

    public override int WasterTime()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 一系列的工作
/// </summary>
public abstract class Job : NPCKey
{
    /// <summary>
    /// 工作的名称
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public int beginWorkTime;
    public int endWorkTime;
    public List<DayWork> dayWorks;
    public List<NpcObj> npcs;
    public BuildingObj buildingObj;
    public Act befAct;
    public Act endAct;
    public Act rewardAct;
    /// <summary>
    /// 工作的总数目
    /// </summary>
    public int sum;
    /// <summary>
    /// 工作的收益
    /// </summary>
    public int money;
    public int ps = 0;
    public int signed { get { return npcs.Count; } }
    public int npcSum { get { return GameArchitect.get.npcManager.jobContainer[this].Count(); } }
    public Job(BuildingObj building)
    {
        this.npcs = new List<NpcObj>();
        dayWorks = new List<DayWork>();
    }
    public virtual float GetRate(NpcObj npcObj)
    {
        return 1;
    }
    public void RegisterJob(NpcObj npcObj)
    {
        sum--;
        npcs.Add(npcObj);
        npcObj.lifeStyle.job = this;
    }
    public bool InWorkTime(int time)
    {
        return time >= beginWorkTime && time <= endWorkTime;
    }
    public bool InEndTime(int time)
    {
        return (time == endWorkTime + 1);
    }
    public bool InStartTime(int time)
    {
        return (time == beginWorkTime);
    }
    public void SetDayJob()
    {
        //GameArchitect.get.npcManager.jobContainer[this];
        var dayWork = dayWorks[ps];
        befAct = dayWork.GetPreAct();
        endAct = dayWork.GetEndAct();
        ps = (ps + 1) % dayWorks.Count;
    }
}

public class jobContainer : NPCContainer<Job, NPCJobSet>
{
    public void Update()
    {
        foreach(var x in npcContainers)
        {
            x.Key.SetDayJob();
        }
    }
}
