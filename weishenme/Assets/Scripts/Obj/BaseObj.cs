using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象的状态
/// </summary>
public abstract class BaseState
{
    public BaseObj obj;
    public virtual void Init()
    {

    }
    public virtual BaseState Clone(BaseObj obj)
    {
        return null;
    }
    public BaseState(BaseObj obj)
    {
        this.obj = obj;
    }

    public BaseObj GetObj()
    {
        return obj;
    }
}
/// <summary>
/// 一个经济情况的信息
/// </summary>
public class EconomicInf
{
    public BaseObj obj;
    public EconomicInf(BaseObj obj)
    {
        this.obj = obj;
    }
}

public abstract class BaseObj
{
    public BaseState state;
    public EconomicInf ecInf;
    public abstract void InitBaseState();

    public abstract void InitEconomicInf();

    public int stateLen=10;
    public BaseObj()
    {
        InitBaseState();
        InitEconomicInf();
    }
    /// <summary>
    /// 更新,输入一个状态,输出一个状态
    /// </summary>
    public abstract void Update(BaseState input);
    /// <summary>
    /// 根据当前的值去预测一个自己未来的状态
    /// </summary>
    /// <param name="input"></param>
    /// <param name="day"></param>
    public abstract void Predict(BaseState input,int day);
    /// <summary>
    /// /获取当前状态
    /// </summary>
    /// <returns></returns>
    public BaseState getNow()
    {
        return state;
    }

}

///// <summary>
///// 可以下订单
///// </summary>
//public interface IOrderUser
//{
//    /// <summary>
//    /// 添加钱
//    /// </summary>
//    /// <param name="money"></param>
//    public void addMoney(BaseState state,Float money);
//    /// <summary>
//    /// 减少钱
//    /// </summary>
//    /// <param name="money"></param>
//    public void reduceMoney(BaseState state, Float money);
//}
////可以接取订单,接取订单应该就可以拉订单
//public interface ICanReceiveOrder: IOrderUser
//{
//    ////接收钱之后进行一系列处理
//    //public void receiveMoney(Float money, GoodsOrderInf goodsInf);
//}
///// <summary>
///// 可以接收请求商品的订单
///// </summary>
//public interface ICanReceiveNormalGoodsOrder:ICanReceiveOrder
//{
//    public void receiveMoney(Float money, NormalGoodsInf goodsInf);

//    public void registerReveiveGoodsOrder();
//    public void unregisterReveiveGoodsOrder();
//}
///// <summary>
///// 可以接收请求生产力的订单
///// </summary>
//public interface ICanReceiveProdOrder : ICanReceiveOrder
//{
//    public void receiveMoney(Float money, ProdGoodsInf goodsInf);
//    public void registerReveiveProdsOrder();
//    public void unregisterReveiveProdsOrder();
//}

///// <summary>
///// 可以请求商品的订单
///// </summary>
//public interface INormalGoodsOrderUser:IOrderUser
//{
//    /// <summary>
//    /// 获取商品列表,用来加进来
//    /// </summary>
//    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state);

//    public CircularQueue<Float> GetPipline(BaseState state);
//}

///// <summary>
///// 可以请求生产力的订单
///// </summary>
//public interface IProdOrderUser : IOrderUser
//{
//    public Float GetProdState(BaseState state);
//}
///// <summary>
///// 可以请求发送商品
///// </summary>
//public interface IRequestPathUser:IOrderUser
//{

//}

//public interface ICanReceivePathOrder: ICanReceiveOrder
//{

//}