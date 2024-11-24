using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����¶���
/// </summary>
public interface IMarketUser
{
    /// <summary>
    /// ������NPC
    /// </summary>
    /// <returns></returns>
    public NpcObj GetNpc();
    /// <summary>
    /// ���Ǯ
    /// </summary>
    /// <param name="money"></param>
    public void addMoney(Float money);
    /// <summary>
    /// ����Ǯ
    /// </summary>
    /// <param name="money"></param>
    public void reduceMoney(Float money);

    public Float getMoney();
}

/// <summary>
/// ��������������
/// </summary>
public interface INeedWork : IMarketUser
{
    /// <summary>
    /// ע�����������һϵ�еĹ���
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public NeedWork[] RegisterNeedWork();
    /// <summary>
    /// ��Ը����չ���Э��
    /// </summary>
    public NeedWork[] UnRegisterNeedWork();

    /// <summary>
    /// ��ǰλ��
    /// </summary>
    /// <returns></returns>
    public SceneObj nowPos();

    public float GetNeedWorkRate(SendWork sendWork);
}


/// <summary>
/// ���������������Ķ���
/// </summary>
public interface ISendWork : IMarketUser
{
    /// <summary>
    /// ��ȡ����������Ĵ���
    /// </summary>
    /// <param name="state"></param>
    /// <param name="prods"></param>
    public void GetProdProcess(INeedWork worker);
    /// <summary>
    /// ע��һϵ������������
    /// </summary>
    /// <returns></returns>
    public SendWork[] RegisterSendWork();
    /// <summary>
    /// ȡ��ע��һϵ������������
    /// </summary>
    /// <returns></returns>
    public SendWork[] UnRegisterSendWork();
    /// <summary>
    /// Ŀ��λ��
    /// </summary>
    /// <returns></returns>
    public SceneObj aimPos();
    public float GetSendWorkRate(NeedWork needWork);
}

/// <summary>
/// ���Թ�Ӷ�Լ�
/// </summary>
public interface ICanEmploySelf:IMarketUser
{
    /// <summary>
    /// ��Ӷ�Լ�
    /// </summary>
    public void EmploySelf();
    /// <summary>
    /// ����Ӷ�Լ�
    /// </summary>
    public void UnEmploySelf();
}

/// <summary>
/// ����������Ʒ�Ķ���
/// </summary>
public interface INeedGoods : IMarketUser
{
    /// <summary>
    /// ��ȡ��Ʒ�б�,�����ӽ���
    /// </summary>
    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum);
    public NeedGoods[] RegisterNeedGoods();
    public NeedGoods[] UnRegisterNeedGoods();
    public SceneObj aimPos();

    public float NeedGoodsSatifyRate(SendGoods sendGoods);
}

/// <summary>
/// ���Խ���������Ʒ�Ķ���
/// </summary>
public interface ISendGoods : IMarketUser
{
    /// <summary>
    /// ������Ʒ�Ķ���
    /// </summary>
    public SendGoods[] RegisterSendGoods();
    /// <summary>
    /// ȡ��������Ʒ����
    /// </summary>
    public SendGoods[] UnRegisterSendGoods();
    public SceneObj nowPos();

    public float SendGoodsSatifyRate(NeedGoods goods);
}

/// <summary>
/// ��������ת�ƶ���
/// </summary>
public interface ISendTransGoods : IMarketUser
{
    /// <summary>
    /// ������Ʒ�Ķ���
    /// </summary>
    public void RegisterRequestTransGoods();
    /// <summary>
    /// ȡ��������Ʒ����
    /// </summary>
    public void UnRegisterReveiveGoodsOrder();
}

/// <summary>
/// ���Դ���ת������
/// </summary>
public interface IReceiveTransGoods : IMarketUser
{
    /// <summary>
    /// ��ȡ��Ʒ�б�,�����ӽ���
    /// </summary>
    public void TransGoodsProcess(BaseState state, GoodsObj goods);
    public void RegisterProcessTransThings();
    public void UnRegisterProcessTransThings();
    /// <summary>
    /// ���˽���ת��
    /// </summary>
    /// <param name="state"></param>
    /// <param name="npcState"></param>
    public void OneDayTransNpc(BaseState state,NpcState npcState);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="npcState"></param>
    public void MoreDayTransNpc(BaseState state,NpcState npcState);
}


/// <summary>
/// ��������
/// </summary>
public interface IBuilding
{
    /// <summary>
    /// �����������������Լ�������,ͨ�����󶩵�,�������ʺ͹�������
    /// </summary>
    public abstract void MaxMoney();
}





/// <summary>
/// ��ͨ�Ĺ���,�ܹ�����������,Ȼ�������Ʒ,������Ʒ
/// </summary>
public interface EmploymentFactory : IBuilding,ISendWork,ISendGoods,INeedGoods
{

}
/// <summary>
/// ԭ���ϵĹ���,�ܹ�����������,Ȼ��,������Ʒ
/// </summary>
public interface SourceEmploymentFactory : IBuilding,ISendGoods,ISendWork
{

}
/// <summary>
/// ����������Ʒ�Ĺ���
/// </summary>
public interface FinalEmploymentFactory: IBuilding, ISendWork, ISendGoods, INeedGoods
{

}
/// <summary>
/// �г�,������NPC������Ʒ
/// </summary>
public interface MarketFactory:IBuilding, ISendWork, ISendGoods, INeedGoods
{

}
/// <summary>
/// ת����Ʒ���˵Ĺ���
/// </summary>
public interface TransGoodsFactory : IBuilding, ICanEmploySelf
{

}

public interface INpc:INeedWork,INeedGoods
{

}