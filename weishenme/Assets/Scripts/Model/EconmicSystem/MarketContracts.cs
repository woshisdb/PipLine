using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ContractState
{
    hasMatch,//�й���
    tryMatch,//�����ҹ���
    noNeedMatch,//û��ע�Ṥ��
}

public class WorkContract:EveryDayContract
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public ContractState state;
    public NeedWork needWork;
    public SendWork sendWork;
    /// <summary>
    /// A��B������
    /// </summary>
    public Float money;
    public WorkContract(NeedWork needWork, SendWork sendWork):base(needWork.obj.GetNpc(),sendWork.obj.GetNpc(),null)
    {
        this.needWork = needWork;
        this.sendWork = sendWork;
        this.money = (needWork.minMoney + sendWork.maxMoney) / 2;
    }

    public override void DayUpdate()
    {

    }
}


/// <summary>
/// �����ṩ����
/// </summary>
public class NeedWork : IEffectShort
{
    /// <summary>
    /// ����
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// ��������͹���
    /// </summary>
    public float minMoney;
    public SendWork sender;
    public ProdEnum prodEnum;
    public float predMoney;
    public bool hasWork { get { return sender!=null; } }
    /// <summary>
    /// �����
    /// </summary>
    public float satifyRate(SendWork sendWork)
    {
        return obj.GetNeedWorkRate(sendWork);
    }
    public SceneObj scene()
    {
        return obj.nowPos();
    }
    public void GetSend(SendWork sendWork)
    {
        this.sender = sendWork;
    }
    public void LostSend(SendWork sendWork)
    {
        this.sender = null;
    }

    public float effect()
    {
        if (hasWork)
        {
            return 0;
        }
        else
        {
            return sender.maxMoney;//�Լ�������
        }
    }

    public NeedWork()
    {
    }
    public NeedWork(INeedWork needer)
    {
        obj = needer;
    }
}
/// <summary>
/// ����Ҫ����
/// </summary>
public class SendWork
{
    /// <summary>
    /// ���ͷ�
    /// </summary>
    public ISendWork obj;
    public ProdEnum prodEnum;
    /// <summary>
    /// һ�����Ĺ���
    /// </summary>
    public Float maxMoney;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public int workTime;
    public float allRate;//������Ҫ��ֵ
    public List<NeedWork> needWorks;
    /// <summary>
    /// �����
    /// </summary>
    public float satifyRate(NeedWork needWork)
    {
        return obj.GetSendWorkRate(needWork);
    }
    public SendWork(ISendWork sender)
    {
        obj = sender;
        maxMoney = new Float(0);
        needWorks=new List<NeedWork>();
    }
    public void AddNeeder(NeedWork needWork)
    {
        if (remainRate > 0)
        {
            needWorks.Add(needWork);
            needWork.GetSend(this);
        }
    }
    public void RemoveNeeder(NeedWork needWork)
    {
        needWorks.Remove(needWork);
        needWork.LostSend(this);
    }
    [ShowInInspector,ReadOnly]
    public float remainRate { get {//ʣ��Ŀ�λ
            float sum = 0;
            foreach(var needWork in needWorks)
            {
                sum+= ProdManager.Instance.TestProd(needWork.obj, prodEnum)* workTime;
            }
            return allRate-sum; } }
    [ShowInInspector, ReadOnly]
    public float getRate
    {
        get
        {
            float sum = 0;
            foreach (var needWork in needWorks)
            {
                sum += ProdManager.Instance.TestProd(needWork.obj, prodEnum) * workTime;
            }
            return sum;
        }
    }
    public SceneObj scene()
    {
        return obj.aimPos();
    }
}
/// <summary>
/// ������Ʒ
/// </summary>
public class SendGoods
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// ÿ��������Ʒ�ṩ����Ŀ
    /// </summary>
    public Int remainSum;
    /// <summary>
    /// ��Ҫ���ֵ�Ǯ
    /// </summary>
    public float minMoney;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public SceneObj scene { get { return obj.nowPos(); } }
    /// <summary>
    /// ���ն���
    /// </summary>
    public ISendGoods obj;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public float sortVal;
    public float satifyRate(NeedGoods needGoods)
    {
        return obj.SendGoodsSatifyRate(needGoods);
    }
    public SendGoods(ISendGoods sender)
    {
        obj= sender;
    }
}

public enum NeedGoodsEnum
{
    Continue,//��������
    Once//һ��������
}

/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class NeedGoods:IEffectShort
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// ÿ��������Ʒ�ṩ����Ŀ
    /// </summary>
    public int needSum;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public SceneObj scene { get { return obj.aimPos(); } }
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// ����Ǯ
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public float satifyRate(SendGoods sendGoods)
    {
        return obj.NeedGoodsSatifyRate(sendGoods);
    }

    public float effect()
    {
        return maxMoney;
    }

    public NeedGoods(INeedGoods needer)
    {
        obj = needer;
        goods = GoodsEnum.goods2;
    }
}
