using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// 根据当前的一系列资源算一个预期收益
    /// </summary>
    /// <returns></returns>
    public float CPI(Dictionary<GoodsEnum, Goods> pairs,int day)
    {

    }
    /// <summary>
    /// 交易,将钱给npc,然后获得收益
    /// </summary>
    /// <param name="buildingState"></param>
    public void GiveMoney(GoodsBuildingObj buildingState,NpcState npcState,Float money)
    {
    }

    /// <summary>
    /// 购买商品
    /// </summary>
    public void Buy(IOrderUser orderUser,BaseState state,Order orderInf)
    {
        orderInf.Effect(orderUser,state);
        if(!orderInf.isPredict)//不是预测的结果
        {
            orderInf.Accept(state,orderUser);//对其他人的影响
        }
    }
    /// <summary>
    /// 获取生产力所需要花费的钱
    /// </summary>
    /// <param name="prods"></param>
    /// <param name="num"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public float GetProdMoney(BaseState state,Tuple<ProdEnum, Int>[] prods, Int num,int day=0,bool isPredic=false)
    {
        if(isPredic)//如果是预测的话则进行推理
        {
        }
        else//则直接出结果
        {

        }
    }
    /// <summary>
    /// 获取要购买商品的价格
    /// </summary>
    /// <param name="prods"></param>
    /// <param name="num"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public float GetGoodsMoney(BaseState state, Tuple<GoodsEnum, Int>[] prods, Int num,int day=0, bool isPredic = false)
    {
        if(isPredic)//如果是预测的话则进行推理
        {

        }
        else//则直接出结果
        {

        }
    }
    /// <summary>
    /// 获取一个道路,从A到B,用于人的走路,根据最大花费,最大时间,获取一个路径列表
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
    /// 根据提供的生产时间预测获取的收入
    /// </summary>
    /// <returns></returns>
    public float PredicateProdEarn()
    {

    }
    /// <summary>
    /// 转移钱,根据协议
    /// </summary>
    /// <returns></returns>
    public float TransMoney(TransMoneyMode ContractMode)
    {

    }
    /// <summary>
    /// 预测某一个人获得的钱
    /// </summary>
    /// <param name="ContractMode"></param>
    /// <returns></returns>
    public float PredicTransMoney(TransMoneyMode ContractMode,NpcState npcState)
    {

    }
    /// <summary>
    /// 道路的钱
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
    paySalary,//发薪水
    buyGoods,//购买商品
    transGoods,//转移商品
    npcPath,//路径转移
}
/// <summary>
/// 转移金钱的类型
/// </summary>
public class TransMoneyMode
{
    public TransMoneyEnum transMoney;
    public Float money;
    /// <summary>
    /// 由A转移到B的钱
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