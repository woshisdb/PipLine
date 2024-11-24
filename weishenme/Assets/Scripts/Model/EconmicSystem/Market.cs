using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 货物管理器
/// </summary>
public class GoodsManager
{
    public Dictionary<GoodsEnum,GoodsObj> goods;
    public BuildingObj building;
    public GoodsManager(GoodsEnum[] goodsEnums,BuildingObj buildingObj)
    {
        building = buildingObj;
        goods = new Dictionary<GoodsEnum, GoodsObj>();
        foreach(var good in goodsEnums)
        {
            GoodsObj goodObj = new GoodsObj(building,0,good);
            goods.Add(good, goodObj);
        }
    }
}


/// <summary>
/// 匹配器
/// </summary>
public abstract class Matcher
{
    /// <summary>
    /// 匹配订单
    /// </summary>
    public abstract void Match();
}
/// <summary>
/// 市场场景
/// </summary>
public class MarketWorkScene
{
    /// <summary>
    /// 一系列请求工作的协议
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// 请求工作的NPC
    /// </summary>
    public List<NeedWork> receiveWorks;
}
/// <summary>
/// 市场场景
/// </summary>
public class MarketGoodsScene
{
    /// <summary>
    /// 一系列请求商品的协议
    /// </summary>
    public List<SendGoods> sendGoods;
    /// <summary>
    /// 可以接收商品的协议
    /// </summary>
    public List<NeedGoods> receiveGoods;
}

public class GoodsMatcher : Matcher
{
    /// <summary>
    /// 对商品的匹配
    /// </summary>
    public BiDictionary<NeedGoods, SendGoods, GoodsContract> goodsMatchs;
    /// <summary>
    /// 发送商品
    /// </summary>
    public HashSet<NeedGoods> needGoods;
    public Dictionary<GoodsEnum, HashSet<SendGoods>> sendGoods;
    public override void Match()
    {
        foreach (var need in needGoods)
        {
            // 获取能提供该商品的发送者集合
            if (!sendGoods.TryGetValue(need.goods, out var sendGoodsSet))
                continue; // 没有该商品的发送者，跳过
            foreach(var x in sendGoodsSet)
            {
                x.sortVal= x.minMoney + MapSystem.Instance.WasterMoney(x.scene, need.scene);
            }
            foreach (var send in sendGoodsSet.OrderBy(s => {
                return s.sortVal;
            }))
            {
                var allcost= MapSystem.Instance.WasterMoney(send.scene, need.scene)+ send.minMoney;
                // 检查是否满足价格条件
                if (allcost > need.maxMoney) break;
                // 分配数量
                int allocated = Math.Min(need.needSum, send.remainSum);
                if (allocated > 0)
                {
                    //need.needSum -= allocated;
                    //send.remainSum -= allocated;
                    EconmicSystem.Instance.TransGoodsMoney(need,send);//进行交易
                    // 如果需求已经满足，跳出循环
                    if (need.needSum <= 0) break;
                }
            }
        }
    }
    public void AddNeed(NeedGoods need)
    {
        needGoods.Add(need);
    }
    public void AddSend(SendGoods send)
    {
        if(sendGoods.ContainsKey(send.goods))
        {
            sendGoods[send.goods].Add(send);
        }
        else
        {
            sendGoods[send.goods] = new HashSet<SendGoods>();
            sendGoods[send.goods].Add(send);
        }
    }
}

public class WorkMatcher : Matcher
{
    /// <summary>
    /// 对工作的匹配
    /// </summary>
    public BiDictionary<NeedWork, SendWork, WorkContract> workMatchs;
    /// <summary>
    /// 发送商品
    /// </summary>
    public SortedSet<SendWork> sendWorks;
    //一系列的工作
    public List<NeedWork> needWorks;
    /// <summary>
    /// 一系列协议
    /// </summary>
    public override void Match()
    {
        List<(SendWork, NeedWork)> ss = new List<(SendWork, NeedWork)>();
        foreach (var sender in sendWorks)
        {
            NeedWork selected = null;
            foreach (var needs in needWorks)
            {
                if (needs.satifyRate(sender) > 0)//选择商品
                {
                    if (selected == null)
                        selected = needs;
                    else
                    {
                        if (needs.satifyRate(sender) > selected.satifyRate(sender))
                        {
                            selected = needs;
                        }
                    }
                }
            }
            needWorks.Remove(selected);
            sendWorks.Remove(sender);
            workMatchs.Add(selected, sender, new WorkContract(selected, sender));
        }
    }
}

public abstract class PathTrans
{
    /// <summary>
    /// 剩余的时间
    /// </summary>
    public int wasterTime;
    /// <summary>
    /// 结束调用
    /// </summary>
    public abstract void EndCall();
}

public class GoodsPathTrans : PathTrans
{
    public override void EndCall()
    {
        throw new NotImplementedException();
    }
}

public class NpcPathTrans : PathTrans
{
    public override void EndCall()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 市场
/// </summary>
public class Market : Singleton<Market>
{
    /// <summary>
    /// 一系列场景
    /// </summary>
    public List<List<SceneObj>> scenes { get { return SaveSystem.Instance.saveData.SceneObjects; } }
    public GoodsMatcher goodsMatcher { get { return SaveSystem.Instance.saveData.goodsMatcher; } }
    public WorkMatcher workMatcher { get { return SaveSystem.Instance.saveData.workMatcher; } }
    public List<PathTrans> pathTrans { get { return SaveSystem.Instance.saveData.pathTrans; } }

    private Market()
    {
    }

    /// <summary>
    /// 匹配场景中的订单
    /// </summary>
    public void MatchOrder()
    {
        goodsMatcher.Match();
        workMatcher.Match();
    }
    /// <summary>
    /// 对路径进行更新
    /// </summary>
    public void PathUpdate()
    {
        for(int i = 0; i < pathTrans.Count; i++)
        {
            PathTrans trans = pathTrans[i];
            trans.wasterTime -= TimeSystem.Instance.dayTime;
            if(trans.wasterTime < 0)//结束了
            {
                trans.EndCall();
            }
        }    
    }
    public void Register(NeedGoods needGoods)
    {
        goodsMatcher.AddNeed(needGoods);
    }
    public void UnRegister(NeedGoods needGoods)
    {

    }

    public void Register(NeedWork needWork)
    {
        
    }
    public void UnRegister(NeedWork needWork)
    {

    }
    public void Register(SendGoods sendGoods)
    {
        goodsMatcher.AddSend(sendGoods);
    }
    public void UnRegister(SendGoods sendGoods)
    {

    }

    public void Register(SendWork sendWork)
    {

    }
    public void UnRegister(SendWork sendWork)
    {

    }
}