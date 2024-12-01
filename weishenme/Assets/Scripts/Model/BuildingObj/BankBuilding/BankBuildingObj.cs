using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankBuildingState : BuildingState
{
    public BankBuildingState(BuildingObj buildingObj, BuildingEnum buildingEnum) : base(buildingObj, buildingEnum)
    {
    }
}

public class BankBuildingEc : BuildingEc
{
    public BankBuildingEc(BaseObj obj) : base(obj)
    {
    }
}

public class BankBuildingObj : BuildingObj
{
    public List<NpcAction> npcActions;
    public BankBuildingObj(BuildingEnum buildingEnum) : base()
    {
        var npcActions = new List<NpcAction>();
        npcActions.Add(new SaveMoneyAction(this));
        npcActions.Add(new LoanMoneyAction(this));
    }
}
