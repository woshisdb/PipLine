using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����״̬
public class PhysicalState
{
    /// <summary>
    /// ����״̬,ʣ�������,��ǰ������״̬
    /// </summary>
    public float remainEnerge;//������ǰ������
    /// <summary>
    /// ����״̬,�������,���������½�,ÿһ���½�1��,��0��
    /// </summary>
    public int strength;
    public NpcObj npcObj;
    /// <summary>
    /// �Լ������ĵط�
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
    /// ÿ�չ��ʵ�����
    /// </summary>
    public List<IEffectShort> baseMoneyList;
    /// <summary>
    /// ������ÿ������
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
    /// ѭ������
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
        ///������Ӱ���������
        baseMoneyList.Add(npc.now.ecState.needWork);
        ///���һϵ�е���Ʒ
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
    /// ��Ҫ��һϵ����Ʒ
    /// </summary>
    public List<NeedGoods> needGoods;
    public NeedWork needWork;
    /// <summary>
    /// �Ƿ���Ҫ����
    /// </summary>
    public bool needWorkB;
    /// <summary>
    /// ��Ǯ
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
    /// �йٴ̼�
    /// </summary>
    public Float joyRate;
    /// <summary>
    /// ʣ��Ľ�Ǯ
    /// </summary>
    public Float money { get { return npc.now.ecState.money; } }
    /// <summary>
    /// ʣ�������
    /// </summary>
    public float remainEnerge { get { return npc.now.physicalState.remainEnerge; } }
    public JoyNeedItem(NpcObj npc) : base(npc)
    {

    }
}

/// <summary>
/// ������������
/// </summary>
public class ShortNeedItem:NeedItem
{
    /// <summary>
    /// ���õ�ʱ����
    /// </summary>
    public EcTimeLine ecTimeLine;//��ϵ���Լ�������
    /// <summary>
    /// ʣ��Ľ�Ǯ
    /// </summary>
    public Float money { get { return npc.now.ecState.money; } }
    /// <summary>
    /// ʣ�������
    /// </summary>
    public float remainEnerge { get { return npc.now.physicalState.remainEnerge; } }
    public ShortNeedItem(NpcObj npc) : base(npc)
    {

    }
    public float getRate()
    {
        return money+ecTimeLine.GetRate(10);//10���ڵ������� 
    }
}
/// <summary>
/// ���ڵ�����
/// </summary>
public class LongTermMoney
{
    /// <summary>
    /// ��Ǯ����
    /// </summary>
    public Func<float> money;
}
/// <summary>
/// ����ҵ������
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
    /// ����
    /// </summary>
    public EcState ecState;
    /// <summary>
    /// ����
    /// </summary>
    public PhysicalState physicalState;
    /// <summary>
    /// ����״̬
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
        ///ע��һϵ�еĶ���
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
    /// ����ÿһ���������ϵ�����,��������npc.����contract
    /// </summary>
    /// <param name="input"></param>
    public override void Update()
    {
        
    }
    public void BefThink()
    {
        //����ÿһ��Э��
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
    //λ��Ϊ��ס��λ��
    public Vector2Int GetWorldPos()
    {
        return now.physicalState.livePlace.GetWorldPos();
    }

    public SceneObj GetSceneObj()
    {
        return now.sceneObj;
    }
}
