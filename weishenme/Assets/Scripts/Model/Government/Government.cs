using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentState:BaseState
{
    /// <summary>
    /// ◊‹ ’»Î
    /// </summary>
    public double money;

    public GovernmentState(BaseObj obj) : base(obj)
    {
    }
}

public class GovernmentEc : EconomicInf
{
    public GovernmentEc(BaseObj obj) : base(obj)
    {
    }
}

public class GovernmentObj : BaseObj
{
    public override void InitBaseState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEconomicInf()
    {
        throw new System.NotImplementedException();
    }

    public override void Predict(BaseState input, int day)
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
