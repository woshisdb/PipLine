using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObj : BaseObj
{
    public string sceneName;
    public List<NpcObj> npcs;
    public List<BuildingObj> sceneObjects;
    public SceneObj()
    {
        sceneName = "";
        npcs = new List<NpcObj>();
    }
}
