using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianZhiBeginAct : Act<LianZhiJob, LianZhiJobInstance>
{
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.Add(GoodsEnum.手, 2);
        building.resource.GetGoods<HandObj>(GoodsEnum.手).UseTool(npc, building.pipLineManager, "炼制铁矿", 2, wasterTime);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public LianZhiBeginAct(Job job, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
    }
}

public class LianZhiEndAct : Act<LianZhiJob, LianZhiJobInstance>
{
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.GetGoods<HandObj>(GoodsEnum.手).ReleaseTool(npc, building.pipLineManager, "炼制铁矿", 2, wasterTime);
        building.resource.Remove(GoodsEnum.手, 2);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public LianZhiEndAct(Job job, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
    }
}

public class LianZhiJobInstance : JobInstance
{
    public LianZhiJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class LianZhiJob : Job
{
    public LianZhiJob(BuildingObj building) : base((e, f) => { return new LianZhiJobInstance(e, f); }, building)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new LianZhiBeginAct(this, 8);
        workday.endAct = new LianZhiEndAct(this, 8);
        dayWorks.Add(workday);
        sum = 100;
    }
}