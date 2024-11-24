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
                x.sortVal= x.minMoney + MapSystem.Instance.WasterMoney(x.scene, need.scene);
            }
            foreach (var send in sendGoodsSet.OrderBy(s => {
                return s.sortVal;
            }))
            {
                var allcost= MapSystem.Instance.WasterMoney(send.scene, need.scene)+ send.minMoney;
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
    /// <summary>
    /// �Թ�����ƥ��
    /// </summary>
    public BiDictionary<NeedWork, SendWork, WorkContract> workMatchs;
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public SortedSet<SendWork> sendWorks;
    //һϵ�еĹ���
    public List<NeedWork> needWorks;
    /// <summary>
    /// һϵ��Э��
    /// </summary>
    public override void Match()
    {
        List<(SendWork, NeedWork)> ss = new List<(SendWork, NeedWork)>();
        foreach (var sender in sendWorks)
        {
            NeedWork selected = null;
            foreach (var needs in needWorks)
            {
                if (needs.satifyRate(sender) > 0)//ѡ����Ʒ
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
    /// ʣ���ʱ��
    /// </summary>
    public int wasterTime;
    /// <summary>
    /// ��������
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
    public List<PathTrans> pathTrans { get { return SaveSystem.Instance.saveData.pathTrans; } }

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
    /// <summary>
    /// ��·�����и���
    /// </summary>
    public void PathUpdate()
    {
        for(int i = 0; i < pathTrans.Count; i++)
        {
            PathTrans trans = pathTrans[i];
            trans.wasterTime -= TimeSystem.Instance.dayTime;
            if(trans.wasterTime < 0)//������
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