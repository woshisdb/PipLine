using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;


/// <summary>
/// NPC�Ļ
/// </summary>
public abstract class Act
{
    /// <summary>
    /// �˷ѵ�ʱ��
    /// </summary>
    public abstract int WasterTime();
    public abstract IEnumerator Run();
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