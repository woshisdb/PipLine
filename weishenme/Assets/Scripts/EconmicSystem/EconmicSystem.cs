using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
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
    public GoodsOrder GetProdMoney(BaseState state,Tuple<ProdEnum, Int>[] prods, Int num,int day=0,bool predicate=false)
    {//Ϊ0ʱ��Ϊ��ǰ��,predicateΪ�Ƿ�ΪԤ��ֵ,���������
        return null;
    }
    /// <summary>
    /// ��ȡҪ������Ʒ�ļ۸�
    /// </summary>
    /// <param name="prods"></param>
    /// <param name="num"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public GoodsOrder GetGoodsMoney(BaseState state, Tuple<GoodsEnum, Int>[] prods, Int num,int day=0, bool predicate = false)
    {
        return null;
    }
}

/// <summary>
/// ������Ϣ
/// </summary>
public class Order
{
    /// <summary>
    /// �Ƿ�ΪԤ��ֵ,�ǵĻ��Ͳ���ִ��Ч��
    /// </summary>
    public bool isPredict;
    /// <summary>
    /// �Զ������ṩ�̵�Ч��
    /// </summary>
    /// <param name="state"></param>
    /// <param name="orderUser"></param>
    public virtual void Accept(BaseState state,IOrderUser orderUser)
    {
        
    }
    public virtual void Effect(IOrderUser orderUser, BaseState state)
    {

    }
}

/// <summary>
/// ��Ʒ����
/// </summary>
public class GoodsOrder:Order
{
    /// <summary>
    /// �����Ļ���
    /// </summary>
    public Float cost;
    public override void Effect(IOrderUser orderUser, BaseState state)
    {
        orderUser.reduceMoney(state, cost);
    }
}
public class GoodsOrderInf
{

}
public class NormalGoodsInf: GoodsOrderInf
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public GoodsEnum goodsEnum;
    /// <summary>
    /// ������Ŀ
    /// </summary>
    public int sum;
    /// <summary>
    /// ��ȡ��������
    /// </summary>
    public ICanReceiveNormalGoodsOrder ReceiveOrder;
    /// <summary>
    /// Ӧת�����Ŀ���
    /// </summary>
    public Float goodsPrice;
}

/// <summary>
/// ����һϵ����Ʒ�Ķ���
/// </summary>
public class NormalGoodsOrder : GoodsOrder
{
    /// <summary>
    /// ��Ʒ���б�
    /// </summary>
    public NormalGoodsInf[] goods;
    public int sum;
    public override void Accept(BaseState state, IOrderUser orderUser)
    {
        foreach (var good in goods)//����ÿ����Ʒ�ṩ��תǮ
        {
            //��������,Ȼ���޸Ķ���
            good.ReceiveOrder.receiveMoney(good.goodsPrice, good);
        }
    }
    /// <summary>
    /// ����Ʒ��ӽ�ȥ
    /// </summary>
    /// <param name="orderUser"></param>
    /// <param name="state"></param>
    public override void Effect(IOrderUser orderUser, BaseState state)
    {
        base.Effect(orderUser, state);
        var user=(INormalGoodsOrderUser)orderUser;
        var value=user.GetPipline(state).FindFront(0);
        value.Value += sum;
    }
}
public class ProdGoodsInf: GoodsOrderInf
{
    public ProdEnum prodEnum;
    /// <summary>
    /// Ӧת�����Ŀ���
    /// </summary>
    public float goodsPrice;
    public float prodAmount;
    public ICanReceiveProdOrder canReceiveProdOrder;
}
/// <summary>
/// ����һϵ���������Ķ���
/// </summary>
public class ProdGoodsOrder : GoodsOrder
{
    /// <summary>
    /// ��������Ʒ
    /// </summary>
    public ProdGoodsInf[] goods;
    public float sum;
    public override void Accept(BaseState state, IOrderUser orderUser)
    {
        foreach (var good in goods)//����ÿ����Ʒ�ṩ��תǮ
        {
            //��������,Ȼ���޸Ķ���
            good.canReceiveProdOrder.receiveMoney(good.goodsPrice, good);
        }
    }
    public override void Effect(IOrderUser orderUser, BaseState state)
    {
        base.Effect(orderUser, state);
        var user =(IProdOrderUser)orderUser;
        Float prod=user.GetProdState(state);
        prod.Value += sum;
    }
}


