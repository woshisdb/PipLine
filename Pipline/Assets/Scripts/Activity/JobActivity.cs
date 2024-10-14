using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//JobActivity
public class BeginJobAct<T> : Act<T>
where T: NormalJob
{
    public string tranName;
    public GoodsEnum goodsEnum;
    public int wasterTime;
    public Source source;
    public override IEnumerator Run()
    {
        source.productivity.UseTools(job.npcs);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public BeginJobAct(Job job,GoodsEnum goodsEnum,string tranName, int wasterTime) : base(job)
    {
        this.tranName = tranName;
        this.wasterTime = wasterTime;
        this.goodsEnum = goodsEnum;
        source = building.pipLineManager.GetTrans(tranName);
    }
}

public class EndJobAct<T> : Act<T>
where T : NormalJob
{
    public string tranName;
    public GoodsEnum goodsEnum;
    public int wasterTime;
    public Source source;
    public override IEnumerator Run()
    {
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public EndJobAct(Job job, GoodsEnum goodsEnum,string tranName, int wasterTime) : base(job)
    {
        this.tranName = tranName;
        this.wasterTime = wasterTime;
        this.goodsEnum = goodsEnum;
        this.source = building.pipLineManager.GetTrans(tranName);
    }
}

public class NormalJob : Job
{
    public string tranName;
    public NormalJob(string tranName, BuildingObj building, int wastertime) : base(building)
    {
        this.tranName = tranName;
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new LianZhiBeginAct(this, wastertime,tranName);
        workday.endAct = new LianZhiEndAct(this, wastertime, tranName);
        dayWorks.Add(workday);
        sum = 100;
        money = 10;
    }
}