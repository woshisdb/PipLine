using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matcher
{
    /// <summary>
    /// ƥ�乤������
    /// </summary>
    public List<(SendWork, NeedWork)> MatchWork(List<SendWork> sendWorks, List<NeedWork> needWorks)
    {
        var matchedWorks = new List<(SendWork, NeedWork)>();
        // ����SendWork �� maxMoney ����NeedWork �� minMoney ����
        sendWorks = sendWorks.OrderByDescending(s => s.maxMoney).ToList();
        needWorks = needWorks.OrderByDescending(n => n.minMoney).ToList();
        while (sendWorks.Any() && needWorks.Any())
        {
            // ����������洢��ǰ���ƥ��
            SendWork bestSender = null;
            NeedWork bestNeeder = null;
            float bestSatisfaction = float.MinValue;
            // ���� SendWork �� NeedWork ��ϣ�ѡ�����������Ҿ����������ȵ����
            foreach (var sender in sendWorks)
            {
                foreach (var needer in needWorks)
                {
                    if (sender.scene() == needer.scene() && sender.maxMoney >= needer.minMoney && sender.isSatify(needer))
                    {
                        float satisfaction = sender.satifyRate(needer);

                        // �������ϵ�����ȸ��ߣ������������
                        if (satisfaction > bestSatisfaction)
                        {
                            bestSender = sender;
                            bestNeeder = needer;
                            bestSatisfaction = satisfaction;
                        }
                    }
                }
            }
            // ����ҵ����ƥ�䣬�������ƥ���б�
            if (bestSender != null && bestNeeder != null)
            {
                matchedWorks.Add((bestSender, bestNeeder));
                sendWorks.Remove(bestSender);  // �Ƴ���ƥ��� SendWork 
                needWorks.Remove(bestNeeder);  // �Ƴ���ƥ��� NeedWork 
            }
            else
            {
                // ���û���ҵ�����������ƥ����˳�ѭ��
                break;
            }
        }
        return matchedWorks;
    }


}




/// <summary>
/// �г�
/// </summary>
public class Market :Singleton<Market>
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
    public List<NeedGoods> receiveGoods;
    //****************************************************
    /// <summary>
    /// һϵ����������Э��
    /// </summary>
    public List<SendWork> sendWorks;
    /// <summary>
    /// ��������NPC
    /// </summary>
    public List<NeedWork> receiveWorks;
}
