using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Path
{
    public SceneObj from;
    public SceneObj to;
    public int wastTime;
    public Path(SceneObj from,SceneObj to,int wastTime)
    {
        this.from = from;
        this.to = to;
        this.wastTime = wastTime;
    }
}

public class WorldMap
{
	[SerializeField]
	public List<Path> paths;
    public WorldMap()
    {
        paths = new List<Path>();
    }
}

/// <summary>
/// 对象资源
/// </summary>
[System.Serializable,CreateAssetMenu(fileName = "NewObjAsset", menuName = "ScriptableObjects/ObjAsset")]
public class ObjAsset : SerializedScriptableObject
{
    [SerializeField]
    public WorldMap map;
    public List<SceneObj> scenes;//一系列的Land
    public ObjAsset()
    {
        map = new WorldMap();
        scenes = new List<SceneObj>();
    }
    [ValueDropdown("Cell")]
    public SceneObj from;
    [ValueDropdown("Cell")]
    public SceneObj to;
    public int wastTime;
    private ValueDropdownList<SceneObj> Cell { 
        get {
            var ret=new ValueDropdownList<SceneObj>();
            foreach (var cell in scenes)
            {
                ret.Add(cell.sceneName,cell);
            }
            return ret;
        }
    }
    [Button]
    public void Add()
    {
        map.paths.Add(new Path(from,to,wastTime));
    }
}