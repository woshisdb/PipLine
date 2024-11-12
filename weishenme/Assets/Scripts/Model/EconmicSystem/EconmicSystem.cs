using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// 交易,将钱给npc,然后获得收益
    /// </summary>
    /// <param name="buildingState"></param>
    public void GiveMoney(GoodsBuildingObj buildingState,NpcState npcState,Float money)
    {
    }
    /// <summary>
    /// 交易金钱
    /// </summary>
    public void TransWorkMoney(NeedWork needWork,SendWork sendWork)
    {
        var givenMoney=Market.Instance.PredicateWorkMoney(sendWork,needWork);
    }
    /// <summary>
    /// 交易金钱
    /// </summary>
    /// <param name="needGoods"></param>
    /// <param name="sendGoods"></param>
    public void TransGoodsMoney(NeedGoods needGoods,SendGoods sendGoods)
    {
        var givenMoney = Market.Instance.PredicateGoodsMoney(sendGoods,needGoods);
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