using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class HouseBuildingState : BuildingState
{
    public List<NpcObj> npcs;
    public HouseBuildingState(BuildingObj buildingObj, BuildingEnum buildingEnum) : base(buildingObj,buildingEnum)
    {
        npcs = new List<NpcObj>();
    }

}

public class HouseBuildingEc : EconomicInf
{
    public HouseBuildingEc(BaseObj obj) : base(obj)
    {
    }
}

public class HouseBuildingObj : BuildingObj
{
    public new HouseBuildingState now { get { return (HouseBuildingState)state; } }
    public HouseBuildingObj(BuildingEnum buildingEnum):base()
    {
        this.state = new HouseBuildingState(this, buildingEnum);
        this.ecInf = new HouseBuildingEc(this);
    }
    /// <summary>
    /// 将场景的noc放到里面
    /// </summary>
    [Button]
    public void SetAllNpcBelong()
    {
        var npcs=scene.npcs;
        foreach(var npc in npcs)
        {
            npc.now.physicalState.livePlace = this;
            now.npcs.Add(npc);
        }
    }
    public override List<UIItemBinder> GetUI()
    {
        var ret = base.GetUI();
        return ret;
    }
}
