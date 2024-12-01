using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
    public static EconmicSystem get { get { return EconmicSystem.Instance; } }
    /// <summary>
    /// ���׽�Ǯ
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
        MapSystem.Instance.cirQueue.FindFront(day).Add(goodsItem);//��Ӿ���ѭ��
        goodsItem.sender.addMoney(sum*sendGoods.minMoney);
        goodsItem.needer.reduceMoney(sum*perGoodsCost);
        GameArchitect.Instance.government.addMoney(sum* transMoney);//����ת����Ʒ��Ǯ
    }
    private EconmicSystem()
    {

    }
}
public enum TransMoneyEnum
{
    paySalary,//��нˮ
    buyGoods,//������Ʒ
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