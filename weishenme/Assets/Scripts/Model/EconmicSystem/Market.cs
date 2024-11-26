using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// ���������
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
/// ƥ����
/// </summary>
public abstract class Matcher
{
    /// <summary>
    /// ƥ�䶩��
    /// </summary>
    public abstract void Match();
}
/// <summary>
/// �г�����
/// </summary>
public class MarketWorkScene
{
    /// <summary>
    /// һϵ����������Э��
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// ��������NPC
    /// </summary>
    public List<NeedWork> receiveWorks;
}
/// <summary>
/// �г�����
/// </summary>
public class MarketGoodsScene
{
    /// <summary>
    /// һϵ��������Ʒ��Э��
    /// </summary>
    public List<SendGoods> sendGoods;
    /// <summary>
    /// ���Խ�����Ʒ��Э��
    /// </summary>
    public List<NeedGoods> receiveGoods;
}

public class GoodsMatcher : Matcher
{
    /// <summary>
    /// ����Ʒ��ƥ��
    /// </summary>
    public BiDictionary<NeedGoods, SendGoods, GoodsContract> goodsMatchs;
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public HashSet<NeedGoods> needGoods;
    public Dictionary<GoodsEnum, HashSet<SendGoods>> sendGoods;
    public override void Match()
    {
        foreach (var need in needGoods)
        {
            // ��ȡ���ṩ����Ʒ�ķ����߼���
            if (!sendGoods.TryGetValue(need.goods, out var sendGoodsSet))
                continue; // û�и���Ʒ�ķ����ߣ�����
            foreach(var x in sendGoodsSet)
            {
                x.sortVal= x.minMoney + MapSystem.Instance.WasterMoney(x.obj, need.obj);
            }
            foreach (var send in sendGoodsSet.OrderBy(s => {
                return s.sortVal;
            }))
            {
                var allcost= MapSystem.Instance.WasterMoney(send.obj, need.obj)+ send.minMoney;
                // ����Ƿ�����۸�����
                if (allcost > need.maxMoney) break;
                // ��������
                int allocated = Math.Min(need.needSum, send.remainSum);
                if (allocated > 0)
                {
                    //need.needSum -= allocated;
                    //send.remainSum -= allocated;
                    EconmicSystem.Instance.TransGoodsMoney(need,send);//���н���
                    // ��������Ѿ����㣬����ѭ��
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
    public Dictionary<ProdEnum,List<SendWork>> sendWorks;
    //һϵ�еĹ���
    public Dictionary<ProdEnum,List<NeedWork>> needWorks;
    /// <summary>
    /// һϵ��Э��
    /// </summary>
    public override void Match()
    {
        foreach (var item in sendWorks)
        {
            foreach(var sender in item.Value)
            {
                foreach (var needer in needWorks[item.Key])
                {
                    if(sender.rate<=0)
                    {
                        break;
                    }
                    if(!needer.hasWork)
                    sender.AddNeeder(needer);
                }
            }
        }
    }
    public void AddNeed(NeedWork need)
    {
        needWorks[need.prodEnum].Add(need);
    }
    public void AddSend(SendWork send)
    {
        sendWorks[send.prodEnum].Add(send);
    }
    public WorkMatcher()
    {
        sendWorks=new Dictionary<ProdEnum,List<SendWork>>();
        needWorks=new Dictionary<ProdEnum,List<NeedWork>>();
        foreach (var e in Enum.GetValues(typeof( ProdEnum )))
        {
            var it=(ProdEnum)e;
            sendWorks.Add(it, new List<SendWork>());
            needWorks.Add(it, new List<NeedWork>());
        }
    }
}

/// <summary>
/// �г�
/// </summary>
public class Market : Singleton<Market>
{
    /// <summary>
    /// һϵ�г���
    /// </summary>
    public List<List<SceneObj>> scenes { get { return SaveSystem.Instance.saveData.SceneObjects; } }
    public GoodsMatcher goodsMatcher { get { return SaveSystem.Instance.saveData.goodsMatcher; } }
    public WorkMatcher workMatcher { get { return SaveSystem.Instance.saveData.workMatcher; } }

    private Market()
    {
    }

    /// <summary>
    /// ƥ�䳡���еĶ���
    /// </summary>
    public void MatchOrder()
    {
        goodsMatcher.Match();
        workMatcher.Match();
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