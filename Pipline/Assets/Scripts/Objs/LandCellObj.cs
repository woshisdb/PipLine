using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObj : BaseObj,ISendEvent
{
    public string sceneName;
    public List<NpcObj> npcs;
    public List<BuildingObj> buildings;
    public SceneObj()
    {
        sceneName = "";
        npcs = new List<NpcObj>();
        buildings = new List<BuildingObj>();
    }
    public void UpdateEvent()
    {
        this.SendEvent(new UpdateSceneEvent());
    }
}
