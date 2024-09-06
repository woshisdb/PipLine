using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBeginAct : BeginJobAct<CaiKuangJob, CaiKuangJobInstance>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryBeginAct(Job job, int wasterTime, string tranName) : base(job,GoodsEnum.´øÌú¿óÊ¯, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CarryEndAct : EndJobAct<CaiKuangJob, CaiKuangJobInstance>
{
    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryEndAct(Job job, int wasterTime, string tranName) : base(job,GoodsEnum.ÊÖ, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CarryJobInstance : NormalJobInstance
{
    public CarryJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class CarryJob : NormalJob
{
    public CarryJob(BuildingObj building) : base((e, f) => { return new LianZhiJobInstance(e, f); }, building,8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new LianZhiBeginAct(this, 8, tranName);
        workday.endAct = new LianZhiEndAct(this, 8, tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}