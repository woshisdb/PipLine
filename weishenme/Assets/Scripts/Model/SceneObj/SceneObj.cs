using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 路径对象
/// </summary>
public class PathObj : BaseObj
{
    public int wasterTime=10;
    public SceneObj from;
    public SceneObj to;
    /// <summary>
    /// 所属的人
    /// </summary>
    public NpcObj belong;
    public BuildingObj fromB;
    public BuildingObj toB;
    public override void InitBaseState()
    {
    }

    public override void InitEconomicInf()
    {
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
    }

}


public class SceneObj : BaseObj,IRegisterEvent
{
    /// <summary>
    /// 一系列的前往目标地的路径
    /// </summary>
    public Dictionary<SceneObj, PathObj> paths;
    /// <summary>
    /// 建筑的列表
    /// </summary>
    public List<BuildingObj> buildings;
    public HashSet<NpcObj> npcs;
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
    public void AddBuilding(BuildingObj buildingObj)
    {

    }
    public void RemoveBuilding(BuildingObj buildingObj)
    {

    }
}
