using System;
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
        return preAct;
    }
    /// <summary>
    /// 后继活动
    /// </summary>
    public Act GetEndAct(NpcObj npc)
    {
        return endAct;
    }

    public int GetWasterTime()
    {
        return preAct.WasterTime();
    }
}

public class JobInstance
{
    public int ps=0;
    public Job job;
    public NpcObj npc;
    public void SetDayJob()
    {
        var dayWork = job.dayWorks[ps];
        dayWork.preAct.instance = this;
        dayWork.endAct.instance = this;
        npc.befAct = dayWork.GetPreAct(npc);
        npc.endAct = dayWork.GetEndAct(npc);
        ps = (ps + 1) % job.dayWorks.Count;
    }
    public JobInstance(Job job, NpcObj npc)
    {
        this.ps = 0;
        this.job = job;
        this.npc = npc;
    }
}

public abstract class Job
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
    public Func<Job,NpcObj,JobInstance> func;
    /// <summary>
    /// 工作的总数目
    /// </summary>
    public int sum;
    public int signed { get { return npcs.Count; } }
    public Job(Func<Job, NpcObj, JobInstance> func,BuildingObj building)
    {
        this.func = func;
        this.npcs=new List<NpcObj>();
        dayWorks = new List<DayWork>();
    }
    public virtual float GetRate(NpcObj npcObj)
    {
        return 1;
    }
    public void RegisterJob(NpcObj npcObj)
    {
        var ins = func(this, npcObj);
        sum--;
        npcs.Add(npcObj);
        npcObj.lifeStyle.job = ins;
    }
    public bool InWorkTime(int time)
    {
        return time>=beginWorkTime&&time<=endWorkTime;
    }
    public bool InEndTime(int time)
    {
        return (time==endWorkTime+1);
    }
    public bool InStartTime(int time)
    {
        return (time == beginWorkTime + 1);
    }
}

public class JobManager
{
    BuildingObj buildingObj;
    public Dictionary<Type,Job> jobs;
    public JobManager(BuildingObj buildingObj)
    {
        this.buildingObj = buildingObj;
        jobs = new Dictionary<Type,Job>();
    }
    public bool RegisterJob<T>(NpcObj npc)
    where T : Job
    {
        Job ret;
        var t = jobs.TryGetValue(typeof(T),out ret);
        if(ret!=null)
        {
            ret.RegisterJob(npc);
            return true;
        }
        else
        {
            return false;
        }
    }
}