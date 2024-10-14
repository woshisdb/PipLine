using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象的状态
/// </summary>
public class BaseState
{
    public virtual void Init()
    {

    }
}
/// <summary>
/// 一个经济情况的信息
/// </summary>
public class EconomicInf
{

}

public abstract class BaseObj<T,F> 
where T : BaseState, new()
where F : EconomicInf, new()
{
    public int stateLen=10;
    /// <summary>
    /// 一系列的状态
    /// </summary>
    public List<T> stateSeq;
    public BaseObj()
    {
        stateSeq = new List<T>(stateLen);
        for(int i = 0; i < stateLen; i++)
        {
            stateSeq.Add(new T());
        }
    }
    /// <summary>
    /// 更新,输入一个状态,输出一个状态
    /// </summary>
    public abstract T Update(T input, T output);
    /// <summary>
    /// /获取当前状态
    /// </summary>
    /// <returns></returns>
    public T getNow()
    {
        return stateSeq[0];//当前状态
    }

}

/// <summary>
/// 可以下订单
/// </summary>
public interface IOrderUser
{
    /// <summary>
    /// 添加钱
    /// </summary>
    /// <param name="money"></param>
    public void addMoney(BaseState state,Float money);
    /// <summary>
    /// 减少钱
    /// </summary>
    /// <param name="money"></param>
    public void reduceMoney(BaseState state, Float money);
}
//可以接取订单,接取订单应该就可以拉订单
public interface ICanReceiveOrder: IOrderUser
{
    ////接收钱之后进行一系列处理
    //public void receiveMoney(Float money, GoodsOrderInf goodsInf);
}
/// <summary>
/// 可以接收请求商品的订单
/// </summary>
public interface ICanReceiveNormalGoodsOrder:ICanReceiveOrder
{
    public void receiveMoney(Float money, NormalGoodsInf goodsInf);

    public void registerReveiveGoodsOrder();
    public void unregisterReveiveGoodsOrder();
}
/// <summary>
/// 可以接收请求生产力的订单
/// </summary>
public interface ICanReceiveProdOrder : ICanReceiveOrder
{
    public void receiveMoney(Float money, ProdGoodsInf goodsInf);
    public void registerReveiveProdsOrder();
    public void unregisterReveiveProdsOrder();
}

/// <summary>
/// 可以请求商品的订单
/// </summary>
public interface INormalGoodsOrderUser:IOrderUser
{
    /// <summary>
    /// 获取商品列表,用来加进来
    /// </summary>
    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state);

    public CircularQueue<Float> GetPipline(BaseState state);
}

/// <summary>
/// 可以请求生产力的订单
/// </summary>
public interface IProdOrderUser : IOrderUser
{
    public Float GetProdState(BaseState state);
}
/// <summary>
/// 可以请求发送商品
/// </summary>
public interface IRequestPathUser:IOrderUser
{

}

public interface ICanReceivePathOrder: ICanReceiveOrder
{

}