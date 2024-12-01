using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物理状态
public class PhysicalState
{
    /// <summary>
    /// 个人状态,剩余的体力,当前的体力状态
    /// </summary>
    public int remainEnerge;//决定当前生产力
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

public class ProdState
{
    /// <summary>
    /// 一天的总时长
    /// </summary>
    public int allTime { get { return Meta.dayTime; } }
    /// <summary>
    /// 剩余自由活动时间
    /// </summary>
    public int remainTime;
    /// <summary>
    /// 想要工作的时间
    /// </summary>
    public int workTime;
    /// <summary>
    /// 前往工作地点的时间
    /// </summary>
    public int goWorkTime;
    /// <summary>
    /// 生活方式
    /// </summary>
    public LifeStyle lifeStyle;
    public NpcObj npcObj;
    public ProdState(NpcObj npc)
    {
        this.npcObj = npc;
    }
}
/// <summary>
/// 每个人的个人能力
/// </summary>
public class NPcPower
{

}

public class NpcState:BaseState
{
    public SceneObj sceneObj;
    /// <summary>
    /// 经济
    /// </summary>
    public EcState ecState;
    /// <summary>
    /// 生产力
    /// </summary>
    public ProdState prodState;
    /// <summary>
    /// 身体
    /// </summary>
    public PhysicalState physicalState;

    public NpcState(BaseObj obj):base(obj)
    {
        ecState = new EcState((NpcObj)obj);
        prodState = new ProdState((NpcObj)obj);
        physicalState = new PhysicalState((NpcObj)obj);
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
