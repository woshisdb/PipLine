using System;
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
    /// <summary>
    /// 所选择的最优路径
    /// </summary>
    public Dictionary<GoodsEnum, Tuple<BuildingObj, List<Path>, int>> goods2Path;
	public override void Update()
	{
        ///来源
        foreach (var goodsPath in goods2Path)
        {
            RequestRes(goodsPath.Key,,goodsPath.Value.Item3,);//去请求资源
        }
    }
    /// <summary>
    /// 商品与需求的数目,与每一个的价格
    /// </summary>
    /// <param name="goods"></param>
    /// <param name="sum"></param>
    public void RequestRes(GoodsEnum goodsEnum,int sum,int cost,int time)
    {
        var fromRes = GoodsGen.GetGoodsObj(goodsEnum);//获取商品
        var maxNum = sum;//请求的资源数目
        foreach (var tran in trans.edge.tras)
        {
            var remain = productivity.remain[tran.Key] / tran.Value;
            maxNum = Mathf.Min(maxNum, remain);
        }
        //int carryNum = fromRes.sum / trans.from.source[0].Item2;
        //int realcarry = Mathf.Min(Mathf.Min(carryNum, maxNum), ((CarryTrans)trans).maxTrans);
        //fromRes.sum -= maxNum * trans.from.source[0].Item2;
        var goods = GoodsGen.GetGoodsObj(goodsEnum);
        goods.sum =maxNum;
        //goods.sum = maxNum * trans.from.source[0].Item2;
        belong.scene.paths[to.building.scene].PushOrder(from, to, goods, cost,time);
    }

	public CarrySource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
	{
        this.belong = building;
		this.from = from;
		this.to = to;
        this.trans = trans;
		this.productivity = productivity;
        goods2Path = new Dictionary<GoodsEnum, Tuple<BuildingObj, List<Path>, int>>();
    }
    /// <summary>
    /// 更新一系列的资源
    /// </summary>
    /// <param name="goodsEnum"></param>
    public void UpdateAllResource()
    {
        goods2Path.Clear();
        foreach (var goods in from.building.goodsEnums)//选择最优路线
        {
            UpdateResource(goods);
        }
    }
    public void UpdateResource(GoodsEnum goodsEnum)
    {
        var obj = GoodsGen.GetGoodsObj(goodsEnum);
        var res = GameArchitect.get.economicSystem.GetGoods(obj, belong.scene);//获得原料中的最小的获取成本
        var result = res.FindMinElement(e => { return e.Item3; });//获得所有成本中最小的那个
        goods2Path.Add(goodsEnum,result);//结果与资源的合集
    }

}