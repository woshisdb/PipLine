using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsBuildingState : BuildingState
{
    /// <summary>
    /// 商品的生产信息
    /// </summary>
    public GoodsStateMeta goodsStateMeta;
    /// <summary>
    /// 循环序列
    /// </summary>
    public CircularQueue<Float> generateList;
    /// <summary>
    /// 生产力的数目
    /// </summary>
    public Float prodSate;
    /// <summary>
    /// 商品管理器
    /// </summary>
    public GoodsManager goodsManager;

    public Dictionary<GoodsEnum, NeedGoods> needs;
    public Dictionary<GoodsEnum, SendGoods> sends;
    public SendWork sendWork;//请求的一系列工作
    public GoodsBuildingState(GoodsBuildingObj obj) : base(obj)
    {
        goodsStateMeta = (GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>();
        goodsManager = new GoodsManager(goodsStateMeta.GetGoods());
        generateList = new CircularQueue<Float>(10);
        foreach (var item in goodsStateMeta.inputs)
        {
            goodslist[item.Item1] = 0;
        }
        goodslist[goodsStateMeta.output.Item1] = 0;
    }
    public override void Init()
    {
        base.Init();
        generateList.Clear();
        prodSate = 0;
    }
}
public class GoodsBuildingEc : BuildingEc
{
    public GoodsBuildingEc(BaseObj obj) : base(obj)
    {
    }
}
/// <summary>
/// 生产商品的建筑
/// </summary>
public class GoodsBuildingObj : BuildingObj, EmploymentFactory
{
    public new GoodsBuildingState now{get{return (GoodsBuildingState)getNow();}}
    public GoodsBuildingObj()
    {
        now.needs = new Dictionary<GoodsEnum, NeedGoods>();
        var state = (GoodsBuildingState)now;
        var inputs=state.goodsStateMeta.inputs;
        var output = state.goodsStateMeta.output;
        now.sends = new Dictionary<GoodsEnum, SendGoods>();
        foreach(var item in inputs)
        {
            var needGoods= new NeedGoods();
            needGoods.goods=item.Item1;
            needGoods.obj = this;
            now.needs[item.Item1] = needGoods;
        }
        var sendGoods=new SendGoods();
        sendGoods.goods = output.Item1;
        sendGoods.obj = this;
        now.sends[output.Item1] = sendGoods;
    }

    public void addMoney(Float money)
    {
        throw new NotImplementedException();
    }

    public SceneObj aimPos()
    {
        throw new NotImplementedException();
    }

    public override void BefThink()
    {
        base.BefThink();
        
    }
    /// <summary>
    /// 根据生产力更新商品流水线
    /// </summary>
    /// <param name="input"></param>
    /// <param name="outpu"></param>
    public void GeneratePipline()
    {
        var state = (GoodsBuildingState)now;
        // 遍历流水线，从队尾开始
        for (int i = 0; i < state.generateList.Size(); i++)
        {
            if (state.prodSate >= 0)
            {
                // 获取当前流水线节点，FindTail(i) 从队尾向头部查找第 i 个元素
                var node = state.generateList.FindTail(i);

                // 判断是否为最后一个节点
                if (i == 0) // 处理最后一个节点（队尾）
                {
                    var needRed = Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    var tempNow = node;
                    node -= needRed;
                    int allCreate = Mathf.CeilToInt(tempNow) - Mathf.CeilToInt(node);
                    // 检查该商品是否完成生产，如果完成则加入到 goodslist 中
                    foreach (var input in ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs)
                    {
                        // 更新商品数量
                        state.goodslist[input.Item1] += input.Item2 * allCreate;
                    }
                }
                else // 处理其他节点（非队尾）
                {
                    // 将商品向前推进（模拟流水线）
                    var nextNode = state.generateList.FindTail(i - 1); // 查找上一个节点
                    nextNode = node;
                    var needRed = Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    nextNode += needRed;
                    node -= needRed;
                }
            }
        }
    }

    public void GetGoodsProcess(BaseState state, GoodsEnum goodsEnum, int sum)
    {
        throw new NotImplementedException();
    }

    public NpcObj GetNpc()
    {
        throw new NotImplementedException();
    }

    public void GetProdProcess(INeedWork worker)
    {
        throw new NotImplementedException();
    }

    public void MaxMoney()
    {
        throw new NotImplementedException();
    }

    public SceneObj nowPos()
    {
        throw new NotImplementedException();
    }

    public void reduceMoney(Float money)
    {
        throw new NotImplementedException();
    }

    public SendGoods[] RegisterReceiveGoods()
    {
        throw new NotImplementedException();
    }

    public NeedGoods[] RegisterSendGoods()
    {
        throw new NotImplementedException();
    }

    public SendWork[] RegisterSendWork()
    {
        throw new NotImplementedException();
    }

    public SendGoods[] UnRegisterReceiveGoods()
    {
        throw new NotImplementedException();
    }

    public NeedGoods[] UnRegisterSendGoods()
    {
        throw new NotImplementedException();
    }

    public SendWork[] UnRegisterSendWork()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 根据状态更新
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override void Update()
    {
        GeneratePipline();
    }
}

///// <summary>
///// 商品加工厂,能够进口商品,出口商品,进口生产力
///// </summary>
//public class ProcessFactoryObj : BuildingObj, EmploymentFactory
//{
//    public BuildingState now { get { return (BuildingState)getNow(); } }
//    /// <summary>
//    /// 根据生产力更新商品流水线
//    /// </summary>
//    /// <param name="input"></param>
//    /// <param name="outpu"></param>
//    public void GeneratePipline(GoodsBuildingState state)
//    {
//        // 遍历流水线，从队尾开始
//        for (int i = 0; i < state.generateList.Size(); i++)
//        {
//            if (state.prodSate >= 0)
//            {
//                // 获取当前流水线节点，FindTail(i) 从队尾向头部查找第 i 个元素
//                var node = state.generateList.FindTail(i);

//                // 判断是否为最后一个节点
//                if (i == 0) // 处理最后一个节点（队尾）
//                {
//                    var needRed = Math.Min(state.prodSate, node);
//                    state.prodSate -= needRed;
//                    var tempNow = node;
//                    node -= needRed;
//                    int allCreate = Mathf.CeilToInt(tempNow) - Mathf.CeilToInt(node);
//                    // 检查该商品是否完成生产，如果完成则加入到 goodslist 中
//                    foreach (var input in ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs)
//                    {
//                        // 更新商品数量
//                        state.goodslist[input.Item1].sum += input.Item2 * allCreate;
//                    }
//                }
//                else // 处理其他节点（非队尾）
//                {
//                    // 将商品向前推进（模拟流水线）
//                    var nextNode = state.generateList.FindTail(i - 1); // 查找上一个节点
//                    nextNode = node;
//                    var needRed = Math.Min(state.prodSate, node);
//                    state.prodSate -= needRed;
//                    nextNode += needRed;
//                    node -= needRed;
//                }
//            }
//        }
//    }
//    /// <summary>
//    /// 添加生产力
//    /// </summary>
//    /// <param name="prods"></param>
//    /// <param name="state"></param>
//    public void AddProds(Tuple<ProdEnum, Int>[] prods,BuildingState state)
//    {

//    }

//    /// <summary>
//    /// 将商品加到管线的队尾中
//    /// </summary>
//    /// <param name="state"></param>
//    /// <param name="val"></param>
//    public void AddPipline(GoodsBuildingState state, int val)
//    {
//        state.generateList.FindFront(0);
//    }
//    /// <summary>
//    /// 根据状态更新
//    /// </summary>
//    /// <param name="input"></param>
//    /// <returns></returns>
//    public override void Update(BaseState input)
//    {
//        var npc = input as GoodsBuildingState;
//        var prodOrderInf = EconmicSystem.get.GetProdMoney(input,((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10);
//        if (prodOrderInf.cost <= npc.ecState.money)//可以购买则继续
//        {
//            EconmicSystem.Instance.Buy(this, input, prodOrderInf);//购买生产力
//        }
//        var goodsOrderInf = EconmicSystem.get.GetGoodsMoney(input,((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10);
//        if (goodsOrderInf.cost <= npc.ecState.money)//购买商品
//        {
//            EconmicSystem.Instance.Buy(this, input, goodsOrderInf);//购买生产力
//        }
//        ///更新流水线
//        GeneratePipline(input);
//    }
//    /// <summary>
//    /// 根据这一天的状态预测这一天之后的变化
//    /// </summary>
//    /// <param name="input"></param>
//    /// <param name="day"></param>
//    public override void Predict(BaseState input,int day)
//    {
//        var prodOrderInf = EconmicSystem.get.GetProdMoney(input, ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10,day,true);
//        if (prodOrderInf.cost <= input.money)//可以购买则继续
//        {
//            EconmicSystem.Instance.Buy(this, input, prodOrderInf);//购买生产力
//        }
//        var goodsOrderInf = EconmicSystem.get.GetGoodsMoney(input, ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10, day, true);
//        if (goodsOrderInf.cost <= input.money)//购买商品
//        {
//            EconmicSystem.Instance.Buy(this, input, goodsOrderInf);//购买生产力
//        }
//        ///更新流水线
//        GeneratePipline(input);
//    }
//    public void addMoney(BaseState state, Float money)
//    {
//        (state as GoodsBuildingState).money += money;
//    }

//    public void reduceMoney(BaseState state, Float money)
//    {
//        (state as GoodsBuildingState).money -= money;
//    }
//    /// <summary>
//    /// 修改当前的状况,根据订单
//    /// </summary>
//    /// <param name="money"></param>
//    /// <param name="goodsInf"></param>
//    public void receiveMoney(Float money, NormalGoodsInf goodsInf)
//    {
//        var now = getNow();//要修改的当前情况
//        //var normalGoodsInf=goodsInf as NormalGoodsInf;
//        now.goodslist[goodsInf.goodsEnum].sum -= goodsInf.sum;
//        addMoney(now, goodsInf.goodsPrice);
//    }
//    /// <summary>
//    /// 注册接收的订单
//    /// </summary>
//    public void registerReveiveGoodsOrder()
//    {

//    }

//    public Dictionary<GoodsEnum, GoodsObj> GetGoodsList(BaseState state)
//    {
//        return ((GoodsBuildingState)state).goodslist;
//    }

//    public Float GetProdState(BaseState state)
//    {
//        return ((GoodsBuildingState)state).prodSate;
//    }

//    public CircularQueue<Float> GetPipline(BaseState state)
//    {
//        return ((GoodsBuildingState)state).generateList;
//    }

//    public void unregisterReveiveGoodsOrder()
//    {
//        throw new NotImplementedException();
//    }
//}

///// <summary>
///// 商品原材料生产厂,只出口不进口
///// </summary>
//public class SourceEmploymentFactoryObj: BuildingObj, SourceEmploymentFactory
//{

//}

///// <summary>
///// 能够转移商品,人的建筑
///// </summary>
//public class TransBuildingObj:BuildingObj, TransGoodsFactory
//{

//}

