using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObj : BaseObj,ISendEvent
{
    public string sceneName;
    public List<NpcObj> npcs;
    public List<BuildingObj> buildings;
    public List<PathObj> paths;
    public SceneObj()
    {
        sceneName = "";
        npcs = new List<NpcObj>();
        buildings = new List<BuildingObj>();
        paths = new List<PathObj>();
    }
    public void UpdateEvent()
    {
        this.SendEvent(new UpdateSceneEvent());
    }
    public void Enter(NpcObj npc)
    {
        this.npcs.Add(npc);
        npc.belong = this;
    }
    public void Leave(NpcObj npc)
    {
        this.npcs.Remove(npc);
        npc.belong = null;
    }
    public void AddBuilding(BuildingObj building)
    {
        GameArchitect.get.buildings.Add(building);
        buildings.Add(building);
        UpdateEvent();
    }
    public void RemoveBuilding(BuildingObj building)
    {
        GameArchitect.get.buildings.Remove(building);
        buildings.Remove(building);
        UpdateEvent();
    }
}
