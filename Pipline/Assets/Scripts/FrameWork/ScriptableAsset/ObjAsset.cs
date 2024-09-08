using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
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
    public List<SceneObj> scenes;//一系列的Land
    [SerializeField]
	public Dictionary<SceneObj,List<Path>> paths;
    public WorldMap()
    {
        paths = new Dictionary<SceneObj, List<Path>>();
        scenes = new List<SceneObj>();
    }
    public void AddScene(SceneObj scene)
    {
        this.scenes.Add(scene);
        paths.Add(scene, new List<Path>());
        var p = new Path(scene, scene, 1);
        paths[scene].Add(p);//自己移动花费1
        scene.paths.Add(scene,new PathObj(p));
    }
    public void UpdateMap()
    {

    }
}

/// <summary>
/// 对象资源
/// </summary>
[System.Serializable,CreateAssetMenu(fileName = "NewObjAsset", menuName = "ScriptableObjects/ObjAsset")]
public class ObjAsset : SerializedScriptableObject
{
    [ShowInInspector]
    public SaveData saveData { get {
            return GameArchitect.get.saveData;
    } }
    public WorldMap map { get {
            return saveData.map;
    } }
    public List<SceneObj> scenes { get {
            return saveData.map.scenes;
    } }
    public ObjAsset()
    {
    }
    [ValueDropdown("Cell"),BoxGroup("SaveMap")]
    public SceneObj from;
    [ValueDropdown("Cell"), BoxGroup("SaveMap")]
    public SceneObj to;
    [BoxGroup("SaveMap")]
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
    [Button, BoxGroup("SaveMap")]
    public void Add()
    {
        if (map != null && map.paths != null)
        {
            var p = new Path(from, to, wastTime);
            map.paths[from].Add(p);
            from.paths.Add(to,new PathObj(p));
        }
    }
    [BoxGroup("商品列表"),ShowInInspector]
    public List<GoodsInf> goodsInfs;
    [BoxGroup("商品列表"), Button]
    public void GenEnum()
    {
        List<string> items = new List<string>();
        foreach (var x in goodsInfs)
        {
            items.Add(x.name);
        }
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("public enum GoodsEnum");
        sb.AppendLine("{");
        for (int i = 0; i < items.Count; i++)
        {
            // 去掉空格并确保枚举项以字母开头
            string enumItem = items[i].Trim().Replace(" ", "_");
            if (char.IsDigit(enumItem[0]))
            {
                enumItem = "_" + enumItem;
            }

            // 添加枚举项，使用逗号分隔并确保最后一项没有逗号
            sb.AppendLine($"    {enumItem} = {i + 1},");
        }

        // 删除最后一个逗号
        sb.Remove(sb.Length - 3, 1);

        sb.AppendLine("}");

        sb.AppendLine("public class GoodsGen{");
        sb.Append(@$"public static GoodsInf GetGoodsInf(GoodsEnum goodsEnum){{
        if (((int)goodsEnum) < GameArchitect.get.objAsset.goodsInfs.Count)
            return GameArchitect.get.objAsset.goodsInfs[((int)goodsEnum) - 1];
        else
            return null;
        }}");
        sb.AppendLine("     public static GoodsObj GetGoodsObj(GoodsEnum goodsEnum){");
        foreach (var x in goodsInfs)
        {
            var tex = x.GetType().Name;
            sb.Append($"if (goodsEnum == GoodsEnum.{x.name}) {{ var x = GetGoodsInf(goodsEnum); var y = new {tex.Replace("Inf", "Obj")}(); y.goodsInf = x; return y; }}\n");
            //sb.Append(@$"if (goodsEnum == GoodsEnum.{x.name}){{
            //    var x = GetGoodsInf(goodsEnum);
            //    var y = new {tex.Replace("Inf", "Obj")}();
            //    y.goodsInf = x;
            //    return y;
            //}}");
        }
        sb.AppendLine("return null;}");
        sb.AppendLine("}");
        File.WriteAllText($"Assets/Scripts/Enums/GoodsEnum.cs", sb.ToString());
        AssetDatabase.Refresh();
    }
    //////////////////////////////////
    [BoxGroup("商品流水线"), ShowInInspector]
    public List<Trans> trans;
    public Trans FindTrans(string name)
    {
        return trans.Find(e => { return e.title == name; });
    }
}