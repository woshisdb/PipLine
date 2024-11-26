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
    public SendWork sender;
    public ProdEnum prodEnum;
    public bool hasWork { get { return sender!=null; } }
    /// <summary>
    /// �����
    /// </summary>
    public float satifyRate(SendWork sendWork)
    {
        return obj.GetNeedWorkRate(sendWork);
    }
    /// <summary>
    /// �Ծ���������
    ///// </summary>
    //public Func<SendWork, float> pathSatifyRate;
    ///// <summary>
    ///// �Խ�Ǯ������̶�
    ///// </summary>
    //public Func<SendWork, float> moneySatify;
    ///// <summary>
    ///// �Թ������ݱ���������
    ///// </summary>
    //public Func<SendWork, float> workStateSatify;
    //public Func<float[]> getRates;
    public NeedWork()
    {
        //pathSatifyRate = f1;
        //moneySatify= f2;
        //workStateSatify = f3;
        //satifyRate = new Func<SendWork, float>(e => {
        //    var path=pathSatifyRate(e);
        //    var money = moneySatify(e);
        //    var work = workStateSatify(e);
        //    var satRate=Math.Min(path,Math.Min(money, work)); 
        //    if(satRate<=0)
        //    return 0;
        //    var rates=getRates();
        //    satRate = path* rates[0] + money* rates[1] + work* rates[2];
        //    return satRate;
        //});
    }
    public NeedWork(INeedWork needer)
    {
        obj = needer;
        //moneySatify = new Func<SendWork, float>(e =>
        //{
        //    return Market.Instance.PredicateRealWorkMoney(e,this) - minMoney;
        //});
        //pathSatifyRate = new Func<SendWork, float>(e =>
        //{
        //    var inf= Market.Instance.NpcDistanceCost(obj.nowPos(),e.obj.aimPos());
        //    var goTime = inf.Item1*2;//ͨ��ʱ��
        //    return ((float)(goTime+e.workTime))/24f;//�ܵĹ���ʱ��
        //});
        //workStateSatify = new Func<SendWork, float>(e =>
        //{
        //    return (float)(3*24-e.wastEnerge);
        //});
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
        if (rate > 0)
        {
            needWorks.Add(needWork);
            needWork.sender = this;//�Ѿ�ƥ����
        }
    }
    public void RemoveNeeder(NeedWork needWork)
    {
        needWorks.Remove(needWork);
        needWork.sender =null;
    }
    public float rate { get {//ʣ��Ŀ�λ
            float sum = 0;
            foreach(var needWork in needWorks)
            {
                sum+= ProdManager.Instance.TestProd(needWork.obj, prodEnum)* workTime;
            }
            return allRate-sum; } }
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
    public NeedGoods(INeedGoods needer)
    {
        obj = needer;
    }
}
