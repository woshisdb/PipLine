using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using QFramework;
using UnityEngine;
public interface ICanShowView
{

}
public class BaseObjectPool<T,F> : SimpleObjectPool<T>
where F : BaseObj
where T : IPutInPool<F>
{
    readonly Action<T> useMethod;
    public BaseObjectPool(Func<T> factoryMethod, Action<T> useMethod, Action<T> resetMethod = null, int initCount = 0) : base(factoryMethod, resetMethod, initCount)
    {
        this.useMethod = useMethod;
    }
    public override T Allocate()
    {
        var ret= base.Allocate();
        useMethod(ret);
        return ret;
    }
    public T Allocate(F obj)
    {
        var ret = base.Allocate();
        useMethod(ret);
        ret.Init(obj);
        return ret;
    }
}

public class GameArchitect : Architecture<GameArchitect>,ISendEvent
{
    public static GameArchitect get
    {
        get { return (GameArchitect)GameArchitect.Interface; }

    }
    public StringBuilder sb;
    public GameLogic gameLogic;
    public Transform buildingPoolT;
    public SaveData saveData;
    public ObjAsset objAsset;
    public TimeSystem timeSystem { get { return saveData.timeSystem; } }
    public List<BuildingObj> buildings { get { return GameArchitect.get.saveData.buildings; } }
    public List<SceneObj> scenes { get { return GameArchitect.get.saveData.map.scenes; } }
    public List<NpcObj> npcs { get { return GameArchitect.get.saveData.npcs; } }
    /// <summary>
    /// 场景对象池
    /// </summary>
    public BaseObjectPool<SceneControler, SceneObj> scenePool;
    protected GameObject sceneTemplate;
    /// <summary>
    /// 建筑对象池
    /// </summary>
    public BaseObjectPool<BuildingControl, BuildingObj> buildingPool;
    protected GameObject cardTemplate;
    public List<SceneControler> sceneControlers;
    protected override void Init()
    {
        buildingPoolT = GameObject.Find("BuildingRoot").transform;
        sb = new StringBuilder();
        Debug.Log(1);
        sceneControlers = new List<SceneControler>();
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        sceneTemplate= Resources.Load<GameObject>("Controler/Scene");
        cardTemplate= Resources.Load<GameObject>("Controler/Card");
        ///初始化场景池
        scenePool = new BaseObjectPool<SceneControler, SceneObj>(
            () =>
            {
                var t = GameObject.Instantiate<GameObject>(sceneTemplate);
                t.transform.SetParent(gameLogic.scenes.transform);
                return t.GetComponent<SceneControler>();
            },
            (e) => {
                e.Allocate();
            },
            (e) =>
            {
                e.Recycle();
            }
        );
        buildingPool = new BaseObjectPool<BuildingControl, BuildingObj>(
            () =>
            {
                var t = GameObject.Instantiate<GameObject>(cardTemplate);
                return t.GetComponent<BuildingControl>();
            },
            (e) => {
                e.Allocate();
            },
            (e) =>
            {
                e.Recycle();
            }
        );
        this.objAsset = Resources.Load<ObjAsset>("NewObjAsset");
        SaveSystem.Instance.Load();//加载数据
        if (SaveSystem.Instance.firstInit)
        {
            FirstInit();
        }
        MapInit();
    }
    /// <summary>
    /// 首次初始化
    /// </summary>
    public void FirstInit()
    {
        var map = new SceneObj();
        map.sceneName = "测试场景";
        saveData.map.scenes.Add(map);
        //添加铁矿厂
        var ironMining = new IronMiningObj();
        for(int i = 0; i < 1; i++)
        map.AddBuilding(ironMining);
        for(int i = 0; i < 100; i++)
        {
            var npc = new NpcObj();
            map.Enter(npc);
            ironMining.jobManager.RegisterJob<CaiKuangJob>(npc);
        }
        //添加高炉
        var gaoLu = new Gaolu();
        map.AddBuilding(gaoLu);
        for(int i=0;i<100;i++)
        {
            var npc = new NpcObj();
            map.Enter(npc);
            gaoLu.jobManager.RegisterJob<LianZhiJob>(npc);
        }
    }
    /// <summary>
    /// 地图的更新
    /// </summary>
    public void MapInit()
    {
        Debug.Log(scenes.Count);
        for(int i=0;i<scenes.Count;i++)
        {
            var map = scenes[i];
            var sc= scenePool.Allocate(map);//初始化场景
            sc.gameObject.transform.position = new Vector3(i * 50, 0, 0);
            sceneControlers.Add(sc);
            map.UpdateEvent();
        }
    }
    public void AddNpc(NpcObj npc,SceneObj scene=null)
    {
        npcs.Add(npc);
        if(scene==null)
        {
            scene = scenes[0];
        }
        scene.Enter(npc);
    }
    public void MoveNpc(NpcObj npc,SceneObj to)
    {
        npc.belong.Leave(npc);
        to.Enter(npc);
    }
    public void RemoveNpc(NpcObj npc)
    {
        npc.belong.Leave(npc);
        npcs.Remove(npc);
    }
}