using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ṩ����
/// </summary>
public class NeedWork
{
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
            return Market.Instance.PredicateMoney(e,this) - minMoney;
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
/// ���Է��Ͷ���
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
    public Func<SendWork, float> pathSatifyRate;
    public SendWork()
    {

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
    public GoodsObj goods;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ��Ҫ���ֵ�Ǯ
    /// </summary>
    public float minMoney;
    /// <summary>
    /// ���ն���
    /// </summary>
    public ISendGoods obj;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public Func<NeedGoods, float> satifyRate;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class NeedGoods
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public GoodsObj goods;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
    /// <summary>
    /// ����Ǯ
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public Func<SendGoods, float> satifyRate;
}
