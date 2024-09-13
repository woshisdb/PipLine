using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuoFanBeginAct : BeginJobAct<LianZhiJob, LianZhiJobInstance>
{
    public override int WasterTime()
    {
        return wasterTime;
    }
    public ZuoFanBeginAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum.手, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class ZuoFanEndAct : EndJobAct<LianZhiJob, LianZhiJobInstance>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public ZuoFanEndAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum.手, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class ZuoFanJobInstance : NormalJobInstance
{
    public ZuoFanJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class ZuoFanJob : NormalJob
{
    public ZuoFanJob(BuildingObj building) : base("制作土豆块", (e, f) => { return new ZuoFanJobInstance(e, f); }, building,8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new ZuoFanBeginAct(this, 8,tranName);
        workday.endAct = new ZuoFanEndAct(this, 8,tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}