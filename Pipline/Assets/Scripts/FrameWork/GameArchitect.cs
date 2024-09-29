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

public static class Meta
{
    public static int unInitVal = -99999999;
    public static int historySum = 10;
}

/// <summary>
/// 金钱
/// </summary>
public class Money
{
    public int money=0;
    public Money()
    {
    }
    public void Update()
    {
    }
    public void Add(int money)
    {
        this.money += money;
    }
}
public static class ListExtensions
{
    // 泛型方法，传入列表和 lambda 表达式，返回最小元素
    public static T FindMinElement<T, TResult>(this List<T> list, Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        if (list == null || list.Count == 0)
            return default(T);
        T minElement = list[0];
        TResult minValue = selector(minElement);

        foreach (T element in list)
        {
            TResult currentValue = selector(element);
            if (currentValue.CompareTo(minValue) < 0)
            {
                minValue = currentValue;
                minElement = element;
            }
        }

        return minElement;
    }
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
    static uint no = 0;
    public static uint No { get { no++;return no; } }
    public Client client { get { return gameLogic.client; } }
    public TimeSystem timeSystem { get { return saveData.timeSystem; } }
    public List<BuildingObj> buildings { get { return GameArchitect.get.saveData.buildings; } }
    public List<SceneObj> scenes { get { return GameArchitect.get.saveData.map.scenes; } }
    public List<NpcObj> npcs { get { return GameArchitect.get.saveData.npcs; } }
    public Dictionary<SceneObj, List<Path>> paths { get { return GameArchitect.get.saveData.map.paths; } }
    public WorldMap worldMap { get { return GameArchitect.get.saveData.map; } }
    public ShuiWuJu shuiWuJu { get { return GameArchitect.get.saveData.shuiWuJu; } }
    public NPCManager npcManager;
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
    public EconomicSystem economicSystem;
    protected override void Init()
    {
        npcManager = new NPCManager();
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
        economicSystem = new EconomicSystem();
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
        ///////////////////////这么多人采矿///////////////////////////////////
        int allpersons = 10;
        for(int i=0;i< allpersons; i++)
        {
            var npc = new NpcObj();
            map.Enter(npc);
        }
        /////////////////////////这么多人炼铁//////////////////////////////////
        //var ironMining = new IronMiningObj();
        //map.AddBuilding(ironMining);//添加铁矿
        //var npc1 = new NpcObj();
        //ironMining.jobManager.RegisterJob<CaiKuangJob>(npc1);
        //map.Enter(npc1);
        //var gaoLu = new Gaolu();
        //map.AddBuilding(gaoLu);
        //for(int i=0;i< allpersons/5; i++)
        //{
        //    gaoLu.jobManager.RegisterJob<LianZhiJob>(npcs[i]);
        //}
        //for (int i = allpersons / 5*1; i < allpersons / 5*2; i++)
        //{
        //    gaoLu.jobManager.RegisterJob<CarryJob>(npcs[i]);
        //}
        ////////////////////////这么多人煤矿//////////////////////////////////////
        //var meikuang = new Meikuang();
        //map.AddBuilding(meikuang);
        //for (int i = allpersons / 5*2; i < allpersons / 5*3; i++)
        //{
        //    meikuang.jobManager.RegisterJob<CaiMeiJob>(npcs[i]);
        //}
        //////////////////////这么多人农场//////////////////////////////////////
        var nongchang = new NongChangObj();
        map.AddBuilding(nongchang);
        for (int i = allpersons / 5*3; i < allpersons / 5*4; i++)
        {
            nongchang.jobManager.RegisterJob<ZuoFanJob>(npcs[i]);
        }
        //var nongchang1 = new NongChangObj();
        //map.AddBuilding(nongchang1);
        //for (int i = allpersons / 5*4; i < allpersons / 5*5; i++)
        //{
        //    nongchang1.jobManager.RegisterJob<ZuoFanJob>(npcs[i]);
        //}
        //////////////////////初始化工作/////////////////////////////////////////
        npcManager.jobContainer.Update();
        Debug.Log("初始化结束");
    }
    /// <summary>
    /// 地图的更新
    /// </summary>
    public void MapInit()
    {
        pathFinder = new PathFinder(worldMap.paths);
        foreach (var x in buildings)
        {
            x.FindResourceWay();
            GameArchitect.get.economicSystem.RegBuildingOut(x);
            GameArchitect.get.economicSystem.RegBuildingIn(x);//请求输入
        }
        //Debug.Log(scenes.Count);
        for(int i=0;i<scenes.Count;i++)//场景初始化
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