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

public struct UnSatifyGoods : ICommand
{
    RequestGoodsCommand requestGoods;
    public void Execute()
    {
    }
    public UnSatifyGoods(RequestGoodsCommand requestGoods)
    {
        this.requestGoods = requestGoods;
    }
}
public struct RequestGoodsCommand:ICommand
{
    public BuildingObj to;
    public BuildingObj from;
    public GoodsObj goods;
    public int wasterTime;
    public int cost;
    public RequestGoodsCommand(BuildingObj from,BuildingObj to,GoodsObj goods,int wasterTime,int cost)
    {
        this.from = from;
        this.to = to;
        this.goods = goods;
        this.wasterTime = wasterTime;
        this.cost = cost;
    }
    /// <summary>
    /// 执行请求资源
    /// </summary>
    public void Execute()
    {
        from.ReceiveRes(this);
        GameArchitect.get.economicSystem.AddBuy(cost,to.scene,goods.sum,goods.goodsInf.goodsEnum);
    }
}

public class CarrySource : Source,ISendEvent,ISendCommand
{
    /// <summary>
    /// 所选择的最优路径
    /// </summary>
    public Dictionary<GoodsEnum, GoodPath> goods2Path;
	public override void Update()
	{
        ///来源
        foreach (var goodsPath in goods2Path)
        {
            RequestRes(goodsPath.Key,goodsPath.Value.from,10,goodsPath.Value.cost,goodsPath.Value.wasterTime);//去请求资源
        }
    }
    /// <summary>
    /// 商品与需求的数目,与每一个的价格
    /// </summary>
    /// <param name="goods"></param>
    /// <param name="sum"></param>
    public void RequestRes(GoodsEnum goodsEnum,BuildingObj building,int sum,int cost,int time)
    {
        var maxNum = Math.Min(sum,belong.money.money/cost);//请求的资源数目
        foreach (var tran in trans.edge.tras)
        {
            var remain = productivity.remain[tran.Key] / tran.Value;
            maxNum = Mathf.Min(maxNum, remain);
        }
        var goods = GoodsGen.GetGoodsObj(goodsEnum);
        goods.sum =maxNum;
        this.Execute(new RequestGoodsCommand(building,this.belong,goods,time,cost));
    }

	public CarrySource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
	{
        this.belong = building;
		this.from = from;
		this.to = to;
        this.trans = trans;
		this.productivity = productivity;
        goods2Path = new Dictionary<GoodsEnum, GoodPath>();
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
        var result = res.FindMinElement(e => { return e.cost; });//获得所有成本中最小的那个
        goods2Path.Add(goodsEnum,result);//结果与资源的合集
    }

}