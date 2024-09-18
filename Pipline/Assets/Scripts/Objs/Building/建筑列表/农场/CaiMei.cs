using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuoFanBeginAct : BeginJobAct<LianZhiJob>
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

public class ZuoFanEndAct : EndJobAct<LianZhiJob>
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

public class ZuoFanJob : NormalJob
{
    public ZuoFanJob(BuildingObj building) : base("制作土豆块" , building,8)
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