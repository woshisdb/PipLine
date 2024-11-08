using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可以提供工作
/// </summary>
public class NeedWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 对象
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// 期望的最低工资
    /// </summary>
    public float minMoney;
    /// <summary>
    /// 满足度
    /// </summary>
    public Func<SendWork, float> satifyRate;
    /// <summary>
    /// 对距离的满足度
    /// </summary>
    public Func<SendWork, float> pathSatifyRate;
    /// <summary>
    /// 对金钱的满足程度
    /// </summary>
    public Func<SendWork, float> moneySatify;
    /// <summary>
    /// 对工作内容本身的满足度
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
            var goTime = inf.Item1*2;//通勤时间
            return ((float)(goTime+e.workTime))/24f;//总的工作时间
        });
        workStateSatify = new Func<SendWork, float>(e =>
        {
            return (float)(3*24-e.wastEnerge);
        });
    }
}
/// <summary>
/// 可以发送订单
/// </summary>
public class SendWork
{
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 发送方
    /// </summary>
    public ISendWork obj;
    /// <summary>
    /// 最多的给的工资
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// 工作时间
    /// </summary>
    public int workTime;
    /// <summary>
    /// 花费的精力
    /// </summary>
    public int wastEnerge;
    /// <summary>
    /// 满足度
    /// </summary>
    public Func<NeedWork, float> satifyRate;
    public Func<SendWork, float> pathSatifyRate;
    public SendWork()
    {

    }
}
/// <summary>
/// 接收商品
/// </summary>
public class SendGoods
{
    /// <summary>
    /// 商品
    /// </summary>
    public GoodsObj goods;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 想要到手的钱
    /// </summary>
    public float minMoney;
    /// <summary>
    /// 接收对象
    /// </summary>
    public ISendGoods obj;
    /// <summary>
    /// 是否能满足
    /// </summary>
    public Func<NeedGoods, float> satifyRate;
}
/// <summary>
/// 可以发送订单
/// </summary>
public class NeedGoods
{
    /// <summary>
    /// 商品
    /// </summary>
    public GoodsObj goods;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// 传输商品
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// 最多可以付的钱
    /// </summary>
    public Func<Float> maxPrice;
    /// <summary>
    /// 最大的钱
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// 是否能满足
    /// </summary>
    public Func<SendGoods, float> satifyRate;
}
