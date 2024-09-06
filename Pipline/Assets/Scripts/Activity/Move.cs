using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBeginAct : BeginJobAct<CaiKuangJob, CaiKuangJobInstance>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryBeginAct(Job job, int wasterTime, string tranName) : base(job, GoodsEnum.带铁矿石, tranName, wasterTime)
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
    public CarryEndAct(Job job, int wasterTime, string tranName) : base(job, GoodsEnum.手, tranName, wasterTime)
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
    public CarryJob(BuildingObj building) : base("搬运商品", (e, f) => { return new LianZhiJobInstance(e, f); }, building, 8)
    {
        this.buildingObj = building;
        dayWorks = new List<DayWork>();
        var workday = new DayWork();
        workday.preAct = new CarryBeginAct(this, 8, tranName);
        workday.endAct = new CarryEndAct(this, 8, tranName);
        dayWorks.Add(workday);
        sum = 100;
    }
}
public class CarrySource : Source
{
	public Resource from;
	public Resource to;
    public Trans trans;
	public Productivity productivity;
	public override void Update()
	{

	}

	public CarrySource(Resource from, Resource to, Trans trans, Productivity productivity)
	{
		this.from = from;
		this.to = to;
        this.trans = trans;
		this.productivity = productivity;
	}
}