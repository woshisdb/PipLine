using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可以接收工作
/// </summary>
public class ReceiveWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 对象
    /// </summary>
    public IReceiveWork obj;
    /// <summary>
    /// 最少给我的钱
    /// </summary>
    public Func<Float> minPrice;
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
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
    public float maxMoney;
}
/// <summary>
/// 接收商品
/// </summary>
public class ReceiveGoods
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
    /// 最少给我的钱
    /// </summary>
    public Func<Float> minPrice;
    public float minMoney;
    /// <summary>
    /// 接收对象
    /// </summary>
    public IReceiveGoods obj;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class SendGoods
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
    public ISendGoods obj;
    /// <summary>
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
    public float maxMoney;
}
