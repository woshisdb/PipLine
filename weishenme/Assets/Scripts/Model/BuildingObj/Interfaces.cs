using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可以下订单
/// </summary>
public interface IMarketUser
{
    /// <summary>
    /// 所属的NPC
    /// </summary>
    /// <returns></returns>
    public NpcObj GetNpc();
    /// <summary>
    /// 添加钱
    /// </summary>
    /// <param name="money"></param>
    public void addMoney(Float money);
    /// <summary>
    /// 减少钱
    /// </summary>
    /// <param name="money"></param>
    public void reduceMoney(Float money);

    public Float getMoney();
}

/// <summary>
/// 接收生产力订单
/// </summary>
public interface INeedWork : IMarketUser
{
    /// <summary>
    /// 注册监听来请求一系列的工作
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public NeedWork[] RegisterNeedWork();
    /// <summary>
    /// 不愿意接收工作协议
    /// </summary>
    public NeedWork[] UnRegisterNeedWork();

    /// <summary>
    /// 当前位置
    /// </summary>
    /// <returns></returns>
    public SceneObj nowPos();

    public float GetNeedWorkRate(SendWork sendWork);
}


/// <summary>
/// 可以请求生产力的订单
/// </summary>
public interface ISendWork : IMarketUser
{
    /// <summary>
    /// 获取完生产力后的处理
    /// </summary>
    /// <param name="state"></param>
    /// <param name="prods"></param>
    public void GetProdProcess(INeedWork worker);
    /// <summary>
    /// 注册一系列生产力订单
    /// </summary>
    /// <returns></returns>
    public SendWork[] RegisterSendWork();
    /// <summary>
    /// 取消注册一系列生产力订单
    /// </summary>
    /// <returns></returns>
    public SendWork[] UnRegisterSendWork();
    /// <summary>
    /// 目标位置
    /// </summary>
    /// <returns></returns>
    public SceneObj aimPos();
    public float GetSendWorkRate(NeedWork needWork);
}

/// <summary>
/// 可以雇佣自己
/// </summary>
public interface ICanEmploySelf:IMarketUser
{
    /// <summary>
    /// 雇佣自己
    /// </summary>
    public void EmploySelf();
    /// <summary>
    /// 不雇佣自己
    /// </summary>
    public void UnEmploySelf();
}

/// <summary>
/// 可以请求商品的订单
/// </summary>
public interface INeedGoods : IMarketUser
{
    /// <summary>
    /// 获取商品列表,用来加进来
    /// </summary>
    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum);
    public NeedGoods[] RegisterNeedGoods();
    public NeedGoods[] UnRegisterNeedGoods();
    public SceneObj aimPos();

    public float NeedGoodsSatifyRate(SendGoods sendGoods);
}

/// <summary>
/// 可以接收请求商品的订单
/// </summary>
public interface ISendGoods : IMarketUser
{
    /// <summary>
    /// 接收商品的订单
    /// </summary>
    public SendGoods[] RegisterSendGoods();
    /// <summary>
    /// 取消接收商品订单
    /// </summary>
    public SendGoods[] UnRegisterSendGoods();
    public SceneObj nowPos();

    public float SendGoodsSatifyRate(NeedGoods goods);
}

/// <summary>
/// 可以请求转移东西
/// </summary>
public interface ISendTransGoods : IMarketUser
{
    /// <summary>
    /// 接收商品的订单
    /// </summary>
    public void RegisterRequestTransGoods();
    /// <summary>
    /// 取消接收商品订单
    /// </summary>
    public void UnRegisterReveiveGoodsOrder();
}

/// <summary>
/// 可以处理转移事物
/// </summary>
public interface IReceiveTransGoods : IMarketUser
{
    /// <summary>
    /// 获取商品列表,用来加进来
    /// </summary>
    public void TransGoodsProcess(BaseState state, GoodsObj goods);
    public void RegisterProcessTransThings();
    public void UnRegisterProcessTransThings();
    /// <summary>
    /// 对人进行转移
    /// </summary>
    /// <param name="state"></param>
    /// <param name="npcState"></param>
    public void OneDayTransNpc(BaseState state,NpcState npcState);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="npcState"></param>
    public void MoreDayTransNpc(BaseState state,NpcState npcState);
}


/// <summary>
/// 生产资料
/// </summary>
public interface IBuilding
{
    /// <summary>
    /// 调用这个函数来最大化自己的收益,通过请求订单,调整工资和工作人数
    /// </summary>
    public abstract void MaxMoney();
}





/// <summary>
/// 普通的工厂,能够请求生产力,然后进口商品,出口商品
/// </summary>
public interface EmploymentFactory : IBuilding,ISendWork,ISendGoods,INeedGoods
{

}
/// <summary>
/// 原材料的工厂,能够请求生产力,然后,出口商品
/// </summary>
public interface SourceEmploymentFactory : IBuilding,ISendGoods,ISendWork
{

}
/// <summary>
/// 生产最终商品的工厂
/// </summary>
public interface FinalEmploymentFactory: IBuilding, ISendWork, ISendGoods, INeedGoods
{

}
/// <summary>
/// 市场,用来向NPC销售商品
/// </summary>
public interface MarketFactory:IBuilding, ISendWork, ISendGoods, INeedGoods
{

}
/// <summary>
/// 转移商品和人的工厂
/// </summary>
public interface TransGoodsFactory : IBuilding, ICanEmploySelf
{

}

public interface INpc:INeedWork,INeedGoods
{

}