using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生产商品的管线
/// </summary>
public class GenGoodsPipline : Pipline
{
    public int maxSum = 99999999;//每回合生产资源的数目
    public List<double> piplineList;
    public Trans trans;//商品间的转移关系
    public Resource from;//原材料仓库
    public Resource to;//商品的仓库
    public Productivity productivity;
    public int wasterTimes;
    public Func<BuildingObj, Job> jobcreate;
    public Type jobType;
    public Job job;
    public override void Update()
    {
        double prodSum = maxSum;//生产力能生产的数目
        // 计算最小的生产力比例
        foreach (var edge in trans.edge.tras)
        {
            prodSum = Math.Min(prodSum, ((double)productivity[edge.Key]) / ((double)edge.Value));
        }
        int sourceMax = int.MaxValue;//能生产商品的数目
        foreach (var source in trans.from.source)
        {
            sourceMax = Math.Min(sourceMax, from.Get(source.Item1) / source.Item2);
        }
        // 移除生产过程中的原材料
        foreach (var source in trans.from.source)
        {
            int sumValue = source.Item2 * sourceMax;
            from.Remove(source.Item1, sumValue);
        }

        // 减少生产力
        foreach (var edge in trans.edge.tras)
        {
            productivity[edge.Key] -= (int)(prodSum * edge.Value);
        }
        piplineList[0] = sourceMax;//剩余产品的数量
                                   // 循环处理每个传输阶段
        for (int i = piplineList.Count - 1; i >= 1; i--)
        {
            var val = piplineList[i];
            var temp = Math.Min(prodSum, val);
            prodSum -= temp;
            piplineList[i] -= temp;
            var createSum = ((int)Math.Ceiling(val) - (int)Math.Ceiling(piplineList[i]));
            if (i == piplineList.Count - 1)//最后一个
            {
                to.Add(trans.to.Item1, createSum * trans.to.Item2);
            }
            else
            {
                piplineList[i + 1] += createSum;
            }
        }
    }
    public override void Init(BuildingObj building)//初始化建筑
    {
        base.Init(building);
        wasterTimes = trans.wasterTimes;
        piplineList = new List<double>(wasterTimes);
        from = building.resource;
        to = building.goodsRes;
        productivity = new Productivity(building);
        job = (Job)Activator.CreateInstance(jobType, building);
        building.InitJob(job);
    }
    public GenGoodsPipline(Trans trans,Type job)
    {
        jobType = job;
    }
}

///// <summary>
///// 进口商品的管线
///// </summary>
//public class CarrayPipline : Pipline
//{
//    /// <summary>
//    /// 所选择的最优路径
//    /// </summary>
//    public Dictionary<GoodsEnum, GoodPath> goods2Path;
//    /// <summary>
//    /// 所有资源的数目
//    /// </summary>
//    public Dictionary<GoodsEnum, ALLOrder> allOrders;
//    /// <summary>
//    /// 拥有多少的商品
//    /// </summary>
//    public Dictionary<GoodsEnum, int> inputsSum;
//    /// <summary>
//    /// 资源的数目
//    /// </summary>
//    public int resourceSum = 1;
//    /// <summary>
//    /// 等比输入资源
//    /// </summary>
//	public override void Update()
//    {
//        foreach (var goodsPath in goods2Path)
//        {
//            int varSource = Math.Max(resourceSum * inputsSum[goodsPath.Key] - allOrders[goodsPath.Key].get(), 0);//需要请求的总数
//            if (varSource > 0)
//                RequestRes(goodsPath.Key, goodsPath.Value.from, varSource, goodsPath.Value.cost, goodsPath.Value.govcost, goodsPath.Value.wasterTime);//去请求资源
//        }
//    }
//    /// <summary>
//    /// 商品与需求的数目,与每一个的价格
//    /// </summary>
//    /// <param name="goods"></param>
//    /// <param name="sum"></param>
//    public void RequestRes(GoodsEnum goodsEnum, BuildingObj building, int sum, int cost, int govCost, int time)
//    {
//        var maxNum = Math.Min(sum, belong.money.money / cost);//请求的资源数目
//        foreach (var tran in trans.edge.tras)
//        {
//            var remain = productivity[tran.Key] / tran.Value;
//            maxNum = Mathf.Min(maxNum, remain);
//        }
//        var goods = GoodsGen.GetGoodsObj(goodsEnum);
//        goods.sum = maxNum;
//        this.Execute(new RequestGoodsCommand(building, this.belong, goods, time, cost, govCost));
//    }
//    public CarrayPipline(BuildingObj building, Resource from, Resource to, Trans trans, Productivity productivity)
//    {
//        this.belong = building;
//        this.from = from;
//        this.to = to;
//        this.trans = trans;
//        this.productivity = productivity;
//        goods2Path = new Dictionary<GoodsEnum, GoodPath>();
//        inputsSum = new Dictionary<GoodsEnum, int>();//一系列的商品
//        allOrders = new Dictionary<GoodsEnum, ALLOrder>();
//        foreach (var tran in trans.from.source)
//        {
//            inputsSum.Add(tran.Item1, tran.Item2);
//            allOrders.Add(tran.Item1, new ALLOrder(belong.resource, tran.Item1));
//        }
//    }
//    /// <summary>
//    /// 更新一系列的进口资源,然后更新基础成本
//    /// </summary>
//    /// <param name="goodsEnum"></param>
//    public void UpdateAllResource()
//    {
//        goods2Path.Clear();
//        foreach (var goods in belong.inputGoods)//选择最优路线
//        {
//            UpdateResource(goods);
//        }
//    }
//    public void UpdateResource(GoodsEnum goodsEnum)
//    {
//        var obj = GoodsGen.GetGoodsObj(goodsEnum);
//        var res = GameArchitect.get.economicSystem.GetGoods(obj, belong.scene);//获得原料中的最小的获取成本
//        var result = res.FindMinElement(e => { return e.cost; });//获得所有成本中最小的那个
//        if (result != null)
//        {
//            result.wasterTime = Math.Max(result.wasterTime, 1);
//            goods2Path.Add(goodsEnum, result);//结果与资源的合集
//        }
//    }
//}


public enum OrderState
{
    hasFinish,//订单已经结束
    broken,//订单出现问题
    inProcess,//正在进行
}

/// <summary>
/// 订单需求
/// </summary>
public class Order
{
    public int id;
    /// <summary>
    /// 需求的时间
    /// </summary>
    public int Day;
    /// <summary>
    /// 商品的信息
    /// </summary>
    public GoodsEnum goods;
    public int price;
    public int sum;
    public int remainsum;
    public int remainprice;
    public OrderState state;
    public Order parent;//父订单
    public Dictionary<GoodsEnum, Order> orders;//一系列的子订单
    public Order(int day, GoodsEnum goodsEnum, int cost, int sum)
    {
        orders = new Dictionary<GoodsEnum, Order>();
        Day = day;
        goods = goodsEnum;
        price = cost;
        remainprice = cost;
        this.sum = sum;
        remainsum = sum;
        state = OrderState.hasFinish;
    }
}


/// <summary>
/// 订单管理器
/// </summary>
public class OrderPipline:Pipline
{
    /// <summary>
    /// 订单列表
    /// </summary>
    public List<Order> orderTree;
    public BuildingObj building;
    /// <summary>
    /// 我要生产某个商品,我要找谁
    /// </summary>
    /// 
    public class OrderInf
    {
        public class OrderDetail
        {
            public int price;//它提供的价格
            public BuildingObj building;//所属的建筑
            public int Day;
            public OrderDetail(int price, BuildingObj building, int day)
            {
                this.price = price;
                this.building = building;
                Day = day;
            }
        }
        public Dictionary<GoodsEnum, OrderDetail> orderInf=new Dictionary<GoodsEnum, OrderDetail>();
    }
    //一个订单对应的订单信息
    public Dictionary<int,OrderInf> orderMemory;
    public OrderPipline(BuildingObj building) 
    {
        this.building = building;
        orderMemory = new Dictionary<int,OrderInf>();
    }
    /// <summary>
    /// 用来处理接到的订单
    /// </summary>
    /// <param name="order"></param>
    public void BOrder(Order order)
    {
        //自己能够处理
        orderTree.Add(order);
        var inf=orderMemory[order.id];
        if(building.getIn()!=null)//自己就可以生产
        foreach(var item in inf.orderInf)
        {
            var ins = building.GetPipline<GenGoodsPipline>().trans.from.source.Find(e=> { return e.Item1 == item.Key; }).Item2;
            var b=item.Value.building;
            var tempOrder = new Order(order.Day,item.Key,item.Value.price,order.sum*ins);
            b.GetPipline<OrderPipline>().BOrder(tempOrder);  
        }
    }
    /// <summary>
    /// 作为甲方发送订单
    /// </summary>
    /// <param name="order"></param>
    public void AOrder(Order order)
    {
        //添加到订单树就好
        orderTree.Add(order);
    }
    /// <summary>
    /// 获取能购买的最低成本价格
    /// 价格,时间
    /// </summary>
    /// <returns></returns>
    public Pair<int,int> GetMinGoodsCost(Order order)
    {
        int sum = order.sum;
        var trans = building.GenGoodsPipline.trans;//商品
        int sumCost = 0;
        int sumDay=0;
        //商品没有进口价格
        if (building.getIn() != null)
            foreach (var ins in trans.from.source)
            {
                var ret = GameArchitect.get.economicSystem.GetMinGoodsCost(ins.Item1,ins.Item2*sum,order.Day);
                var retC = ret.Item3;
                var retB=ret.Item2;
                var retA=ret.Item1;
                orderMemory[order.id].orderInf[ins.Item1]=new OrderInf.OrderDetail(retA,retB,retC);
                sumDay = Math.Max(sumDay,retC);
                sumCost += retA;
            }
        sumCost += 10;
        return new Pair<int, int>(sumCost,sumDay);
    }
    /// <summary>
    /// 单价,总数,如果有能力接则返回真
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Tuple<bool, int,int> PlanOrder(Order order)
    {
        int time = order.Day;
        int money = order.price;
        int sum = order.sum;
        orderMemory.Add(order.id, new OrderInf());
        var ret = GetMinGoodsCost(order);//获取订单的最小价格
        return new Tuple<bool, int, int>((GetAllGoodsSum(time) - GetOrderGoodsSum(time)) >= sum && ret.Item1 <= money&&ret.Item2<=time, ret.Item1,ret.Item2);
    }
    /// <summary>
    /// 获取一段时间的总产能数量
    /// </summary>
    /// <returns></returns>
    public int GetAllGoodsSum(int time)
    {
        return 10;
        ////通过预测来解决.
        ////理论每天最大产量
        ////每天能够
        //// 理论每天最大产量（可配置，或由工厂设定）
        //int maxDailyProduction = maxPipSum;

        //// 实际每天可用产能，受限于资源、设备状态等（可根据具体需求调整）
        //int actualDailyProduction = GetAvailableDailyProduction(time);

        //// 计算理论总产能
        //int theoreticalTotalProduction = maxDailyProduction * time;

        //// 实际总产能，受限于实际每天的可用产能
        //int actualTotalProduction = actualDailyProduction * time;

        //// 返回理论最大总产能和实际总产能中的较小值
        //return Math.Min(theoreticalTotalProduction, actualTotalProduction);
    }
    public int GetAvailableDailyProduction(int time)
    {
        return 10;
    }
    public int GetOrderGoodsSum(int time)
    {
        return 10;
    }

}

/// <summary>
/// 管线,用来配置建筑的Update函数,Job函数,行为函数
/// </summary>
public class Pipline
{
    public BuildingObj belong;
    /// <summary>
    /// 建筑
    /// </summary>
    public virtual void Update()
    {

    }
    public virtual void Init(BuildingObj building)
    {
        belong = building;
        building.pipLineManager.pairs.Add(this);
    }
}


