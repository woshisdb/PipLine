using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ǯ�Ĺ���
/// </summary>
public class SaveMoneyAction : NpcAction
{
    public BankBuildingObj bank;
    public SaveMoneyAction(BankBuildingObj bank)
    {
        this.bank = bank;
    }

    public override bool Condition(NpcObj state)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(NpcObj state)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// ����Ĺ���
/// </summary>
public class LoanMoneyAction : NpcAction
{
    public BankBuildingObj bank; 
    public LoanMoneyAction(BankBuildingObj bank)
    {
        this.bank = bank;
    }

    public override bool Condition(NpcObj state)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(NpcObj state)
    {
        throw new System.NotImplementedException();
    }
}