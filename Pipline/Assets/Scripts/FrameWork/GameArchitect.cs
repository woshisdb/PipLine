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
    public Dictionary<SceneObj, List<Path>> paths { get { return GameArchitect.get.saveData.map.paths; } }
    public WorldMap worldMap { get { return GameArchitect.get.worldMap; } }
    public PathFinder pathFinder;
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
    public BaseObjectPool<PathControler, PathObj> pathPool;
    protected GameObject pathTemplate;

    public List<SceneControler> sceneControlers;
    protected override void Init()
    {
        TransEnum.Init();
        buildingPoolT = GameObject.Find("BuildingRoot").transform;
        sb = new StringBuilder();
        Debug.Log(1);
        sceneControlers = new List<SceneControler>();
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        sceneTemplate= Resources.Load<GameObject>("Controler/Scene");
        cardTemplate= Resources.Load<GameObject>("Controler/Card");
        pathTemplate= Resources.Load<GameObject>("Controler/Path");
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
        pathPool = new BaseObjectPool<PathControler, PathObj>(
            () =>
            {
                var t = GameObject.Instantiate<GameObject>(pathTemplate);
                return t.GetComponent<PathControler>();
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
        timeSystem.time = 0;
        var map = new SceneObj();
        map.sceneName = "测试场景";
        saveData.map.AddScene(map);
        //添加铁矿厂
        var ironMining = new IronMiningObj();
        map.AddBuilding(ironMining);//添加铁矿
        var gaoLu = new Gaolu();
        ///////////////////////////////////
        var t = GameArchitect.get.objAsset.FindTrans("开采铁矿石");
        var v = new CarryTrans();
        v.title = "搬运商品";
        v.maxTrans = 2;
        v.wasterTimes = 1;
        v.from.source.Add(new Pair<GoodsEnum, int>(GoodsEnum.铁矿石, 1));
        v.to.source.Add(new Pair<GoodsEnum, int>(GoodsEnum.铁矿石, 1));
        ironMining.pipLineManager.SetTrans(
        new List<TransNode>()
        {
            new TransNode(t,ironMining.resource,ironMining.goodsRes),
            new TransNode(v,ironMining.goodsRes,gaoLu.resource)
        });
        map.AddBuilding(gaoLu);//添加熔炉
        ///////////////////////这么多人采矿///////////////////////////////////
        var npc1 = new NpcObj();
        npc1.sum = 1000000;
        map.Enter(npc1);
        ironMining.jobManager.RegisterJob<CaiKuangJob>(npc1);
        ///////////////////////这么多人炼铁//////////////////////////////////
        var npc2 = new NpcObj();
        npc2.sum = 1000000;
        map.Enter(npc2);
        gaoLu.jobManager.RegisterJob<LianZhiJob>(npc2);
        //////////////////////这么多人搬运//////////////////////////////////////
        var npc3 = new NpcObj();
        npc3.sum = 1000000;
        map.Enter(npc3);
        ironMining.jobManager.RegisterJob<CarryJob>(npc3);
        foreach (var x in npcs)
        {
            x.lifeStyle.job.SetDayJob();
        }
        Debug.Log("初始化结束");
    }
    /// <summary>
    /// 地图的更新
    /// </summary>
    public void MapInit()
    {
        Debug.Log(scenes.Count);
        for(int i=0;i<scenes.Count;i++)//场景初始化
        {
            var map = scenes[i];
            var sc= scenePool.Allocate(map);//初始化场景
            sc.gameObject.transform.position = new Vector3(i * 50, 0, 0);
            sceneControlers.Add(sc);
            map.UpdateEvent();
        }
        pathFinder = new PathFinder(worldMap.paths);
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