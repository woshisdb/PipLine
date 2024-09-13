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

/// <summary>
/// ��Ǯ
/// </summary>
public class Money
{
    public int money=0;
}
public static class ListExtensions
{
    // ���ͷ����������б�� lambda ���ʽ��������СԪ��
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
    public TimeSystem timeSystem { get { return saveData.timeSystem; } }
    public List<BuildingObj> buildings { get { return GameArchitect.get.saveData.buildings; } }
    public List<SceneObj> scenes { get { return GameArchitect.get.saveData.map.scenes; } }
    public List<NpcObj> npcs { get { return GameArchitect.get.saveData.npcs; } }
    public Dictionary<SceneObj, List<Path>> paths { get { return GameArchitect.get.saveData.map.paths; } }
    public WorldMap worldMap { get { return GameArchitect.get.saveData.map; } }
    public PathFinder pathFinder;
    /// <summary>
    /// ���������
    /// </summary>
    public BaseObjectPool<SceneControler, SceneObj> scenePool;
    protected GameObject sceneTemplate;
    /// <summary>
    /// ���������
    /// </summary>
    public BaseObjectPool<BuildingControl, BuildingObj> buildingPool;
    protected GameObject cardTemplate;
    public BaseObjectPool<PathControler, PathObj> pathPool;
    protected GameObject pathTemplate;

    public List<SceneControler> sceneControlers;
    public EconomicSystem economicSystem;
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
        ///��ʼ��������
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
        SaveSystem.Instance.Load();//��������
        if (SaveSystem.Instance.firstInit)
        {
            FirstInit();
        }
        economicSystem = new EconomicSystem();
        MapInit();
    }
    /// <summary>
    /// �״γ�ʼ��
    /// </summary>
    public void FirstInit()
    {
        timeSystem.time = 0;
        var map = new SceneObj();
        map.sceneName = "���Գ���";
        saveData.map.AddScene(map);
        ///////////////////////��ô���˲ɿ�///////////////////////////////////
        var ironMining = new IronMiningObj();
        map.AddBuilding(ironMining);//�������
        var npc1 = new NpcObj();
        npc1.sum = 1;
        map.Enter(npc1);
        ironMining.jobManager.RegisterJob<CaiKuangJob>(npc1);
        //var npc3 = new NpcObj();
        //npc3.sum = 1;
        //map.Enter(npc3);
        //ironMining.jobManager.RegisterJob<CarryJob>(npc3);
        ///////////////////////��ô��������//////////////////////////////////
        var gaoLu = new Gaolu();
        map.AddBuilding(gaoLu);//�����¯
        var npc2 = new NpcObj();
        npc2.sum = 1;
        map.Enter(npc2);
        gaoLu.jobManager.RegisterJob<LianZhiJob>(npc2);
        var npc4 = new NpcObj();
        npc4.sum = 2;
        map.Enter(npc4);
        gaoLu.jobManager.RegisterJob<CarryJob>(npc4);
        //////////////////////��ô����ú��//////////////////////////////////////
        var meikuang = new Meikuang();
        map.AddBuilding(meikuang);//�����¯
        var npc5 = new NpcObj();
        npc5.sum = 1;
        map.Enter(npc5);
        meikuang.jobManager.RegisterJob<CaiMeiJob>(npc5);
        //var npc6 = new NpcObj();
        //npc6.sum = 1;
        //map.Enter(npc6);
        //meikuang.jobManager.RegisterJob<CarryJob>(npc6);
        //////////////////////��ô����ũ��//////////////////////////////////////
        var nongchang = new NongChangObj();
        map.AddBuilding(nongchang);//�����¯
        var npc7 = new NpcObj();
        npc7.sum = 1;
        map.Enter(npc7);
        nongchang.jobManager.RegisterJob<ZuoFanJob>(npc7);
        //var npc8 = new NpcObj();
        //npc8.sum = 1;
        //map.Enter(npc8);
        //nongchang.jobManager.RegisterJob<CarryJob>(npc8);
        //////////////////////��ʼ������/////////////////////////////////////////
        foreach (var x in npcs)
        {
            x.lifeStyle.job.SetDayJob();
        }
        Debug.Log("��ʼ������");
    }
    /// <summary>
    /// ��ͼ�ĸ���
    /// </summary>
    public void MapInit()
    {
        foreach(var x in buildings)
        {
            x.FindResourceWay();
        }
        Debug.Log(scenes.Count);
        for(int i=0;i<scenes.Count;i++)//������ʼ��
        {
            var map = scenes[i];
            var sc= scenePool.Allocate(map);//��ʼ������
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