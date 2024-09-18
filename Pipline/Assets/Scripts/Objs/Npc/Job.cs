using System;
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



public class JobManager
{
    BuildingObj buildingObj;
    public Dictionary<Type,Job> jobs;
    public JobManager(BuildingObj buildingObj)
    {
        this.buildingObj = buildingObj;
        jobs = new Dictionary<Type,Job>();
    }
    public bool RegisterJob<T>(NpcObj npc)//��Ҫ��¼��NPCManager��
    where T : Job
    {
        Job ret;
        var t = jobs.TryGetValue(typeof(T),out ret);
        if(ret!=null)
        {
            ret.RegisterJob(npc);
            //ע��һ�������˿�
            GameArchitect.get.npcManager.jobContainer.RegContainer(ret, npc);
            return true;
        }
        else
        {
            return false;
        }
    }
}