using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Goods
{
    public Int sum=0;
    public Float cost=0;
}
public class BuildingState:BaseState
{
    public Float money;//总的资金
    public Dictionary<GoodsEnum, Goods> goodslist;
    public BuildingState():base()
    {
        goodslist = new Dictionary<GoodsEnum, Goods>();
        money = 0;
        Init();
    }
    public override void Init()
    {
        base.Init();
        money = 0;
        foreach(var item in goodslist)
        {
            goodslist[item.Key] = new Goods();
        }
    }
}

public class BuildingEc:EconomicInf
{

}

/// <summary>
/// 建筑对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class BuildingObj<T,F> :BaseObj<T, F>,ISendEvent,ISendCommand,IRegisterEvent 
where T:BuildingState,new()
where F:BuildingEc,new()
{
    /// <summary>
    /// 建筑对象
    /// </summary>
    public BuildingObj():base()
    {

    }
    public override T Update(T input, T output)
    {
        output.Init();
        output.money=input.money;
        foreach(var x in input.goodslist)
        {
            output.goodslist[x.Key].sum = input.goodslist[x.Key].sum;
            output.goodslist[x.Key].cost = input.goodslist[x.Key].cost;
        }
        return output;
    }
    /// <summary>
    /// 获取给定生产力的最小花费
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public GoodsOrder GetProdMoney(Tuple<ProdEnum, Int>[] prods,Int num)
    {
        return null;
    }
    /// <summary>
    /// 获取给定商品的最小花费
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public GoodsOrder GetGoodsMoney(Tuple<GoodsEnum, Int>[] prods, Int num)
    {
        return null;
    }
}

public class GoodsBuildingState:BuildingState
{
    public GoodsStateMeta goodsStateMeta;
    /// <summary>
    /// 循环序列
    /// </summary>
    public CircularQueue<Float> generateList;
    public Float prodSate;
    public GoodsBuildingState():base()
    {
        goodsStateMeta = (GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>();
        generateList = new CircularQueue<Float>(10);
        foreach (var item in goodsStateMeta.inputs)
        {
            goodslist[item.Item1] = new Goods();
        }
        goodslist[goodsStateMeta.output.Item1] = new Goods();
    }
    public override void Init()
    {
        base.Init();
        generateList.Clear();
        prodSate = 0;
    }
}

public class GoodsBuildingEc: BuildingEc
{

}

public class GoodsBuildingObj:BuildingObj<GoodsBuildingState, GoodsBuildingEc>,ICanReceiveNormalGoodsOrder, INormalGoodsOrderUser, IProdOrderUser
{

    /// <summary>
    /// 根据生产力更新商品流水线
    /// </summary>
    /// <param name="input"></param>
    /// <param name="outpu"></param>
    public void GeneratePipline(GoodsBuildingState state)
    {
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
                    node-=needRed;
                    int allCreate = Mathf.CeilToInt(tempNow)- Mathf.CeilToInt(node);
                    // 检查该商品是否完成生产，如果完成则加入到 goodslist 中
                    foreach (var input in ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs)
                    {
                        // 更新商品数量
                        state.goodslist[input.Item1].sum += input.Item2 * allCreate;
                    }
                }
                else // 处理其他节点（非队尾）
                {
                    // 将商品向前推进（模拟流水线）
                    var nextNode = state.generateList.FindTail(i - 1); // 查找上一个节点
                    nextNode = node;
                    var needRed=Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    nextNode +=needRed;
                    node -= needRed;
                }
            }
        }
    }
    /// <summary>
    /// 将商品加到管线的队尾中
    /// </summary>
    /// <param name="state"></param>
    /// <param name="val"></param>
    public void AddPipline(GoodsBuildingState state,int val)
    {
        state.generateList.FindFront(0);
    }

    public override GoodsBuildingState Update(GoodsBuildingState input, GoodsBuildingState output)
    {
        base.Update(input, output);//更新状态
        var prodOrderInf = GetProdMoney(((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10);
        if (prodOrderInf.cost <= output.money)//可以购买则继续
        {
            EconmicSystem.Instance.Buy(this,output,prodOrderInf);//购买生产力
        }
        var goodsOrderInf = GetGoodsMoney(((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10);
        if (goodsOrderInf.cost<= output.money)//购买商品
        {
            EconmicSystem.Instance.Buy(this, output, goodsOrderInf);//购买生产力
        }
        ///更新流水线
        GeneratePipline(output);
        return output;
    }

    public void addMoney(BaseState state, Float money)
    {
        (state as GoodsBuildingState).money += money;
    }

    public void reduceMoney(BaseState state, Float money)
    {
        (state as GoodsBuildingState).money -= money;
    }
    /// <summary>
    /// 修改当前的状况,根据订单
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public void receiveMoney(Float money, NormalGoodsInf goodsInf)
    {
        var now=getNow();//要修改的当前情况
        //var normalGoodsInf=goodsInf as NormalGoodsInf;
        now.goodslist[goodsInf.goodsEnum].sum -= goodsInf.sum;
        addMoney(now, goodsInf.goodsPrice);
    }
    //注册接收的订单
    public void registerReveiveGoodsOrder()
    {

    }

    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state)
    {
        return ((GoodsBuildingState)state).goodslist;
    }

    public Float GetProdState(BaseState state)
    {
        return ((GoodsBuildingState)state).prodSate;
    }

    public CircularQueue<Float> GetPipline(BaseState state)
    {
        return ((GoodsBuildingState)state).generateList;
    }
}