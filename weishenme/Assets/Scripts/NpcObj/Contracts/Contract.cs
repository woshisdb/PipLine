using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 协议,只对NPC有效,里面包含一系列的Action
/// </summary>
public abstract class Contract
{
    /// <summary>
    /// 甲方
    /// </summary>
    public NpcState a;
    /// <summary>
    /// 乙方
    /// </summary>
    public NpcState b;
    public NpcState ruleNpc;
    /// <summary>
    /// 协议同意时候的初始化
    /// </summary>
    /// <param name="baseState"></param>
    public virtual void Init(NpcState a,NpcState b,NpcState ruleNpc)
    {
        this.a = a;
        this.b = b;
        this.ruleNpc = ruleNpc;
    }
    /// <summary>
    /// 判断协议是否生效
    /// </summary>
    /// <returns></returns>
    public abstract bool Condition();
    /// <summary>
    /// B破坏协议的影响
    /// </summary>
    public abstract void BBreak();
    /// <summary>
    /// A破坏协议的影响
    /// </summary>
    public abstract void ABreak();
}

/// <summary>
/// 每过多久更新一次
/// </summary>
public abstract class CircleContract:Contract
{
    /// <summary>
    /// 更新倒计时的时间
    /// </summary>
    /// <returns></returns>
    public abstract int EndCircle();
    /// <summary>
    /// 当天的更新
    /// </summary>
    public abstract void DayUpdate();
}

/// <summary>
/// 每天都需要执行的协议
/// </summary>
public abstract class EveryDayContract:Contract
{
    public int EndCircle()
    {
        return 0;
    }
}


/// <summary>
/// 一次执行的协议
/// </summary>
public abstract class OnceDayContract : Contract
{
}


/// <summary>
/// 协议列表
/// </summary>
public class ContractList
{
    /// <summary>
    /// 他的工作类型
    /// </summary>
    public IncomeType incomeType;
    /// <summary>
    /// 如果存在工作协议
    /// </summary>
    public bool hasWorkContract { get { return workContract!=null; } }

    public bool isSelfEmploy;

    /// <summary>
    /// 工作协议,能够用来赚钱,只能有一个
    /// </summary>
    public WorkContract workContract;
    /// <summary>
    /// 工厂所属协议
    /// </summary>
    public List<IOwnerEmploymentFactoryContract> buildingsBelongContract;
    public void GetMoney()
    {
        
    }
}

