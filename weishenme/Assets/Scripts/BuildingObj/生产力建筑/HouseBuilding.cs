using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一个商品房子,用于NPC的家,可以睡觉,放置家具,存储物品
/// </summary>
public class HouseBuildingState : BuildingState
{
    /// <summary>
    /// 存储的一系列商品
    /// </summary>
    public Dictionary<GoodsEnum, int> storeGoods;
    public HouseBuildingState() : base()
    {

    }
    public override void Init()
    {

    }
}
/// <summary>
///房子经济信息
/// </summary>
public class HouseBuildingEc : BuildingEc
{

}
/// <summary>
/// 房子的建筑
/// </summary>
public class HouseBuildingObj : BuildingObj
{
}