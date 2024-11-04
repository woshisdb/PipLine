using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可以下订单
/// </summary>
public interface IContractUser
{
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
}

/// <summary>
/// 可以请求生产力的订单
/// </summary>
public interface ISendWorkContract : IContractUser
{
    /// <summary>
    /// 招聘人的个数
    /// </summary>
    /// <returns></returns>
    public Int getProdSum();

    public void setProdSum(Int prodSum);
    /// <summary>
    /// 获取完生产力后的处理
    /// </summary>
    /// <param name="state"></param>
    /// <param name="prods"></param>
    public void GetProdProcess(NpcObj npc);
    /// <summary>
    /// 注册一系列生产力订单
    /// </summary>
    /// <returns></returns>
    public void RegisterRequestWorkContract();
    /// <summary>
    /// 取消注册一系列生产力订单
    /// </summary>
    /// <returns></returns>
    public void UnRegisterRequestWorkContract();
    /// <summary>
    /// 目标位置
    /// </summary>
    /// <returns></returns>
    public SceneObj aimPos();
}

/// <summary>
/// 接收生产力订单
/// </summary>
public interface ICanReceiveWorkContract : IContractUser
{
    /// <summary>
    /// 注册监听来请求一系列的工作
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public void RegisterReceiveWorkContract();
    /// <summary>
    /// 不愿意接收工作协议
    /// </summary>
    public void UnRegisterReceiveWorkContract();

    /// <summary>
    /// 选择一系列工作中的后续处理
    /// </summary>
    /// <param name="works"></param>
    /// <returns></returns>
    public void DecisionBestWork(WorkContract[] works);
    /// <summary>
    /// 当前位置
    /// </summary>
    /// <returns></returns>
    public SceneObj nowPos();
}

/// <summary>
/// 可以雇佣自己
/// </summary>
public interface ICanEmploySelf:IContractUser
{
    public NpcObj GetNpc();
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
/// 可以接收请求商品的订单
/// </summary>
public interface ICanReceiveGoodsContract : IContractUser
{
    /// <summary>
    /// 接收商品的订单
    /// </summary>
    public void RegisterReveiveGoodsOrder();
    /// <summary>
    /// 取消接收商品订单
    /// </summary>
    public void UnRegisterReveiveGoodsOrder();
}

/// <summary>
/// 可以请求商品的订单
/// </summary>
public interface ISendGoodsContract : IContractUser
{
    /// <summary>
    /// 获取商品列表,用来加进来
    /// </summary>
    public void GetGoodsProcess(BaseState state,GoodsEnum goodsEnum,int sum);

    public void RegisterRequestGoodsContract();
    public void UnRegisterRequestGoodsContract();
}


/// <summary>
/// 可以请求转移东西
/// </summary>
public interface ICanRequestTransGoods : IContractUser
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
public interface ICanProcessTransGoods : IContractUser
{
    /// <summary>
    /// 获取商品列表,用来加进来
    /// </summary>
    public void TransGoodsProcess(BaseState state, Goods goods);

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
/// 是否可以用来申请收入
/// </summary>
public interface ICanRaiseMoney
{
    /// <summary>
    /// 预测某个人有多少的收入
    /// </summary>
    /// <returns></returns>
    public float Predicate(NpcObj npc);
    /// <summary>
    /// 金钱的循环周期
    /// </summary>
    /// <returns></returns>
    public int MoneyCircle();
}

/// <summary>
/// 是否可以生产产品
/// </summary>
public interface ICanGeneratePipline
{
    public void GeneratePipline(GoodsBuildingState state);
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
public interface EmploymentFactory : IBuilding,ISendWorkContract, ISendGoodsContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}


/// <summary>
/// 原材料的工厂,能够请求生产力,然后,出口商品
/// </summary>
public interface SourceEmploymentFactory : IBuilding,ISendWorkContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}

/// <summary>
/// 转移商品和人的工厂
/// </summary>
public interface TransGoodsFactory : IBuilding,ISendWorkContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}

