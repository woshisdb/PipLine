using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// ����,��Ǯ��npc,Ȼ��������
    /// </summary>
    /// <param name="buildingState"></param>
    public void GiveMoney(GoodsBuildingObj buildingState,NpcState npcState,Float money)
    {
    }
    /// <summary>
    /// ���׽�Ǯ
    /// </summary>
    public void TransWorkMoney(NeedWork needWork,SendWork sendWork)
    {
        var givenMoney=Market.Instance.PredicateWorkMoney(sendWork,needWork);
    }
    /// <summary>
    /// ���׽�Ǯ
    /// </summary>
    /// <param name="needGoods"></param>
    /// <param name="sendGoods"></param>
    public void TransGoodsMoney(NeedGoods needGoods,SendGoods sendGoods)
    {
        var givenMoney = Market.Instance.PredicateGoodsMoney(sendGoods,needGoods);
    }
    /// <summary>
    /// ��ȡһ����·,��A��B,�����˵���·,������󻨷�,���ʱ��,��ȡһ��·���б�
    /// </summary>
    /// <returns></returns>
    public List<PathObj> GetRoad(SceneObj a,SceneObj b,Float maxCost,Int maxTime)
    {
        return null;
    }

    public void AddPathOrder(SceneObj sceneObj)
    {

    }
}
public enum TransMoneyEnum
{
    paySalary,//��нˮ
    buyGoods,//������Ʒ
    transGoods,//ת����Ʒ
    npcPath,//·��ת��
}
/// <summary>
/// ת�ƽ�Ǯ������
/// </summary>
public class TransMoneyMode
{
    public TransMoneyEnum transMoney;
    public Float money;
}
public class PaySalaryMode: TransMoneyMode
{

}
public class BuyGoodsMode : TransMoneyMode
{
    public GoodsEnum goodsEnum;
}
public class TransGoodsMode : TransMoneyMode
{

}
public class NpcPathMode : TransMoneyMode
{

}