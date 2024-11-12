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
    public SortedSet<NeedGoods> needGoods;
    public Dictionary<GoodsEnum,HashSet<SendGoods>> sendGoods;
    public override void Match()
    {
        List<(NeedGoods, SendGoods)> ss=new List<(NeedGoods, SendGoods)>();
        foreach (NeedGoods s in needGoods)
        {
            var goodsEnum=s.goods;
            SendGoods selected = null;
            foreach(var sends in sendGoods[goodsEnum])//������Ʒ
            {
                if(sends.satifyRate(s)>0)//ѡ����Ʒ
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
public class Market :Singleton<Market>
{
    /// <summary>
    /// һϵ�г���
    /// </summary>
    public List<SceneObj> scenes;
    public GoodsMatcher goodsMatcher;
    public WorkMatcher workMatcher;
    public List<PathTrans> pathTrans; 
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
    /// ��������֮��ľ���ͻ�Ǯ
    /// </summary>
    /// <returns></returns>
    public (int,float) GoodsDistanceCost(SceneObj a,SceneObj b)
    {
        return (1,0);
    }
    /// <summary>
    /// NPC֮��ľ���ͻ�Ǯ
    /// </summary>
    /// <returns></returns>
    public (int, float) NpcDistanceCost(SceneObj a, SceneObj b)
    {
        return (1, 0);
    }
    /// <summary>
    /// Ԥ��Ա���ܻ�õ�ʵ������
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateWorkMoney(SendWork sendWork, NeedWork needWork)
    {
        return sendWork.maxMoney;
    }
    /// <summary>
    /// Ԥ��Ա�����׳�·�Ѻ��õ�����
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
    /// Ԥ����Ʒ���ڼ۸�
    /// </summary>
    /// <param name="sendWork"></param>
    /// <param name="needWork"></param>
    /// <returns></returns>
    public float PredicateGoodsMoney(SendGoods sendGoods, NeedGoods needGoods)
    {
        return sendGoods.minMoney+GoodsDistanceCost(sendGoods.obj.nowPos(), needGoods.obj.aimPos()).Item2;
    }
    /// <summary>
    /// Ԥ��Ա�����׳�·�Ѻ��õ�����
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