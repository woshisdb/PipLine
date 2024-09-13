using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianZhiBeginAct : BeginJobAct<LianZhiJob, LianZhiJobInstance>
{
    public override int WasterTime()
    {
        return wasterTime;
    }
    public LianZhiBeginAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum. ÷, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class LianZhiEndAct : EndJobAct<LianZhiJob, LianZhiJobInstance>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public LianZhiEndAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum. ÷, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class LianZhiJobInstance : NormalJobInstance
{
    public LianZhiJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class LianZhiJob : NormalJob
{
    public LianZhiJob(BuildingObj building) : base("¡∂÷∆Ã˙øÛ", (e, f) => { return new LianZhiJobInstance(e, f); }, building,8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new LianZhiBeginAct(this, 8,tranName);
        workday.endAct = new LianZhiEndAct(this, 8,tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}