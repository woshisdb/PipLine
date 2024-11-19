using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem:Singleton<MapSystem>
{
    /// <summary>
    /// 一系列的场景
    /// </summary>
    public List<List<SceneObj>> scenes { get { return SaveSystem.Instance.saveData.SceneObjects; } }
    public HashSet<NpcObj> npcs { get { return SaveSystem.Instance.saveData.npcs; } }
    /// <summary>
    /// 在给定花费的最小时间
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public int WasterTime(SceneObj a,SceneObj b,int money)
    {
        float rate = 1.8f;
        var dx=Mathf.Abs(a.now.x-b.now.x);
        var dy=Mathf.Abs(a.now.y-b.now.y);
        return (int)( (dx+dy)*rate );
    }

    public int WasterMoney(SceneObj a, SceneObj b,int time)
    {
        return 1;
    }
    private MapSystem()
    {
    }
}
