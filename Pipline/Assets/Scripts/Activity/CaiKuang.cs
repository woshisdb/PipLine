using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiKuangBeginAct : BeginJobAct<CaiKuangJob,CaiKuangJobInstance> {
    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiKuangBeginAct(Job job,int wasterTime,string tranName) :base(job, GoodsEnum.手,tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiKuangEndAct : EndJobAct<CaiKuangJob, CaiKuangJobInstance>
{
    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiKuangEndAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum.手, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiKuangJobInstance : NormalJobInstance
{
    public CaiKuangJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class CaiKuangJob : NormalJob
{
    public CaiKuangJob(BuildingObj building) : base("开采铁矿石",(e,f) => { return new CaiKuangJobInstance(e, f); },building,8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday=new DayWork();
        workday.preAct= new CaiKuangBeginAct(this,8,tranName);
        workday.endAct = new CaiKuangEndAct(this,8,tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}
