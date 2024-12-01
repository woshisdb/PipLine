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
        ///���ش浵��Ϣ
        saveSystem.Load();
        ///��ͼ��Ϣ
        mapSystem=MapSystem.Instance;
        ///�г�ϵͳ
        market=Market.Instance;
        ///ʱ��ϵͳ
        timeSystem=TimeSystem.Instance;
        ///����Ķ���
        worldViewSystem = GameObject.Find("World").GetComponent<WorldView>();
        worldViewSystem.Bind(mapSystem);
        uiManager.selectUI.BindObj();
    }
    public HashSet<NpcObj> npcs { get { return mapSystem.npcs; } }

    /// <summary>
    /// ����ѭ��
    /// </summary>
    public void Update()
    {
        ////NPCע�Ṥ������,�����Ƿ���Ҫ����
        NpcThink();
        ///��������,�޸Ķ�����������,�۸��
        BuildingThink();
        //��������
        MarketWorkUpdate();
        //�������������ĸ���
        BuildingUpdate();
        ///�г���Ʒ�ĸ���
        MarketGoodsUpdate();
        //��ͼ������Դ��ˮ��
        MapSystem.Instance.Update();
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void BuildingUpdate()
    {
        foreach (var scenex in mapSystem.scenes)//���³���
        {
            foreach (var scene in scenex)
                foreach (var building in scene.buildings)
                {
                    building.Update();
                }
        }
    }

    /// <summary>
    /// ˼��
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
    /// ������ǰ���и���
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
    /// �Զ����ĸ���
    /// </summary>
    public void MarketWorkUpdate()
    {
        Market.Instance.workMatcher.Match();
    }
    /// <summary>
    /// ����Ʒ�����ĸ���
    /// </summary>
    public void MarketGoodsUpdate()
    {
        Market.Instance.goodsMatcher.Match();
    }
}
