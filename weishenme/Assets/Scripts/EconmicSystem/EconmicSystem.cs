using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// ���ݵ�ǰ��һϵ����Դ��һ��Ԥ������
    /// </summary>
    /// <returns></returns>
    public float CPI(Dictionary<GoodsEnum, Goods> pairs,int day)
    {

    }
    /// <summary>
    /// ����,��Ǯ��npc,Ȼ��������
    /// </summary>
    /// <param name="buildingState"></param>
    public void GiveMoney(GoodsBuildingObj buildingState,NpcState npcState,Float money)
    {
    }

    /// <summary>
    /// ������Ʒ
    /// </summary>
    public void Buy(IOrderUser orderUser,BaseState state,Order orderInf)
    {
        orderInf.Effect(orderUser,state);
        if(!orderInf.isPredict)//����Ԥ��Ľ��
        {
            orderInf.Accept(state,orderUser);//�������˵�Ӱ��
        }
    }
    /// <summary>
    /// ��ȡ����������Ҫ���ѵ�Ǯ
    /// </summary>
    /// <param name="prods"></param>
    /// <param name="num"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public float GetProdMoney(BaseState state,Tuple<ProdEnum, Int>[] prods, Int num,int day=0,bool isPredic=false)
    {
        if(isPredic)//�����Ԥ��Ļ����������
        {
        }
        else//��ֱ�ӳ����
        {

        }
    }
    /// <summary>
    /// ��ȡҪ������Ʒ�ļ۸�
    /// </summary>
    /// <param name="prods"></param>
    /// <param name="num"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public float GetGoodsMoney(BaseState state, Tuple<GoodsEnum, Int>[] prods, Int num,int day=0, bool isPredic = false)
    {
        if(isPredic)//�����Ԥ��Ļ����������
        {

        }
        else//��ֱ�ӳ����
        {

        }
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

    /// <summary>
    /// �����ṩ������ʱ��Ԥ���ȡ������
    /// </summary>
    /// <returns></returns>
    public float PredicateProdEarn()
    {

    }
    /// <summary>
    /// ת��Ǯ,����Э��
    /// </summary>
    /// <returns></returns>
    public float TransMoney(TransMoneyMode ContractMode)
    {

    }
    /// <summary>
    /// Ԥ��ĳһ���˻�õ�Ǯ
    /// </summary>
    /// <param name="ContractMode"></param>
    /// <returns></returns>
    public float PredicTransMoney(TransMoneyMode ContractMode,NpcState npcState)
    {

    }
    /// <summary>
    /// ��·��Ǯ
    /// </summary>
    /// <returns></returns>
    public float GetRoadMoney(SceneObj a,SceneObj b,int time)
    {

    }
    public float PredicateRoadMoney()
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
    /// <summary>
    /// ��Aת�Ƶ�B��Ǯ
    /// </summary>
    public IContractUser toA;
    public IContractUser toB;
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