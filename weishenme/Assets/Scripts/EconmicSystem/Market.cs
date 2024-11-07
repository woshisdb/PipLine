using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market
{
    public Dictionary<SceneObj, MarketScene> Markets = new Dictionary<SceneObj, MarketScene>();
}
/// <summary>
/// 市场场景
/// </summary>
public class MarketScene
{
    /// <summary>
    /// 一系列请求商品的协议
    /// </summary>
    public List<SendGoods> requestGoods;
    /// <summary>
    /// 一系列请求工作的协议
    /// </summary>
    public List<SendWork> requestWork;
    /// <summary>
    /// 请求工作的NPC
    /// </summary>
    public List<ReceiveWork> requestWorkNpc;
    /// <summary>
    /// 可以接收商品的协议
    /// </summary>
    public List<ReceiveGoods> canReceiveGoodsContracts;
}