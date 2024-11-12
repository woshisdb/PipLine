using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Э��,ֻ��NPC��Ч,�������һϵ�е�Action
/// </summary>
public abstract class Contract
{
    /// <summary>
    /// �׷�
    /// </summary>
    public NpcObj a;
    /// <summary>
    /// �ҷ�
    /// </summary>
    public NpcObj b;
    public NpcObj ruleNpc;
    /// <summary>
    /// Э��ͬ��ʱ��ĳ�ʼ��
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
/// ÿ�춼��Ҫִ�е�Э��
/// </summary>
public abstract class EveryDayContract:Contract
{
    protected EveryDayContract(NpcObj a, NpcObj b, NpcObj ruleNpc) : base(a, b, ruleNpc)
    {
    }

    public abstract void DayUpdate();
}


/// <summary>
/// һ��ִ�е�Э��
/// </summary>
public abstract class OnceDayContract : Contract
{
    protected OnceDayContract(NpcObj a, NpcObj b, NpcObj ruleNpc) : base(a, b, ruleNpc)
    {
    }
}


/// <summary>
/// Э���б�
/// </summary>
public class ContractList
{
    /// <summary>
    /// ���Ĺ�������
    /// </summary>
    public IncomeType incomeType;
    /// <summary>
    /// ������ڹ���Э��
    /// </summary>
    public bool hasWorkContract { get { return workContract!=null; } }

    public bool isSelfEmploy;

    /// <summary>
    /// ����Э��,�ܹ�����׬Ǯ,ֻ����һ��
    /// </summary>
    public WorkContract workContract;
    public void GetMoney()
    {
        
    }
}

