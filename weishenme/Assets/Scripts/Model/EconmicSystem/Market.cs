using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoodsManager
{
    public Dictionary<GoodsEnum,GoodsObj> goods;
    public GoodsManager(GoodsEnum[] goodsEnums)
    {
        goods = new Dictionary<GoodsEnum, GoodsObj>();
        
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
    public SortedSet<NeedGoods> needGoods;
    public Dictionary<GoodsEnum,HashSet<SendGoods>> sendGoods;
    public override void Match()
    {
        List<(NeedGoods, SendGoods)> ss=new List<(NeedGoods, SendGoods)>();
        foreach (NeedGoods s in needGoods)
        {
            var goodsEnum=s.goods;
            SendGoods selected = null;
            foreach(var sends in sendGoods[goodsEnum])//接收商品
            {
                if(sends.satifyRate(s)>0)//选择商品
                {
                    if(selected==null)
                        selected = sends;
                    else
                    {
                        if(sends.satifyRate(s)>selected.satifyRate(s))
                        {
                            selected=sends;
                        }
                    }
                }
            }
            needGoods.Remove(s);
            sendGoods[selected.goods].Remove(selected);
            goodsMatchs.Add(s, selected,new GoodsContract(s,selected));
        }
        return;
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
public class Market :Singleton<Market>
{
    /// <summary>
    /// 一系列场景
    /// </summary>
    public List<SceneObj> scenes;
    public GoodsMatcher goodsMatcher;
    public WorkMatcher workMatcher;
    public List<PathTrans> pathTrans; 
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
    /// <summary>
    /// 预期员工能获得的实际收入
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateWorkMoney(SendWork sendWork, NeedWork needWork)
    {
        return sendWork.maxMoney;
    }
    /// <summary>
    /// 预期员工在抛出路费后获得的收入
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateRealWorkMoney(SendWork sendWork,NeedWork needWork)
    {
        var disCost = NpcDistanceCost(needWork.obj.nowPos(), sendWork.obj.aimPos()).Item2;
        return PredicateWorkMoney(sendWork,needWork)-disCost*2;
    }
    /// <summary>
    /// 预期商品进口价格
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateGoodsMoney(SendGoods sendGoods, NeedGoods needGoods)
    {
        return sendGoods.minMoney+GoodsDistanceCost(sendGoods.obj.nowPos(), needGoods.obj.aimPos()).Item2;
    }
    /// <summary>
    /// 预期员工在抛出路费后获得的收入
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateRealGoodsMoney(SendGoods sendGoods, NeedGoods needGoods)
    {
        var disCost = GoodsDistanceCost(sendGoods.obj.nowPos(), needGoods.obj.aimPos()).Item2;
        return disCost + PredicateGoodsMoney(sendGoods, needGoods);
    }
}