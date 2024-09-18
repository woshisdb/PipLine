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
    public CarryBeginAct(Job job, int wasterTime, string tranName) : base(job, GoodsEnum.��, tranName, wasterTime)
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
    public CarryEndAct(Job job, int wasterTime, string tranName) : base(job, GoodsEnum.��, tranName, wasterTime)
    {
        this.wasterTime = wasterTime;
    }
}

public class CarryJob : NormalJob
{
    public CarryJob(BuildingObj building) : base("������Ʒ", building, 8)
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
        title = "������Ʒ";
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
    /// ִ��������Դ
    /// </summary>
    public void Execute()
    {
        from.ReceiveRes(this);
        GameArchitect.get.economicSystem.AddBuyB(cost,govCost,from,to,goods.sum, goods.goodsInf.goodsEnum);//������
        GameArchitect.get.economicSystem.AddBuy(cost,govCost,to.scene,goods.sum,goods.goodsInf.goodsEnum);//������
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
    /// <summary>
    /// ��ѡ�������·��
    /// </summary>
    public Dictionary<GoodsEnum, GoodPath> goods2Path;
    /// <summary>
    /// ������Դ����Ŀ
    /// </summary>
    public Dictionary<GoodsEnum, ALLOrder> allOrders;//ӵ�ж��ٵ���Ʒ
    public Dictionary<GoodsEnum,int> inputsSum;
    /// <summary>
    /// ��Դ����Ŀ
    /// </summary>
    public int resourceSum=1;//���������ٵĲ�Ʒ
    /// <summary>
    /// �ȱ�������Դ
    /// </summary>
	public override void Update()
	{
        foreach (var goodsPath in goods2Path)
        {
            int varSource =Math.Max( resourceSum * inputsSum[goodsPath.Key] - allOrders[goodsPath.Key].get() ,0);//��Ҫ���������
            if(varSource > 0)
            RequestRes(goodsPath.Key,goodsPath.Value.from, varSource, goodsPath.Value.cost,goodsPath.Value.govcost,goodsPath.Value.wasterTime);//ȥ������Դ
        }
    }
    /// <summary>
    /// ��Ʒ���������Ŀ,��ÿһ���ļ۸�
    /// </summary>
    /// <param name="goods"></param>
    /// <param name="sum"></param>
    public void RequestRes(GoodsEnum goodsEnum,BuildingObj building,int sum,int cost,int govCost,int time)
    {
        var maxNum = Math.Min(sum,belong.money.money/cost);//�������Դ��Ŀ
        foreach (var tran in trans.edge.tras)
        {
            var remain = productivity.remain[tran.Key] / tran.Value;
            maxNum = Mathf.Min(maxNum, remain);
        }
        var goods = GoodsGen.GetGoodsObj(goodsEnum);
        goods.sum =maxNum;
        this.Execute(new RequestGoodsCommand(building,this.belong,goods,time,cost,govCost));
    }
	public CarrySource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
	{
        this.belong = building;
		this.from = from;
		this.to = to;
        this.trans = trans;
		this.productivity = productivity;
        goods2Path = new Dictionary<GoodsEnum, GoodPath>();
        inputsSum = new Dictionary<GoodsEnum, int>();//һϵ�е���Ʒ
        allOrders = new Dictionary<GoodsEnum, ALLOrder>();
        foreach (var tran in trans.from.source)
        {
            inputsSum.Add(tran.Item1, tran.Item2);
            allOrders.Add(tran.Item1,new ALLOrder(belong.resource, tran.Item1));
        }
    }
    /// <summary>
    /// ����һϵ�еĽ�����Դ,Ȼ����»����ɱ�
    /// </summary>
    /// <param name="goodsEnum"></param>
    public void UpdateAllResource()
    {
        goods2Path.Clear();
        foreach (var goods in belong.inputGoods)//ѡ������·��
        {
            UpdateResource(goods);
        }
    }
    public void UpdateResource(GoodsEnum goodsEnum)
    {
        var obj = GoodsGen.GetGoodsObj(goodsEnum);
        var res = GameArchitect.get.economicSystem.GetGoods(obj, belong.scene);//���ԭ���е���С�Ļ�ȡ�ɱ�
        var result = res.FindMinElement(e => { return e.cost; });//������гɱ�����С���Ǹ�
        if(result != null)
        {
            result.wasterTime = Math.Max(result.wasterTime, 1);
            goods2Path.Add(goodsEnum, result);//�������Դ�ĺϼ�
        }
    }

}