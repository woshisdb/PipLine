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
    /// <summary>
    /// 自己的家
    /// </summary>
    public HouseBuildingState homeState;
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
            ret+=building.Rate();//获取当前每个生产资料的总收入
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

public class NpcObj : BaseObj
{
    public NpcState now { get { return (NpcState)getNow(); } }
    /// <summary>
    /// 重新思考接收的协议那些不能执行
    /// </summary>
    public void ReThinkContract(NpcState npcState)
    {

    }

    /// <summary>
    /// 根据当前世界去计算一个最好收入
    /// </summary>
    public void Plan(NpcState npcState)
    {

    }

    /// <summary>
    /// 对于固定工资情况进行更新,他除了身体外一无所有
    /// </summary>
    public void BefFixedSalaryUpdate()
    {
        if(now.ecState.contracts.hasWorkContract)//如果拥有工作
        {

        }
        else//如果不拥有工作则寻找工作
        {

        }
    }
    /// <summary>
    /// 自营经济的更新,他所拥有的个人生产资料一个人就满载了
    /// </summary>
    public void BefSelfEmployment()
    {
        var works = now.ecState;
        if(works.needWork)//如果需要工作则将生产力挂载到里面
        {
            if()//没有工作,那就去找
            {

            }
            else
            {

            }
        }
        else
        {
            //最大化中间加工商的收入
            foreach (var factory in works.EmploymentFactories)
            {
                ///最大化收入
                factory.MaxMoney();//确定收入
            }
            ///最大化资源出口的收入
            foreach (var factory in works.SourceEmploymentFactories)
            {
                ///最大化收入
                factory.MaxMoney();//确定收入
            }
            ///最大化转移商品的收入
            foreach (var factory in works.transGoodsFactories)
            {
                ///最大化收入
                factory.MaxMoney();
            }
        }
    }
    /// <summary>
    /// 他所拥有的生产资料,个人的工作对收入的影响比较大
    /// </summary>
    public void BefSelfAndOtherEmployment()
    {
        var works = now.ecState;
        //最大化中间加工商的收入
        foreach (var factory in works.EmploymentFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
        ///最大化资源出口的收入
        foreach (var factory in works.SourceEmploymentFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
        ///最大化转移商品的收入
        foreach (var factory in works.transGoodsFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
    }
    /// <summary>
    /// 自己进行资本收益的更新,自己的工作对收入的ing想不大
    /// </summary>
    public void BefCapitalGains()
    {
        var works = now.ecState;
        //最大化中间加工商的收入
        foreach (var factory in works.EmploymentFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
        ///最大化资源出口的收入
        foreach (var factory in works.SourceEmploymentFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
        ///最大化转移商品的收入
        foreach (var factory in works.transGoodsFactories)
        {
            ///最大化收入
            factory.MaxMoney();
        }
    }

    /// <summary>
    /// 更新每一个生产资料的数据,并更新完npc.根据contract
    /// </summary>
    /// <param name="input"></param>
    public override void Update(BaseState input)
    {
        //遍历每一个协议
        
    }
    public override void Predict(BaseState input, int day)
    {

    }
    public void addMoney(BaseState state, Float money)
    {
        ((NpcState)state).ecState.money += money;
    }
    public void receiveMoney(Float money, ProdGoodsInf goodsInf)
    {
        this.now.ecState.money += money;
    }
    public void reduceMoney(BaseState state, Float money)
    {
        ((NpcState)state).ecState.money -= money;
    }

    public void registerReveiveProdsOrder()
    {

    }

    public void unregisterReveiveProdsOrder()
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
}
