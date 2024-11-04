using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����¶���
/// </summary>
public interface IContractUser
{
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
}

/// <summary>
/// ���������������Ķ���
/// </summary>
public interface ISendWorkContract : IContractUser
{
    /// <summary>
    /// ��Ƹ�˵ĸ���
    /// </summary>
    /// <returns></returns>
    public Int getProdSum();

    public void setProdSum(Int prodSum);
    /// <summary>
    /// ��ȡ����������Ĵ���
    /// </summary>
    /// <param name="state"></param>
    /// <param name="prods"></param>
    public void GetProdProcess(NpcObj npc);
    /// <summary>
    /// ע��һϵ������������
    /// </summary>
    /// <returns></returns>
    public void RegisterRequestWorkContract();
    /// <summary>
    /// ȡ��ע��һϵ������������
    /// </summary>
    /// <returns></returns>
    public void UnRegisterRequestWorkContract();
    /// <summary>
    /// Ŀ��λ��
    /// </summary>
    /// <returns></returns>
    public SceneObj aimPos();
}

/// <summary>
/// ��������������
/// </summary>
public interface ICanReceiveWorkContract : IContractUser
{
    /// <summary>
    /// ע�����������һϵ�еĹ���
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public void RegisterReceiveWorkContract();
    /// <summary>
    /// ��Ը����չ���Э��
    /// </summary>
    public void UnRegisterReceiveWorkContract();

    /// <summary>
    /// ѡ��һϵ�й����еĺ�������
    /// </summary>
    /// <param name="works"></param>
    /// <returns></returns>
    public void DecisionBestWork(WorkContract[] works);
    /// <summary>
    /// ��ǰλ��
    /// </summary>
    /// <returns></returns>
    public SceneObj nowPos();
}

/// <summary>
/// ���Թ�Ӷ�Լ�
/// </summary>
public interface ICanEmploySelf:IContractUser
{
    public NpcObj GetNpc();
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
/// ���Խ���������Ʒ�Ķ���
/// </summary>
public interface ICanReceiveGoodsContract : IContractUser
{
    /// <summary>
    /// ������Ʒ�Ķ���
    /// </summary>
    public void RegisterReveiveGoodsOrder();
    /// <summary>
    /// ȡ��������Ʒ����
    /// </summary>
    public void UnRegisterReveiveGoodsOrder();
}

/// <summary>
/// ����������Ʒ�Ķ���
/// </summary>
public interface ISendGoodsContract : IContractUser
{
    /// <summary>
    /// ��ȡ��Ʒ�б�,�����ӽ���
    /// </summary>
    public void GetGoodsProcess(BaseState state,GoodsEnum goodsEnum,int sum);

    public void RegisterRequestGoodsContract();
    public void UnRegisterRequestGoodsContract();
}


/// <summary>
/// ��������ת�ƶ���
/// </summary>
public interface ICanRequestTransGoods : IContractUser
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
public interface ICanProcessTransGoods : IContractUser
{
    /// <summary>
    /// ��ȡ��Ʒ�б�,�����ӽ���
    /// </summary>
    public void TransGoodsProcess(BaseState state, Goods goods);

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
/// �Ƿ����������������
/// </summary>
public interface ICanRaiseMoney
{
    /// <summary>
    /// Ԥ��ĳ�����ж��ٵ�����
    /// </summary>
    /// <returns></returns>
    public float Predicate(NpcObj npc);
    /// <summary>
    /// ��Ǯ��ѭ������
    /// </summary>
    /// <returns></returns>
    public int MoneyCircle();
}

/// <summary>
/// �Ƿ����������Ʒ
/// </summary>
public interface ICanGeneratePipline
{
    public void GeneratePipline(GoodsBuildingState state);
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
public interface EmploymentFactory : IBuilding,ISendWorkContract, ISendGoodsContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}


/// <summary>
/// ԭ���ϵĹ���,�ܹ�����������,Ȼ��,������Ʒ
/// </summary>
public interface SourceEmploymentFactory : IBuilding,ISendWorkContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}

/// <summary>
/// ת����Ʒ���˵Ĺ���
/// </summary>
public interface TransGoodsFactory : IBuilding,ISendWorkContract, ICanReceiveGoodsContract, ICanRequestTransGoods, ICanRaiseMoney, ICanEmploySelf
{

}

