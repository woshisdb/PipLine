using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

public interface IAct<T,F>
where T : Job
where F:JobInstance
{
    public T GetJob();
    public F GetInstance();
}

/// <summary>
/// NPC�Ļ
/// </summary>
public abstract class Act
{
    public Job job;
    public JobInstance instance;
    public NpcObj npc { get { return instance.npc; } }
    public BuildingObj building { get { return job.buildingObj; } }
    /// <summary>
    /// �˷ѵ�ʱ��
    /// </summary>
    public abstract int WasterTime();
    public abstract IEnumerator Run();
    public Act(Job job,JobInstance instance)
    {
        this.job = job;
        this.instance = instance;
    }
}
public abstract class Act<T, F> : Act,IAct<T, F>
where T : Job
where F : JobInstance
{
    protected Act(Job job, JobInstance instance) : base(job, instance)
    {
    }

    public F GetInstance()
    {
        return (F)instance;
    }

    public T GetJob()
    {
        return (T)job;
    }
}
/// <summary>
/// NPC����Ϊ
/// </summary>
public abstract class Activity
{
    public string activityName { get { return GetType().Name; } }
    /// <summary>
    /// ��ȡ��ɫ����Ϊ 
    /// </summary>
    /// <returns></returns>
    public abstract Act GetNPCAct();
    /// <summary>
    /// ��ȡ���ִ��ʱ��
    /// </summary>
    /// <returns></returns>
    public abstract int GetTime();
}

public class SeqNpcAct : Act
{
    public Act[] npcActs;
    public override int WasterTime()
    {
        int sum = 0;
        foreach(var npcAct in npcActs)
        {
            sum += npcAct.WasterTime();
        }
        return sum;
    }
    public override IEnumerator Run()
    {
        for(int i=0;i<npcActs.Length;i++)
        {
            yield return npcActs[i].Run();
        }
    }
    public SeqNpcAct(Job job,JobInstance instance, params Act[] npcActs):base(job,instance)
    {
        this.npcActs = npcActs;
    }
}