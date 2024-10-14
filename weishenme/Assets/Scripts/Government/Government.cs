using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentState:BaseState
{
    /// <summary>
    /// ◊‹ ’»Î
    /// </summary>
    public double money;
}

public class GovernmentEc:EconomicInf
{

}

public class GovernmentObj : BaseObj<GovernmentState, GovernmentEc>
{
    public override GovernmentState Update(GovernmentState input, GovernmentState output)
    {
        return output;
    }
}
