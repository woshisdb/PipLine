using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiMeiBeginAct : BeginJobAct<LianZhiJob>
{
    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiMeiBeginAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum.��, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiMeiEndAct : EndJobAct<LianZhiJob>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiMeiEndAct(Job job, int wasterTime,string tranName) : base(job,GoodsEnum.��, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiMeiJob : NormalJob
{
    public CaiMeiJob(BuildingObj building) : base("����ú̿", building,8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new CaiMeiBeginAct(this, 8,tranName);
        workday.endAct = new CaiMeiEndAct(this, 8,tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}