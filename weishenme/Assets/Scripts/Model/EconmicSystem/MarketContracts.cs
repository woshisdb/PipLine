using System;
using System.Collections;
using System.Collections.Generic;
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

public class GoodsContract : EveryDayContract
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public ContractState state;
    public NeedGoods needGoods;
    public SendGoods sendGoods;
    /// <summary>
    /// A��B������
    /// </summary>
    public Float money;
    public GoodsContract(NeedGoods needGoods, SendGoods sendGoods) : base(needGoods.obj.GetNpc(), sendGoods.obj.GetNpc(), null)
    {
        this.needGoods = needGoods;
        this.sendGoods = sendGoods;
        this.money = (needGoods.maxMoney + sendGoods.minMoney) / 2;
    }

    public override void DayUpdate()
    {

    }
}

/// <summary>
/// �����ṩ����
/// </summary>
public class NeedWork
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public ContractState state;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ����
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// ��������͹���
    /// </summary>
    public float minMoney;
    /// <summary>
    /// �����
    /// </summary>
    public Func<SendWork, float> satifyRate;
    /// <summary>
    /// �Ծ���������
    /// </summary>
    public Func<SendWork, float> pathSatifyRate;
    /// <summary>
    /// �Խ�Ǯ������̶�
    /// </summary>
    public Func<SendWork, float> moneySatify;
    /// <summary>
    /// �Թ������ݱ���������
    /// </summary>
    public Func<SendWork, float> workStateSatify;
    public Func<float[]> getRates;
    public NeedWork(Func<SendWork, float> f1, Func<SendWork, float> f2, Func<SendWork, float> f3)
    {
        pathSatifyRate = f1;
        moneySatify= f2;
        workStateSatify = f3;
        satifyRate = new Func<SendWork, float>(e => {
            var path=pathSatifyRate(e);
            var money = moneySatify(e);
            var work = workStateSatify(e);
            var satRate=Math.Min(path,Math.Min(money, work)); 
            if(satRate<=0)
            return 0;
            var rates=getRates();
            satRate = path* rates[0] + money* rates[1] + work* rates[2];
            return satRate;
        });
    }
    public NeedWork()
    {
        moneySatify = new Func<SendWork, float>(e =>
        {
            return Market.Instance.PredicateRealWorkMoney(e,this) - minMoney;
        });
        pathSatifyRate = new Func<SendWork, float>(e =>
        {
            var inf= Market.Instance.NpcDistanceCost(obj.nowPos(),e.obj.aimPos());
            var goTime = inf.Item1*2;//ͨ��ʱ��
            return ((float)(goTime+e.workTime))/24f;//�ܵĹ���ʱ��
        });
        workStateSatify = new Func<SendWork, float>(e =>
        {
            return (float)(3*24-e.wastEnerge);
        });
    }
}
/// <summary>
/// ����Ҫ����
/// </summary>
public class SendWork
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ���ͷ�
    /// </summary>
    public ISendWork obj;
    /// <summary>
    /// ���ĸ��Ĺ���
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public int workTime;
    /// <summary>
    /// ���ѵľ���
    /// </summary>
    public int wastEnerge;
    /// <summary>
    /// �����
    /// </summary>
    public Func<NeedWork, float> satifyRate;
    public SendWork()
    {
        satifyRate = new Func<NeedWork, float>(e => { return 1; });//��Ϊ1
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
    public int remainSum;
    /// <summary>
    /// ��Ҫ���ֵ�Ǯ
    /// </summary>
    public float minMoney;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ���ն���
    /// </summary>
    public ISendGoods obj;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public Func<NeedGoods, float> satifyRate;
    public SendGoods()
    {
        satifyRate = new Func<NeedGoods, float>(e =>
        {
            return 1;
        });
    }
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class NeedGoods
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
    public Func<SceneObj> scene;
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
    public Func<SendGoods, float> satifyRate;
    public Func<SendGoods, float> satifySumRate;
    public Func<SendGoods,float> satifyMoneyRate;
    public Func<float[]> getRates;
    public NeedGoods()
    {
        satifySumRate = new Func<SendGoods, float>(e =>
        {
            return e.remainSum - needSum;
        });
        satifyMoneyRate = new Func<SendGoods, float>(e => {
            return maxMoney- e.minMoney;
        });
        satifyRate = new Func<SendGoods, float>(e =>
        {
            var t = getRates();
            var sum = satifySumRate(e);
            var money = satifyMoneyRate(e);
            var satRate = Math.Min(sum,money);
            if(satRate <= 0)
                return 0;
            return t[0]* sum + t[1]* money;
        });
    }
}
