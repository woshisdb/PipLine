using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// 交易金钱
    /// </summary>
    /// <param name="needGoods"></param>
    /// <param name="sendGoods"></param>
    public void TransGoodsMoney(NeedGoods needGoods,SendGoods sendGoods)
    {
        var transMoney = MapSystem.Instance.WasterMoney(sendGoods.obj, needGoods.obj);
        var perGoodsCost=sendGoods.minMoney + transMoney;
        var sum = Math.Min((int)(needGoods.obj.getMoney()/perGoodsCost),Math.Min(sendGoods.remainSum,needGoods.needSum));
        var goodsItem = new TransGoodsItem();
        goodsItem.sendGoods = sendGoods;
        goodsItem.needGoods = needGoods;
        goodsItem.sender = sendGoods.obj;
        goodsItem.needer=needGoods.obj;
        goodsItem.goodsCount = sum;
        var time = MapSystem.Instance.WasterTime(sendGoods.obj, needGoods.obj);
        var day = time / TimeSystem.Instance.dayTime;
        MapSystem.Instance.cirQueue.FindFront(day).Add(goodsItem);//添加经济循环
        goodsItem.sender.addMoney(sum*sendGoods.minMoney);
        goodsItem.needer.reduceMoney(sum*perGoodsCost);
        GameArchitect.Instance.government.addMoney(sum* transMoney);//用来转移商品的钱
    }
    private EconmicSystem()
    {

    }
}
public enum TransMoneyEnum
{
    paySalary,//发薪水
    buyGoods,//购买商品
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