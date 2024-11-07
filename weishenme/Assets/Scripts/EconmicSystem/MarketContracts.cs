using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///// <summary>
///// ѭ��������ƷЭ��
///// </summary>
//public class RequestGoodsContract: CircleContract
//{
//    /// <summary>
//    /// ��ʼʱ��
//    /// </summary>
//    public int beginTime;
//    /// <summary>
//    /// ѭ������
//    /// </summary>
//    public int circle;
//    /// <summary>
//    /// ��Ҫ���ѵ�Ǯ
//    /// </summary> 
//    public int dealMoney;
//    /// <summary>
//    /// ��Ҫ��ȡ��Ʒ����Ϣ
//    /// </summary>
//    public GoodsObj goodsInf;
//    public override void ABreak()
//    {
//    }
//    public override void BBreak()
//    {
//        //��Ǯ�˻�ȥ
//    }
//    public override bool Condition()
//    {
//        return true;
//    }
//    public override void DayUpdate()
//    {
//    }
//    public override int EndCircle()
//    {

//    }
//    public override void Init(NpcState a, NpcState b, NpcState ruleNpc)
//    {
//        base.Init(a, b, ruleNpc);
//        beginTime = TimeSystem.Instance.nowDay;//��ǰ������
//    }
//}
///// <summary>
///// ���Խ�����Ʒ����Ľ�����Ϣ
///// </summary>
//public class CanReceiveGoodsContract
//{
//    /// <summary>
//    /// ��Ʒ����������
//    /// </summary>
//    public Func<GoodsEnum> goods;
//    /// <summary>
//    /// ÿ���¿����ṩ�������Ʒ����Ŀ
//    /// </summary>
//    public Func<int> maxSum;
//    /// <summary>
//    /// ��ǰ��Ʒ�����ٿɻ�ü۸�
//    /// </summary>
//    public Func<float> minPrice;
//}

/// <summary>
/// ���Խ��չ���
/// </summary>
public class ReceiveWork
{
    /// <summary>
    /// npc
    /// </summary>
    public IReceiveWork receiver;
    /// <summary>
    /// ���ٸ��ҵ�Ǯ
    /// </summary>
    public Func<Float> minPrice;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class SendWork
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<BuildingObj> building;
    public ISendWork obj;
    /// <summary>
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
}
/// <summary>
/// ������Ʒ
/// </summary>
public class ReceiveGoods
{
    /// <summary>
    /// npc
    /// </summary>
    public Func<NpcObj> npc;
    /// <summary>
    /// ���ٸ��ҵ�Ǯ
    /// </summary>
    public Func<Float> minPrice;
    public IReceiveGoods obj;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class SendGoods
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<BuildingObj> building;
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public ISendGoods obj;

    /// <summary>
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
}