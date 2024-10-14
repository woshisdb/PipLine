using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EconmicSystem:Singleton<EconmicSystem>
{
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
    public GoodsOrder GetProdMoney(BaseState state,Tuple<ProdEnum, Int>[] prods, Int num,int day=0,bool predicate=false)
    {//为0时候为当前天,predicate为是否为预测值,而不是真的
        return null;
    }
    /// <summary>
    /// 获取要购买商品的价格
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
/// 订单信息
/// </summary>
public class Order
{
    /// <summary>
    /// 是否为预测值,是的话就不用执行效果
    /// </summary>
    public bool isPredict;
    /// <summary>
    /// 对订单人提供商的效果
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
/// 商品订单
/// </summary>
public class GoodsOrder:Order
{
    /// <summary>
    /// 订单的花费
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
    /// 商品类型
    /// </summary>
    public GoodsEnum goodsEnum;
    /// <summary>
    /// 订单数目
    /// </summary>
    public int sum;
    /// <summary>
    /// 接取订单的人
    /// </summary>
    public ICanReceiveNormalGoodsOrder ReceiveOrder;
    /// <summary>
    /// 应转给她的开销
    /// </summary>
    public Float goodsPrice;
}

/// <summary>
/// 购买一系列商品的订单
/// </summary>
public class NormalGoodsOrder : GoodsOrder
{
    /// <summary>
    /// 商品的列表
    /// </summary>
    public NormalGoodsInf[] goods;
    public int sum;
    public override void Accept(BaseState state, IOrderUser orderUser)
    {
        foreach (var good in goods)//更新每个商品提供商转钱
        {
            //处理收入,然后修改订单
            good.ReceiveOrder.receiveMoney(good.goodsPrice, good);
        }
    }
    /// <summary>
    /// 将商品添加进去
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
    /// 应转给她的开销
    /// </summary>
    public float goodsPrice;
    public float prodAmount;
    public ICanReceiveProdOrder canReceiveProdOrder;
}
/// <summary>
/// 购买一系列生产力的订单
/// </summary>
public class ProdGoodsOrder : GoodsOrder
{
    /// <summary>
    /// 生产力商品
    /// </summary>
    public ProdGoodsInf[] goods;
    public float sum;
    public override void Accept(BaseState state, IOrderUser orderUser)
    {
        foreach (var good in goods)//更新每个商品提供商转钱
        {
            //处理收入,然后修改订单
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


