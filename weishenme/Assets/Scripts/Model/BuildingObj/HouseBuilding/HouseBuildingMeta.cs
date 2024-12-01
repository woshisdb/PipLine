using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuildingMeta : BuildingMeta
{
    public override BuildingObj createBuildingObj()
    {
        return new HouseBuildingObj(ReturnEnum());
    }

    public override GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] {};
    }

    public override BuildingEnum ReturnEnum()
    {
        return BuildingEnum.houseBuilding;
    }
    public HouseBuildingMeta():base()
    {
        view = (GameObject)Resources.Load("Prefab/building");
    }
}
