using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����״̬
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
/// һ�������������Ϣ
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
    /// ����,����һ��״̬,���һ��״̬
    /// </summary>
    public abstract void Update(BaseState input);
    /// <summary>
    /// ���ݵ�ǰ��ֵȥԤ��һ���Լ�δ����״̬
    /// </summary>
    /// <param name="input"></param>
    /// <param name="day"></param>
    public abstract void Predict(BaseState input,int day);
    /// <summary>
    /// /��ȡ��ǰ״̬
    /// </summary>
    /// <returns></returns>
    public BaseState getNow()
    {
        return state;
    }

}

///// <summary>
///// �����¶���
///// </summary>
//public interface IOrderUser
//{
//    /// <summary>
//    /// ���Ǯ
//    /// </summary>
//    /// <param name="money"></param>
//    public void addMoney(BaseState state,Float money);
//    /// <summary>
//    /// ����Ǯ
//    /// </summary>
//    /// <param name="money"></param>
//    public void reduceMoney(BaseState state, Float money);
//}
////���Խ�ȡ����,��ȡ����Ӧ�þͿ���������
//public interface ICanReceiveOrder: IOrderUser
//{
//    ////����Ǯ֮�����һϵ�д���
//    //public void receiveMoney(Float money, GoodsOrderInf goodsInf);
//}
///// <summary>
///// ���Խ���������Ʒ�Ķ���
///// </summary>
//public interface ICanReceiveNormalGoodsOrder:ICanReceiveOrder
//{
//    public void receiveMoney(Float money, NormalGoodsInf goodsInf);

//    public void registerReveiveGoodsOrder();
//    public void unregisterReveiveGoodsOrder();
//}
///// <summary>
///// ���Խ��������������Ķ���
///// </summary>
//public interface ICanReceiveProdOrder : ICanReceiveOrder
//{
//    public void receiveMoney(Float money, ProdGoodsInf goodsInf);
//    public void registerReveiveProdsOrder();
//    public void unregisterReveiveProdsOrder();
//}

///// <summary>
///// ����������Ʒ�Ķ���
///// </summary>
//public interface INormalGoodsOrderUser:IOrderUser
//{
//    /// <summary>
//    /// ��ȡ��Ʒ�б�,�����ӽ���
//    /// </summary>
//    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state);

//    public CircularQueue<Float> GetPipline(BaseState state);
//}

///// <summary>
///// ���������������Ķ���
///// </summary>
//public interface IProdOrderUser : IOrderUser
//{
//    public Float GetProdState(BaseState state);
//}
///// <summary>
///// ������������Ʒ
///// </summary>
//public interface IRequestPathUser:IOrderUser
//{

//}

//public interface ICanReceivePathOrder: ICanReceiveOrder
//{

//}