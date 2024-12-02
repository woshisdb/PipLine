using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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


/// <summary>
/// 可以提供工作
/// </summary>
public class NeedWork : IEffectShort
{
    /// <summary>
    /// 对象
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// 期望的最低工资
    /// </summary>
    public float minMoney;
    public SendWork sender;
    public ProdEnum prodEnum;
    public float predMoney;
    public bool hasWork { get { return sender!=null; } }
    /// <summary>
    /// 满足度
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
            return sender.maxMoney;//自己的收入
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
/// 可以要求工作
/// </summary>
public class SendWork
{
    /// <summary>
    /// 发送方
    /// </summary>
    public ISendWork obj;
    public ProdEnum prodEnum;
    /// <summary>
    /// 一个给的工资
    /// </summary>
    public Float maxMoney;
    /// <summary>
    /// 工作时间
    /// </summary>
    public int workTime;
    public float allRate;//所有需要的值
    public List<NeedWork> needWorks;
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
    public float remainRate { get {//剩余的空位
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
public class NeedGoods:IEffectShort
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
