using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matcher
{
    /// <summary>
    /// ƥ�䷢���ߺͽ����ߵĹ�������
    /// </summary>
    /// <param name="sendWorks">���͹���������б�</param>
    /// <param name="receiveWorks">���չ���������б�</param>
    /// <returns>ƥ�䵽�Ĺ��������</returns>
    public List<(SendWork, ReceiveWork)> MatchWorks(List<SendWork> sendWorks, List<ReceiveWork> receiveWorks)
    {
        var matchedWorks = new List<(SendWork, ReceiveWork)>();
        foreach (var sender in sendWorks)
        {
            foreach (var receiver in receiveWorks)
            {
                // ����Ƿ�����λ�úͼ۸�����
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
    /// ƥ�䷢���ߺͽ����ߵ���Ʒ����
    /// </summary>
    /// <param name="sendGoodsList">������Ʒ������б�</param>
    /// <param name="receiveGoodsList">������Ʒ������б�</param>
    /// <returns>ƥ�䵽����Ʒ�����</returns>
    public List<(SendGoods, ReceiveGoods)> MatchGoods(List<SendGoods> sendGoodsList, List<ReceiveGoods> receiveGoodsList)
    {
        var matchedGoods = new List<(SendGoods, ReceiveGoods)>();
        foreach (var sender in sendGoodsList)
        {
            foreach (var receiver in receiveGoodsList)
            {
                // ����Ƿ�����λ�úͼ۸�����
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
/// �г�
/// </summary>
public class Market
{
    /// <summary>
    /// ÿ�������������������Ϣ
    /// </summary>
    public Dictionary<SceneObj, MarketScene> Markets = new Dictionary<SceneObj, MarketScene>();
    /// <summary>
    /// ƥ��һ�����ʵĶ���
    /// </summary>
    public void MatchOrder()
    {
        for()
        {

        }
    }
    /// <summary>
    /// ��������֮��ľ���ͻ�Ǯ
    /// </summary>
    /// <returns></returns>
    public Tuple<int,float> GoodsDistanceCost(SceneObj a,SceneObj b)
    {
        return new Tuple<int, float>(1,0);
    }
    /// <summary>
    /// NPC֮��ľ���ͻ�Ǯ
    /// </summary>
    /// <returns></returns>
    public Tuple<int, float> NpcDistanceCost(SceneObj a, SceneObj b)
    {
        return new Tuple<int, float>(1, 0);
    }
}



/// <summary>
/// �г�����
/// </summary>
public class MarketScene
{
    /// <summary>
    /// һϵ��������Ʒ��Э��
    /// </summary>
    public List<SendGoods> sendGoods;
    /// <summary>
    /// ���Խ�����Ʒ��Э��
    /// </summary>
    public List<ReceiveGoods> receiveGoods;
    //****************************************************
    /// <summary>
    /// һϵ����������Э��
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// ��������NPC
    /// </summary>
    public List<ReceiveWork> receiveWorks;
}
