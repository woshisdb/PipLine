using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

public interface IAct<T>
where T : Job
{
    public T GetJob();
}

/// <summary>
/// NPC�Ļ
/// </summary>
public abstract class Act
{
    public Job job;
    public BuildingObj building { get { return job.buildingObj; } }
    /// <summary>
    /// �˷ѵ�ʱ��
    /// </summary>
    public abstract int WasterTime();
    public abstract IEnumerator Run();
    public Act(Job job)
    {
        this.job = job;
    }
}
public abstract class Act<T> : Act,IAct<T>
where T : Job
{
    protected Act(Job job) : base(job)
    {
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
    public SeqNpcAct(Job job, params Act[] npcActs):base(job)
    {
        this.npcActs = npcActs;
    }
}