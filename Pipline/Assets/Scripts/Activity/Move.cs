using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBeginAct : Act<CaiKuangJob, CaiKuangJobInstance>
{
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.Add(GoodsEnum.返, 2);
        building.resource.GetGoods<HandObj>(GoodsEnum.返).UseTool(npc, 2, wasterTime);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryBeginAct(Job job, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
    }
}

public class CarryEndAct : Act<CaiKuangJob, CaiKuangJobInstance>
{
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.GetGoods<HandObj>(GoodsEnum.返).ReleaseTool(npc, 2, wasterTime);
        building.resource.Remove(GoodsEnum.返, 2);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryEndAct(Job job, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
    }
}

public class CarryJobInstance : JobInstance
{
    public CarryJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class CarryJob : Job
{
    public CarryJob(BuildingObj building) : base((e, f) => { return new LianZhiJobInstance(e, f); }, building)
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