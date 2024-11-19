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
    }
    public HashSet<NpcObj> npcs { get { return mapSystem.npcs; } }

    /// <summary>
    /// ����ѭ��
    /// </summary>
    public void Update()
    {
        ///�Գ�������һϵ�еĳ�ʼ��,������Щ��·����ǰ��
        //InitScene();
        ////NPC����
        //NpcThink();
        ///��������,�Լ������ļ۸�
        BuildingThink();
        ////����ÿ����·,�������
        //PathUpdate();
        ///���г����и���
        //MarketWorkUpdate();
        //�������������ĸ���
        BuildingUpdate();
        //�˸��¹������Ʒ  
        //NpcAfterUpdate();
        ///�г���Ʒ�ĸ���
        //MarketGoodsUpdate();
    }

    private void NpcAfterUpdate()
    {

    }

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

    private void PathUpdate()
    {
        //throw new NotImplementedException();
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
    /// ���³�����ÿһ����·,���ж��Ƿ������ͨ
    /// </summary>
    public void InitScene()
    {
        foreach (var scenex in mapSystem.scenes)//���³���
        {
            foreach (var scene in scenex)
            {

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
