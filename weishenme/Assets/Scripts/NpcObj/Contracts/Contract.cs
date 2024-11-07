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
    public NpcState a;
    /// <summary>
    /// �ҷ�
    /// </summary>
    public NpcState b;
    /// <summary>
    /// Э��ͬ��ʱ��ĳ�ʼ��
    /// </summary>
    /// <param name="baseState"></param>
    public abstract void Init();
    /// <summary>
    /// �ж�Э���Ƿ���Ч
    /// </summary>
    /// <returns></returns>
    public abstract bool Condition();
}


/// <summary>
/// ÿ�춼��Ҫִ�е�Э��
/// </summary>
public abstract class EveryDayContract:Contract
{
    /// <summary>
    /// ÿһ��ĸ���
    /// </summary>
    public abstract void DayUpdate();
    /// <summary>
    /// B�ƻ�Э���Ӱ��
    /// </summary>
    public abstract void BBreak();
    /// <summary>
    /// A�ƻ�Э���Ӱ��
    /// </summary>
    public abstract void ABreak();
}

/// <summary>
/// һ��ִ�е�Э��
/// </summary>
public abstract class OnceDayContract : Contract
{
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
    /// <summary>
    /// ��������Э��
    /// </summary>
    public List<IOwnerEmploymentFactoryContract> buildingsBelongContract;
    public void GetMoney()
    {
        
    }
}
