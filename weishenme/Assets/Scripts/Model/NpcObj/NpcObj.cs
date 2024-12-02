using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物理状态
public class PhysicalState
{
    /// <summary>
    /// 个人状态,剩余的体力,当前的体力状态
    /// </summary>
    public float remainEnerge;//决定当前生产力
    /// <summary>
    /// 个人状态,身体情况,饥饿导致下降,每一天下降1点,到0则
    /// </summary>
    public int strength;
    public NpcObj npcObj;
    /// <summary>
    /// 自己所呆的地方
    /// </summary>
    public HouseBuildingObj livePlace;
    public PhysicalState(NpcObj obj)
    {
        this.npcObj = obj;
    }
}
public class EcTimeLine
{
    /// <summary>
    /// 每日工资的数据
    /// </summary>
    public List<IEffectShort> baseMoneyList;
    /// <summary>
    /// 基本的每日收入
    /// </summary>
    public float baseMoney { get
        {
            float ret=0;
            foreach(var item in baseMoneyList)
            {
                ret += item.effect();
            }
            return ret;
        } }
    public class EcTimeLineItem
    {
        public int time;
        public EcTimeLine timeLine;
        public float value;
        public float getValue
        {
            get
            {
                return timeLine.baseMoney+value;
            }
        }
        public EcTimeLineItem(EcTimeLine timeLine, int time)
        {
            this.timeLine = timeLine;
            this.time = time;
        }
    }
    /// <summary>
    /// 循环队列
    /// </summary>
    public CircularQueue<EcTimeLineItem> ecInfos;
    public NpcObj npc;
    public EcTimeLine(NpcObj npc)
    {
        this.npc = npc;
        ecInfos = new CircularQueue<EcTimeLineItem>(30);
        for(int i=0;i<30;i++)
        {
            ecInfos.Enqueue(new EcTimeLineItem(this,i));
        }
        baseMoneyList = new List<IEffectShort>();
        ///工作会影响短期收入
        baseMoneyList.Add(npc.now.ecState.needWork);
        ///添加一系列的商品
        baseMoneyList.AddRange(npc.now.ecState.needGoods);
    }
    public float GetRate(int day)
    {
        return baseMoney * day;
    }
}
public class EcState
{
    /// <summary>
    /// 想要的一系列商品
    /// </summary>
    public List<NeedGoods> needGoods;
    public NeedWork needWork;
    /// <summary>
    /// 是否需要工作
    /// </summary>
    public bool needWorkB;
    /// <summary>
    /// 金钱
    /// </summary>
    public Float money;
    public NpcObj npcObj;
    public EcState(NpcObj npcObj)
    {
        this.npcObj = npcObj;
        money=new Float(0);
        needGoods=new List<NeedGoods>();
        var needGood = new NeedGoods(npcObj);
        needGoods.Add(needGood);
    }
}

public class NeedItem
{
    public NpcObj npc;
    public NeedItem(NpcObj npc)
    {
        this.npc = npc;
    }
}

public class JoyNeedItem : NeedItem
{
    /// <summary>
    /// 感官刺激
    /// </summary>
    public Float joyRate;
    /// <summary>
    /// 剩余的金钱
    /// </summary>
    public Float money { get { return npc.now.ecState.money; } }
    /// <summary>
    /// 剩余的能量
    /// </summary>
    public float remainEnerge { get { return npc.now.physicalState.remainEnerge; } }
    public JoyNeedItem(NpcObj npc) : base(npc)
    {

    }
}

/// <summary>
/// 短期能量需求
/// </summary>
public class ShortNeedItem:NeedItem
{
    /// <summary>
    /// 经济的时间线
    /// </summary>
    public EcTimeLine ecTimeLine;//关系到自己的收入
    /// <summary>
    /// 剩余的金钱
    /// </summary>
    public Float money { get { return npc.now.ecState.money; } }
    /// <summary>
    /// 剩余的能量
    /// </summary>
    public float remainEnerge { get { return npc.now.physicalState.remainEnerge; } }
    public ShortNeedItem(NpcObj npc) : base(npc)
    {

    }
    public float getRate()
    {
        return money+ecTimeLine.GetRate(10);//10天内的总收入 
    }
}
/// <summary>
/// 长期的收入
/// </summary>
public class LongTermMoney
{
    /// <summary>
    /// 金钱收入
    /// </summary>
    public Func<float> money;
}
/// <summary>
/// 对事业的需求
/// </summary>
public class LongNeedItem : NeedItem
{
    public LongTermMoney longTermMoney;
    public LongNeedItem(NpcObj npc) : base(npc)
    {

    }
}

public class DreamNeedItem : NeedItem
{
    public DreamNeedItem(NpcObj npc) : base(npc)
    {
    }
}

public class NeedState
{
    public NpcObj npcObj;
    public ShortNeedItem shortNeed;
    public LongNeedItem longNeed;
    public JoyNeedItem joyNeed;
    public DreamNeedItem dreamNeed;
    public NeedState(NpcObj npc)
    {
        npcObj = npc;
    }
    public float GetRate()
    {
        return shortNeed.getRate();
    }
}

public class NpcState:BaseState
{
    public SceneObj sceneObj;
    /// <summary>
    /// 经济
    /// </summary>
    public EcState ecState;
    /// <summary>
    /// 身体
    /// </summary>
    public PhysicalState physicalState;
    /// <summary>
    /// 需求状态
    /// </summary>
    public NeedState needState;
    public NpcState(BaseObj obj):base(obj)
    {
        ecState = new EcState((NpcObj)obj);
        physicalState = new PhysicalState((NpcObj)obj);
        needState = new NeedState((NpcObj)obj);
    }
}
public class NpcEc : EconomicInf
{
    public NpcEc(BaseObj obj) : base(obj)
    {
    }
}

public class NpcObj : BaseObj,INpc
{
    public NeedWork needWork { get { return now.ecState.needWork; } }
    public NpcState now { get { return (NpcState)getNow(); } }
    public void Init(SceneObj sceneObj)
    {
        now.sceneObj = sceneObj;
        ///注册一系列的订单
        var goods=RegisterNeedGoods();
        if(goods!=null)
        {
            foreach(var need in goods)
            {
                Market.Instance.Register(need);
            }
        }
        now.ecState.needWork = new NeedWork();
        now.ecState.needWork.obj = this;
        now.ecState.needWork.prodEnum = ProdEnum.prod1;
        Market.Instance.Register(RegisterNeedWork()[0] );
    }
    /// <summary>
    /// 更新每一个生产资料的数据,并更新完npc.根据contract
    /// </summary>
    /// <param name="input"></param>
    public override void Update()
    {
        
    }
    public void BefThink()
    {
        //遍历每一个协议
        //var works = RegisterNeedWork();
        //foreach (var work in works)
        //{
        //    Market.Instance.Register(work);
        //}
    }
    public override void Predict(BaseState input, int day)
    {

    }

    public override void InitBaseState()
    {
        state=new NpcState(this);
    }

    public override void InitEconomicInf()
    {
        ecInf=new NpcEc(this);
    }

    public NeedWork[] RegisterNeedWork()
    {
        return new NeedWork[] { needWork };
    }

    public NeedWork[] UnRegisterNeedWork()
    {
        return new NeedWork[] { needWork};
    }

    public SceneObj nowPos()
    {
        return now.sceneObj;
    }

    public NpcObj GetNpc()
    {
        return this;
    }

    public void addMoney(Float money)
    {
        now.ecState.money.value += money;
    }

    public void reduceMoney(Float money)
    {
        now.ecState.money.value -= money;
    }

    public override string ShowString()
    {
        return "";
    }

    public float GetNeedWorkRate(SendWork sendWork)
    {
        throw new System.NotImplementedException();
    }


    public NeedGoods[] RegisterNeedGoods()
    {
        return now.ecState.needGoods.ToArray();
    }

    public NeedGoods[] UnRegisterNeedGoods()
    {
        return now.ecState.needGoods.ToArray();
    }

    public SceneObj aimPos()
    {
        return now.sceneObj;
    }

    public float NeedGoodsSatifyRate(SendGoods sendGoods)
    {
        return 0.5f;
    }

    public Float getMoney()
    {
        return now.ecState.money;
    }

    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum)
    {

    }

    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }
    //位置为所住的位置
    public Vector2Int GetWorldPos()
    {
        return now.physicalState.livePlace.GetWorldPos();
    }

    public SceneObj GetSceneObj()
    {
        return now.sceneObj;
    }
}
