using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//JobActivity
public class BeginJobAct : Act<CaiKuangJob, CaiKuangJobInstance>
{
    public GoodsEnum goodsEnum;
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.Add(goodsEnum, 2);
        building.resource.GetGoods<HandObj>(goodsEnum).UseTool(npc, 2, wasterTime);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public BeginJobAct(Job job,GoodsEnum goodsEnum, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
        this.goodsEnum = goodsEnum;
    }
}

public class EndJobAct : Act<CaiKuangJob, CaiKuangJobInstance>
{
    public GoodsEnum goodsEnum;
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.GetGoods<HandObj>(goodsEnum).ReleaseTool(npc, 2, wasterTime);
        building.resource.Remove(goodsEnum, 2);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public EndJobAct(Job job, GoodsEnum goodsEnum, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
        this.goodsEnum = goodsEnum;
    }
}

public class NormalJobInstance : JobInstance
{
    public NormalJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class NormalJob : Job
{
    public NormalJob(BuildingObj building,int wastertime) : base((e, f) => { return new LianZhiJobInstance(e, f); }, building)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new LianZhiBeginAct(this, wastertime);
        workday.endAct = new LianZhiEndAct(this, wastertime);
        dayWorks.Add(workday);
        sum = 100;
    }
}