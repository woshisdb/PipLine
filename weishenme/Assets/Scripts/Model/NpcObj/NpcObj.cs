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
}

public class EcState
{
    /// <summary>
    /// 是否需要工作
    /// </summary>
    public bool needWork;
    /// <summary>
    /// 是否允许寻找工作
    /// </summary>
    public bool allowFindJob;
    /// <summary>
    /// 允许执行跑动的行为
    /// </summary>
    public bool allowGo;
    /// <summary>
    /// 允许购买商品
    /// </summary>
    public bool allowBuyThing;
    /// <summary>
    /// 是否是自由人
    /// </summary>
    public bool isFreedom { get { return belong == null; } }
    /// <summary>
    /// 所属于的人
    /// </summary>
    public NpcState belong;
    /// <summary>
    /// 金钱
    /// </summary>
    public Float money;
    /// <summary>
    /// 签署的合约
    /// </summary>
    public ContractList contracts;
    /// <summary>
    /// 一系列的生产资料
    /// </summary>
    public List<EmploymentFactory> EmploymentFactories;
    public List<SourceEmploymentFactory> SourceEmploymentFactories;
    public List<TransGoodsFactory> transGoodsFactories;
    ///// <summary>
    ///// 自己的家
    ///// </summary>
    //public HouseBuildingState homeState;
    /// <summary>
    /// 收入的类型
    /// </summary>
    public IncomeType incomeEnum;
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
    //****************************************
    /// <summary>
    /// 获得当前的NPCState的情况
    /// </summary>
    /// <returns></returns>
    public float Rate()
    {
        float ret = 0;
        foreach (var building in ecState.EmploymentFactories)
        {
            //ret+=building.Rate();//获取当前每个生产资料的总收入
        }
        return ret;
    }
    public NpcState(BaseObj obj):base(obj)
    {
        ecState.money = 0;
    }
    public override void Init()
    {
        base.Init();
        ecState.money = 0;
    }
}

public class NpcObj : BaseObj,INpc
{
    public NeedWork needWork;
    public NpcState now { get { return (NpcState)getNow(); } }
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
        var works = RegisterReceiveWork();
        foreach (var work in works)
        {
            Market.Instance.Register(work);
        }
    }
    public override void Predict(BaseState input, int day)
    {

    }

    public override void InitBaseState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEconomicInf()
    {
        throw new System.NotImplementedException();
    }

    public NeedWork[] RegisterReceiveWork()
    {
        return new NeedWork[] { needWork };
    }

    public NeedWork[] UnRegisterReceiveWork()
    {
        return new NeedWork[] { needWork};
    }

    public SceneObj nowPos()
    {
        return now.sceneObj;
    }

    public NpcObj GetNpc()
    {
        return (NpcObj)now.ecState.belong.GetObj();
    }

    public void addMoney(Float money)
    {
        throw new System.NotImplementedException();
    }

    public void reduceMoney(Float money)
    {
        throw new System.NotImplementedException();
    }

    public override string ShowString()
    {
        throw new System.NotImplementedException();
    }
}
