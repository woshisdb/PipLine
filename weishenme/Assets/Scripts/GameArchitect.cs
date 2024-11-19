using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    public MapSystem mapSystem;
    public Market market;
    public UIManager uiManager;
    public SaveSystem saveSystem;
    public TimeSystem timeSystem;
    public WorldView worldViewSystem;
    private GameArchitect()
    {

    }
    public override void OnSingletonInit()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        saveSystem=SaveSystem.Instance;
        ///加载存档信息
        saveSystem.Load();
        ///地图信息
        mapSystem=MapSystem.Instance;
        ///市场系统
        market=Market.Instance;
        ///时间系统
        timeSystem=TimeSystem.Instance;
        ///世界的对象
        worldViewSystem = GameObject.Find("World").GetComponent<WorldView>();
        worldViewSystem.Bind(mapSystem);
    }
    public HashSet<NpcObj> npcs { get { return mapSystem.npcs; } }

    /// <summary>
    /// 更新循环
    /// </summary>
    public void Update()
    {
        ///对场景进行一系列的初始化,更新那些道路可以前进
        //InitScene();
        ////NPC请求
        //NpcThink();
        ///建筑更新,自己订单的价格
        BuildingThink();
        ////更新每个道路,货物搬运
        //PathUpdate();
        ///对市场进行更新
        //MarketWorkUpdate();
        //建筑进行生产的更新
        BuildingUpdate();
        //人更新购买的物品  
        //NpcAfterUpdate();
        ///市场商品的更新
        //MarketGoodsUpdate();
    }

    private void NpcAfterUpdate()
    {

    }

    private void BuildingUpdate()
    {
        foreach (var scenex in mapSystem.scenes)//更新场景
        {
            foreach (var scene in scenex)
                foreach (var building in scene.buildings)
                {
                    building.Update();
                }
        }
    }

    private void PathUpdate()
    {
        //throw new NotImplementedException();
    }
    /// <summary>
    /// 思考
    /// </summary>
    private void NpcThink()
    {
        foreach(var scenex in mapSystem.scenes)
        {
            foreach (var scene in scenex)
                foreach (var npc in scene.npcs)
                {
                    npc.BefThink();
                }
        }
    }

    /// <summary>
    /// 更新场景的每一条道路,来判断是否可以走通
    /// </summary>
    public void InitScene()
    {
        foreach (var scenex in mapSystem.scenes)//更新场景
        {
            foreach (var scene in scenex)
            {

            }
        }
    }


    /// <summary>
    /// 在生产前进行更新
    /// </summary>
    public void BuildingThink()
    {
        foreach (var scenex in mapSystem.scenes)
        {
            foreach(var scene in scenex)
            foreach (var building in scene.buildings)
            {
                building.BefThink();
            }
        }
    }
    /// <summary>
    /// 对订单的更新
    /// </summary>
    public void MarketWorkUpdate()
    {
        Market.Instance.workMatcher.Match();
    }
    /// <summary>
    /// 对商品订单的更新
    /// </summary>
    public void MarketGoodsUpdate()
    {
        Market.Instance.goodsMatcher.Match();
    }
}
