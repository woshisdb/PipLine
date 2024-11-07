using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matcher
{
    /// <summary>
    /// 匹配发送者和接收者的工作请求。
    /// </summary>
    /// <param name="sendWorks">发送工作请求的列表</param>
    /// <param name="receiveWorks">接收工作请求的列表</param>
    /// <returns>匹配到的工作请求对</returns>
    public List<(SendWork, ReceiveWork)> MatchWorks(List<SendWork> sendWorks, List<ReceiveWork> receiveWorks)
    {
        var matchedWorks = new List<(SendWork, ReceiveWork)>();
        foreach (var sender in sendWorks)
        {
            foreach (var receiver in receiveWorks)
            {
                // 检查是否满足位置和价格条件
                if (sender.scene() == receiver.scene() &&
                    sender.maxPrice() >= receiver.minPrice())
                {
                    matchedWorks.Add((sender, receiver));
                }
            }
        }
        return matchedWorks;
    }

    /// <summary>
    /// 匹配发送者和接收者的商品请求。
    /// </summary>
    /// <param name="sendGoodsList">发送商品请求的列表</param>
    /// <param name="receiveGoodsList">接收商品请求的列表</param>
    /// <returns>匹配到的商品请求对</returns>
    public List<(SendGoods, ReceiveGoods)> MatchGoods(List<SendGoods> sendGoodsList, List<ReceiveGoods> receiveGoodsList)
    {
        var matchedGoods = new List<(SendGoods, ReceiveGoods)>();
        foreach (var sender in sendGoodsList)
        {
            foreach (var receiver in receiveGoodsList)
            {
                // 检查是否满足位置和价格条件
                if (sender.scene() == receiver.scene() &&
                    sender.maxPrice() >= receiver.minPrice())
                {
                    matchedGoods.Add((sender, receiver));
                }
            }
        }
        return matchedGoods;
    }
}


/// <summary>
/// 市场
/// </summary>
public class Market
{
    /// <summary>
    /// 每个场景中请求与接收信息
    /// </summary>
    public Dictionary<SceneObj, MarketScene> Markets = new Dictionary<SceneObj, MarketScene>();
    /// <summary>
    /// 匹配一个合适的订单
    /// </summary>
    public void MatchOrder()
    {
        for()
        {

        }
    }
    /// <summary>
    /// 两个场景之间的距离和花钱
    /// </summary>
    /// <returns></returns>
    public Tuple<int,float> GoodsDistanceCost(SceneObj a,SceneObj b)
    {
        return new Tuple<int, float>(1,0);
    }
    /// <summary>
    /// NPC之间的距离和花钱
    /// </summary>
    /// <returns></returns>
    public Tuple<int, float> NpcDistanceCost(SceneObj a, SceneObj b)
    {
        return new Tuple<int, float>(1, 0);
    }
}



/// <summary>
/// 市场场景
/// </summary>
public class MarketScene
{
    /// <summary>
    /// 一系列请求商品的协议
    /// </summary>
    public List<SendGoods> sendGoods;
    /// <summary>
    /// 可以接收商品的协议
    /// </summary>
    public List<ReceiveGoods> receiveGoods;
    //****************************************************
    /// <summary>
    /// 一系列请求工作的协议
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// 请求工作的NPC
    /// </summary>
    public List<ReceiveWork> receiveWorks;
}
