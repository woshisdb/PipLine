using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可以提供工作
/// </summary>
public class NeedWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 对象
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// 期望的最低工资
    /// </summary>
    public float minMoney;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class SendWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 发送方
    /// </summary>
    public ISendWork obj;
    /// <summary>
    /// 最多的给的工资
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// 是否能满足工作
    /// </summary>
    public Func<NeedWork, bool> isSatify;
    /// <summary>
    /// 满足度
    /// </summary>
    public Func<NeedWork, float> satifyRate;
}
/// <summary>
/// 接收商品
/// </summary>
public class SendGoods
{
    /// <summary>
    /// 商品
    /// </summary>
    public GoodsObj goods;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 想要到手的钱
    /// </summary>
    public float minMoney;
    /// <summary>
    /// 接收对象
    /// </summary>
    public ISendGoods obj;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class NeedGoods
{
    /// <summary>
    /// 商品
    /// </summary>
    public GoodsObj[] goods;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 传输商品
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
    /// <summary>
    /// 最大的钱
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// 是否能满足
    /// </summary>
    public Func<SendGoods, bool> isSatify;
    /// <summary>
    /// 是否能满足
    /// </summary>
    public Func<SendGoods, bool> satifyRate;
}
