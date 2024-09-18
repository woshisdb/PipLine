using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiKuangBeginAct : BeginJobAct<CaiKuangJob> {
    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiKuangBeginAct(Job job,int wasterTime,string tranName) :base(job, GoodsEnum.手,tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiKuangEndAct : EndJobAct<CaiKuangJob>
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

public class CaiKuangJob : NormalJob
{
    public CaiKuangJob(BuildingObj building) : base("开采铁矿石",building,8)
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
