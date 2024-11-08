using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matcher
{
    /// <summary>
    /// 匹配工作需求
    /// </summary>
    public List<(SendWork, NeedWork)> MatchWork(List<SendWork> sendWorks, List<NeedWork> needWorks)
    {
        var matchedWorks = new List<(SendWork, NeedWork)>();
        // 排序：SendWork 按 maxMoney 降序，NeedWork 按 minMoney 降序
        sendWorks = sendWorks.OrderByDescending(s => s.maxMoney).ToList();
        needWorks = needWorks.OrderByDescending(n => n.minMoney).ToList();
        while (sendWorks.Any() && needWorks.Any())
        {
            // 定义变量来存储当前最佳匹配
            SendWork bestSender = null;
            NeedWork bestNeeder = null;
            float bestSatisfaction = float.MinValue;
            // 遍历 SendWork 和 NeedWork 组合，选择满足条件且具有最高满足度的组合
            foreach (var sender in sendWorks)
            {
                foreach (var needer in needWorks)
                {
                    if (sender.scene() == needer.scene() && sender.maxMoney >= needer.minMoney && sender.isSatify(needer))
                    {
                        float satisfaction = sender.satifyRate(needer);

                        // 如果该组合的满足度更高，则更新最佳组合
                        if (satisfaction > bestSatisfaction)
                        {
                            bestSender = sender;
                            bestNeeder = needer;
                            bestSatisfaction = satisfaction;
                        }
                    }
                }
            }
            // 如果找到最佳匹配，则将其加入匹配列表
            if (bestSender != null && bestNeeder != null)
            {
                matchedWorks.Add((bestSender, bestNeeder));
                sendWorks.Remove(bestSender);  // 移除已匹配的 SendWork 
                needWorks.Remove(bestNeeder);  // 移除已匹配的 NeedWork 
            }
            else
            {
                // 如果没有找到满足条件的匹配项，退出循环
                break;
            }
        }
        return matchedWorks;
    }


}




/// <summary>
/// 市场
/// </summary>
public class Market :Singleton<Market>
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
        
    }
    /// <summary>
    /// 两个场景之间的距离和花钱
    /// </summary>
    /// <returns></returns>
    public (int,float) GoodsDistanceCost(SceneObj a,SceneObj b)
    {
        return (1,0);
    }
    /// <summary>
    /// NPC之间的距离和花钱
    /// </summary>
    /// <returns></returns>
    public (int, float) NpcDistanceCost(SceneObj a, SceneObj b)
    {
        return (1, 0);
    }
    public float PredicateWorkMoney(SendWork sendWork, NeedWork needWork)
    {
        return sendWork.maxMoney;
    }
    public float PredicateMoney(SendWork sendWork,NeedWork needWork)
    {
        var disCost = NpcDistanceCost(needWork.obj.nowPos(), sendWork.obj.aimPos()).Item2;
        return PredicateWorkMoney(sendWork,needWork)-disCost;
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
    public List<NeedGoods> receiveGoods;
    //****************************************************
    /// <summary>
    /// 一系列请求工作的协议
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// 请求工作的NPC
    /// </summary>
    public List<NeedWork> receiveWorks;
}
