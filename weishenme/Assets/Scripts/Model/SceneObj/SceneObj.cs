using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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

public class SceneState:BaseState
{
    public int x;
    public int y;
    /// <summary>
    /// 一系列的建筑
    /// </summary>
    public List<BuildingObj> buildings;
    /// <summary>
    /// 所有的NPC
    /// </summary>
    public HashSet<NpcObj> npcs;

    public SceneState(BaseObj obj) : base(obj)
    {
        buildings = new List<BuildingObj>();
        npcs = new HashSet<NpcObj>();
    }
}
public class SceneEcInf : EconomicInf
{
    public SceneEcInf(BaseObj obj) : base(obj)
    {
    }
}
public class SceneObj : BaseObj, IRegisterEvent
{
    public SceneState now { get { return (SceneState)state; } }
    /// <summary>
    /// 建筑的列表
    /// </summary>
    public List<BuildingObj> buildings { get { return now.buildings; } }
    public HashSet<NpcObj> npcs { get { return now.npcs; } }
    public override void InitBaseState()
    {
        this.state = new SceneState(this);
    }

    public override void InitEconomicInf()
    {
        this.ecInf= new SceneEcInf(this);
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
        buildings.Add(buildingObj);
        buildingObj.Init();
    }
    public void RemoveBuilding(BuildingObj buildingObj)
    {
        buildings.Remove(buildingObj);
    }
    public void AddNpc()
    {
        var npc=new NpcObj();
        npc.Init(this);
        npcs.Add(npc);
    }
    public SceneObj():base()
    {

    }
}
