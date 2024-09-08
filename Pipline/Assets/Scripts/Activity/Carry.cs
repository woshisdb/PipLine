using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBeginAct : BeginJobAct<CaiKuangJob, CaiKuangJobInstance>
{

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CarryBeginAct(Job job, int wasterTime, string tranName) : base(job, GoodsEnum.手, tranName, wasterTime)
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
public class CarryTrans : Trans
{
    public int maxTrans;
}
public class CarrySource : Source
{
	public override void Update()
	{
        ///来源
        var fromRes = from.GetGoods<GoodsObj>(trans.from.source[0].Item1);
        var maxNum = 99999999;
        foreach (var tran in trans.edge.tras)
        {
            var remain = productivity.remain[tran.Key] / tran.Value;
            maxNum = Mathf.Min(maxNum, remain);
        }
        int carryNum = fromRes.sum / trans.from.source[0].Item2;
        int realcarry = Mathf.Min(Mathf.Min(carryNum, maxNum), ((CarryTrans)trans).maxTrans);
        fromRes.sum -= realcarry * trans.from.source[0].Item2;
        var goods = GoodsGen.GetGoodsObj(trans.from.source[0].Item1);
        goods.sum = realcarry * trans.from.source[0].Item2;
        belong.scene.paths[to.building.scene].PushOrder(from, to, goods, trans.wasterTimes);
    }
	public CarrySource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
	{
        this.belong = building;
		this.from = from;
		this.to = to;
        this.trans = trans;
		this.productivity = productivity;
    }
    /// <summary>
    /// 更新一系列的资源
    /// </summary>
    /// <param name="goodsEnum"></param>
    public void UpdateResource(GoodsEnum goodsEnum)
    {
        var obj = GoodsGen.GetGoodsObj(goodsEnum);
        var res = GameArchitect.get.economicSystem.GetGoods(obj, belong.scene);//获得原料
        var result = res.FindMinElement(e => { return e.Item3; });
    }

}