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
    public NpcObj a;
    /// <summary>
    /// 乙方
    /// </summary>
    public NpcObj b;
    public NpcObj ruleNpc;
    /// <summary>
    /// 协议同意时候的初始化
    /// </summary>
    /// <param name="baseState"></param>
    public Contract(NpcObj a, NpcObj b, NpcObj ruleNpc)
    {
        this.a = a;
        this.b = b;
        this.ruleNpc = ruleNpc;
    }
}

/// <summary>
/// 每天都需要执行的协议
/// </summary>
public abstract class EveryDayContract:Contract
{
    protected EveryDayContract(NpcObj a, NpcObj b, NpcObj ruleNpc) : base(a, b, ruleNpc)
    {
    }

    public abstract void DayUpdate();
}


/// <summary>
/// 一次执行的协议
/// </summary>
public abstract class OnceDayContract : Contract
{
    protected OnceDayContract(NpcObj a, NpcObj b, NpcObj ruleNpc) : base(a, b, ruleNpc)
    {
    }
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
    public void GetMoney()
    {
        
    }
}

