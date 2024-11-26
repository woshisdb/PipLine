using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentState:BaseState
{
    /// <summary>
    /// ◊‹ ’»Î
    /// </summary>
    public Float money;

    public GovernmentState(BaseObj obj) : base(obj)
    {
        money=new Float(0);
    }
}

public class GovernmentEc : EconomicInf
{
    public GovernmentEc(BaseObj obj) : base(obj)
    {
    }
}

public class GovernmentObj : BaseObj, IMarketUser
{
    public GovernmentObj():base()
    {

    }
    public void addMoney(Float money)
    {
        throw new System.NotImplementedException();
    }

    public Float getMoney()
    {
        throw new System.NotImplementedException();
    }

    public NpcObj GetNpc()
    {
        throw new System.NotImplementedException();
    }

    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }

    public override void InitBaseState()
    {
        state = new GovernmentState(this);
    }

    public override void InitEconomicInf()
    {
        ecInf = new GovernmentEc(this);
    }

    public override void Predict(BaseState input, int day)
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

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
