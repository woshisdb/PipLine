using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;


/// <summary>
/// NPC的活动
/// </summary>
public abstract class Act
{
    /// <summary>
    /// 浪费的时间
    /// </summary>
    public abstract int WasterTime();
    public abstract IEnumerator Run();
}
/// <summary>
/// NPC的行为
/// </summary>
public abstract class Activity
{
    public string activityName { get { return GetType().Name; } }
    /// <summary>
    /// 获取角色的行为 
    /// </summary>
    /// <returns></returns>
    public abstract Act GetNPCAct();
    /// <summary>
    /// 获取活动的执行时间
    /// </summary>
    /// <returns></returns>
    public abstract int GetTime();
}

public class SeqNpcAct : Act
{
    public List<Act> npcActs;
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
        for(int i=0;i<npcActs.Count;i++)
        {
            yield return npcActs[i].Run();
        }
    }
}