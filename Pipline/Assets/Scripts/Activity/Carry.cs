using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBeginAct : BeginJobAct<CaiKuangJob>
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

public class CarryEndAct : EndJobAct<CaiKuangJob>
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

public class CarryJob : NormalJob
{
    public CarryJob(BuildingObj building) : base("搬运商品", building, 8)
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
    public CarryTrans()
    {
        title = "搬运商品";
    }
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
    public int govCost;
    public RequestGoodsCommand(BuildingObj from,BuildingObj to,GoodsObj goods,int wasterTime,int cost,int govCost)
    {
        this.from = from;
        this.to = to;
        this.goods = goods;
        this.wasterTime = wasterTime;
        this.cost = cost;
        this.govCost = govCost;
    }
    /// <summary>
    /// 执行请求资源
    /// </summary>
    public void Execute()
    {
        from.ReceiveRes(this);
        GameArchitect.get.economicSystem.AddBuyB(cost,govCost,from,to,goods.sum, goods.goodsInf.goodsEnum);//请求奖励
        GameArchitect.get.economicSystem.AddBuy(cost,govCost,to.scene,goods.sum,goods.goodsInf.goodsEnum);//请求奖励
    }
}

public class ALLOrder
{
    public Resource resource;
    public int orderSum=0;
    public GoodsEnum goods;
    public int get()
    {
        return orderSum + resource.Get(goods);
    }
    public ALLOrder(Resource resource, GoodsEnum goods)
    {
        orderSum=0;
        this.resource = resource;
        this.goods = goods;
    }
}

public class CarrySource : Source,ISendEvent,ISendCommand
{
    
}