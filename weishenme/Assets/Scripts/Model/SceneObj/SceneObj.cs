using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 路径对象
/// </summary>
/// 
public class PathState:BaseState
{
    /// <summary>
    /// 建筑的各种信息
    /// </summary>
    public PathMeta pathMeta { get { return Meta.Instance.getMeta(pathEnum); } }
    /// <summary>
    /// 所属于的人
    /// </summary>
    public NpcObj belong;
    public Float money;
    public PathEnum pathEnum;
    public int rate;
    public PathState(PathObj pathObj, PathEnum pathEnum) : base(pathObj)
    {
        this.pathEnum = pathEnum;
        //buildingMeta=Meta.Instance.getMeta(buildingEnum);
        money = new Float(100);
        rate = 20;
    }
}


public class PathObj : BaseObj, IWorldPosition
{
    public PathEnum pathEnum;
    /// <summary>
    /// 当前所在的场景
    /// </summary>
    public SceneObj scene;
    /// <summary>
    /// 当前所在的位置
    /// </summary>
    public int x;
    public int y;
    /// <summary>
    /// 所属的人
    /// </summary>
    public NpcObj belong;
    public PathState now { get { return (PathState)getNow(); } }
    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }

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

    public Vector2Int GetWorldPos()
    {
        return new Vector2Int(x, y);
    }

    public SceneObj GetSceneObj()
    {
        return scene;
    }

    public PathObj(PathEnum pathEnum):base()
    {
        this.pathEnum = pathEnum;
        state=new PathState(this,pathEnum);
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
    public List<PathObj> paths;
    public MapLabels mapLabels;
    /// <summary>
    /// 所有的NPC
    /// </summary>
    public HashSet<NpcObj> npcs;

    public SceneState(BaseObj obj) : base(obj)
    {
        buildings = new List<BuildingObj>();
        npcs = new HashSet<NpcObj>();
        mapLabels = new MapLabels(10, 10);
    }
}
public class SceneEcInf : EconomicInf
{
    public SceneEcInf(BaseObj obj) : base(obj)
    {
    }
}
public class MapLabels
{
    public List<List<int>> distance;
    public List<List<int>> vis;
    public int xl;
    public int yl;
    public MapLabels(int xl,int yl)
    {
        Init(xl,yl);
    }
    [Button]
    public void Init(int x, int y)
    {
        distance = new List<List<int>>();
        vis = new List<List<int>>();
        this.xl = x;
        this.yl = y;
        for (int i = 0; i < xl; i++)
        {
            distance.Add(new List<int>());
            vis.Add(new List<int>());
            for (int j = 0; j < yl; j++)
            {
                distance[i].Add(0);//0代表不能
                vis[i].Add(0);
            }
        }
        visLabel = 1;
    }
    public void PathUpdate(PathObj pathObj)
    {
        distance[pathObj.x][pathObj.y] = pathObj.now.rate;
    }
    public int visLabel;
    public void EndSearch()
    {
        visLabel++;
    }
    public void SetVis(int x,int y)
    {
        vis[x][y] = visLabel;
    }
    public bool IsVis(int x,int y)
    {
        return vis[x][y] == visLabel;
    }

}
public class SceneObj : BaseObj, IRegisterEvent
{
    public SceneState now { get { return (SceneState)state; } }
    /// <summary>
    /// 建筑的列表
    /// </summary>
    public List<BuildingObj> buildings { get { return now.buildings; } }
    public List<PathObj> paths { get{ return now.paths; } }
    public HashSet<NpcObj> npcs { get { return now.npcs; } }
    public override void InitBaseState()
    {
        this.state = new SceneState(this);
    }

    public override void InitEconomicInf()
    {
        this.ecInf= new SceneEcInf(this);
    }
    /// <summary>
    /// 从A前往B
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public int GetTime(Vector2Int from,Vector2Int to)
    {
        var cost = AStarSearcher.FindPath(from, to, now.mapLabels.IsVis, now.mapLabels.SetVis, now.mapLabels.distance);
        now.mapLabels.EndSearch();
        return cost;
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
    public void AddPath(PathObj pathObj)
    {
        paths.Add(pathObj);
    }
    public void RemoveBuilding(PathObj pathObj)
    {
        paths.Remove(pathObj);
    }
    public void AddNpc()
    {
        var npc=new NpcObj();
        npc.Init(this);
        npcs.Add(npc);
    }

    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }

    public SceneObj():base()
    {

    }
}
