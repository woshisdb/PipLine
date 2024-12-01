using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存钱的工作
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
/// 贷款的工作
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