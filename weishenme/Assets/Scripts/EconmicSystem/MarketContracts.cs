using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// 循环请求商品协议
///// </summary>
//public class RequestGoodsContract: CircleContract
//{
//    /// <summary>
//    /// 开始时间
//    /// </summary>
//    public int beginTime;
//    /// <summary>
//    /// 循环周期
//    /// </summary>
//    public int circle;
//    /// <summary>
//    /// 需要花费的钱
//    /// </summary> 
//    public int dealMoney;
//    /// <summary>
//    /// 需要获取商品的信息
//    /// </summary>
//    public GoodsObj goodsInf;
//    public override void ABreak()
//    {
//    }
//    public override void BBreak()
//    {
//        //把钱退回去
//    }
//    public override bool Condition()
//    {
//        return true;
//    }
//    public override void DayUpdate()
//    {
//    }
//    public override int EndCircle()
//    {

//    }
//    public override void Init(NpcState a, NpcState b, NpcState ruleNpc)
//    {
//        base.Init(a, b, ruleNpc);
//        beginTime = TimeSystem.Instance.nowDay;//当前的天数
//    }
//}
///// <summary>
///// 可以接收商品请求的建筑信息
///// </summary>
//public class CanReceiveGoodsContract
//{
//    /// <summary>
//    /// 商品的生产类型
//    /// </summary>
//    public Func<GoodsEnum> goods;
//    /// <summary>
//    /// 每个月可以提供的最大商品的数目
//    /// </summary>
//    public Func<int> maxSum;
//    /// <summary>
//    /// 当前商品的最少可获得价格
//    /// </summary>
//    public Func<float> minPrice;
//}

/// <summary>
/// 可以接收工作
/// </summary>
public class ReceiveWork
{
    /// <summary>
    /// npc
    /// </summary>
    public IReceiveWork receiver;
    /// <summary>
    /// 最少给我的钱
    /// </summary>
    public Func<Float> minPrice;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class SendWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<BuildingObj> building;
    public ISendWork obj;
    /// <summary>
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
}
/// <summary>
/// 接收商品
/// </summary>
public class ReceiveGoods
{
    /// <summary>
    /// npc
    /// </summary>
    public Func<NpcObj> npc;
    /// <summary>
    /// 最少给我的钱
    /// </summary>
    public Func<Float> minPrice;
    public IReceiveGoods obj;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class SendGoods
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<BuildingObj> building;
    /// <summary>
    /// 传输商品
    /// </summary>
    public ISendGoods obj;

    /// <summary>
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
}