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
    public GovernmentObj government { get{ return saveSystem.saveData.Government; } }
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
        uiManager.selectUI.BindObj();
    }
    public HashSet<NpcObj> npcs { get { return mapSystem.npcs; } }

    /// <summary>
    /// 更新循环
    /// </summary>
    public void Update()
    {
        ////NPC注册工作后处理,比如是否需要工作
        NpcThink();
        ///建筑更新,修改订单的需求量,价格等
        BuildingThink();
        //建筑更新
        MarketWorkUpdate();
        //建筑进行生产的更新
        BuildingUpdate();
        ///市场商品的更新
        MarketGoodsUpdate();
        //地图更新资源流水线
        MapSystem.Instance.Update();
    }

    /// <summary>
    /// 建筑更新
    /// </summary>
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
