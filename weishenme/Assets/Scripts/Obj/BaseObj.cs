using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����״̬
/// </summary>
public class BaseState
{
    public virtual void Init()
    {

    }
}
/// <summary>
/// һ�������������Ϣ
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
    /// һϵ�е�״̬
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
    /// ����,����һ��״̬,���һ��״̬
    /// </summary>
    public abstract T Update(T input, T output);
    /// <summary>
    /// /��ȡ��ǰ״̬
    /// </summary>
    /// <returns></returns>
    public T getNow()
    {
        return stateSeq[0];//��ǰ״̬
    }

}

/// <summary>
/// �����¶���
/// </summary>
public interface IOrderUser
{
    /// <summary>
    /// ���Ǯ
    /// </summary>
    /// <param name="money"></param>
    public void addMoney(BaseState state,Float money);
    /// <summary>
    /// ����Ǯ
    /// </summary>
    /// <param name="money"></param>
    public void reduceMoney(BaseState state, Float money);
}
//���Խ�ȡ����,��ȡ����Ӧ�þͿ���������
public interface ICanReceiveOrder: IOrderUser
{
    ////����Ǯ֮�����һϵ�д���
    //public void receiveMoney(Float money, GoodsOrderInf goodsInf);
}
/// <summary>
/// ���Խ���������Ʒ�Ķ���
/// </summary>
public interface ICanReceiveNormalGoodsOrder:ICanReceiveOrder
{
    public void receiveMoney(Float money, NormalGoodsInf goodsInf);

    public void registerReveiveGoodsOrder();
    public void unregisterReveiveGoodsOrder();
}
/// <summary>
/// ���Խ��������������Ķ���
/// </summary>
public interface ICanReceiveProdOrder : ICanReceiveOrder
{
    public void receiveMoney(Float money, ProdGoodsInf goodsInf);
    public void registerReveiveProdsOrder();
    public void unregisterReveiveProdsOrder();
}

/// <summary>
/// ����������Ʒ�Ķ���
/// </summary>
public interface INormalGoodsOrderUser:IOrderUser
{
    /// <summary>
    /// ��ȡ��Ʒ�б�,�����ӽ���
    /// </summary>
    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state);

    public CircularQueue<Float> GetPipline(BaseState state);
}

/// <summary>
/// ���������������Ķ���
/// </summary>
public interface IProdOrderUser : IOrderUser
{
    public Float GetProdState(BaseState state);
}
/// <summary>
/// ������������Ʒ
/// </summary>
public interface IRequestPathUser:IOrderUser
{

}

public interface ICanReceivePathOrder: ICanReceiveOrder
{

}