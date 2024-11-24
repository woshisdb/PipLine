using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContractState
{
    hasMatch,//有工作
    tryMatch,//试着找工作
    noNeedMatch,//没有注册工作
}

public class WorkContract:EveryDayContract
{
    /// <summary>
    /// 工作状态
    /// </summary>
    public ContractState state;
    public NeedWork needWork;
    public SendWork sendWork;
    /// <summary>
    /// A给B的收入
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
    /// 工作状态
    /// </summary>
    public ContractState state;
    public NeedGoods needGoods;
    public SendGoods sendGoods;
    /// <summary>
    /// A给B的收入
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
/// 可以提供工作
/// </summary>
public class NeedWork
{
    /// <summary>
    /// 工作状态
    /// </summary>
    public ContractState state;
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
    public float satifyRate(SendWork sendWork)
    {
        return obj.GetNeedWorkRate(sendWork);
    }
    /// <summary>
    /// 对距离的满足度
    ///// </summary>
    //public Func<SendWork, float> pathSatifyRate;
    ///// <summary>
    ///// 对金钱的满足程度
    ///// </summary>
    //public Func<SendWork, float> moneySatify;
    ///// <summary>
    ///// 对工作内容本身的满足度
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
        //    var goTime = inf.Item1*2;//通勤时间
        //    return ((float)(goTime+e.workTime))/24f;//总的工作时间
        //});
        //workStateSatify = new Func<SendWork, float>(e =>
        //{
        //    return (float)(3*24-e.wastEnerge);
        //});
    }
}
/// <summary>
/// 可以要求工作
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
    public float satifyRate(NeedWork needWork)
    {
        return obj.GetSendWorkRate(needWork);
    }
    public SendWork(ISendWork sender)
    {
        obj = sender;
        //satifyRate = new Func<NeedWork, float>(e => { return 1; });//都为1
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
    public GoodsEnum goods;
    /// <summary>
    /// 每天期望商品提供的数目
    /// </summary>
    public Int remainSum;
    /// <summary>
    /// 想要到手的钱
    /// </summary>
    public float minMoney;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public SceneObj scene { get { return obj.nowPos(); } }
    /// <summary>
    /// 接收对象
    /// </summary>
    public ISendGoods obj;
    /// <summary>
    /// 是否能满足
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
    Continue,//持续需求
    Once//一次性需求
}

/// <summary>
/// 可以发送订单
/// </summary>
public class NeedGoods
{
    /// <summary>
    /// 商品
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// 每天期望商品提供的数目
    /// </summary>
    public int needSum;
    /// <summary>
    /// 所住的位置
    /// </summary>
    public SceneObj scene { get { return obj.aimPos(); } }
    /// <summary>
    /// 传输商品
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// 最大的钱
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// 是否能满足
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
