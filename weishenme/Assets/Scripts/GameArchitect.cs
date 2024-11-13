using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    public MapSystem mapSystem;
    public Market market;
    public UIManager uiManager;
    public GameArchitect()
    {

    }
    public override void OnSingletonInit()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public List<NpcObj> npcs;

    /// <summary>
    /// ����ѭ��
    /// </summary>
    public void Update()
    {
        ///�Գ�������һϵ�еĳ�ʼ��,������Щ��·����ǰ��
        InitScene();
        //NPC����
        NpcThink();
        ///��������,�Լ������ļ۸�
        BuildingThink();
        //����ÿ����·,�������
        PathUpdate();
        ///���г����и���
        MarketWorkUpdate();
        //�������������ĸ���
        BuildingUpdate();
        //�˸��¹������Ʒ  
        NpcAfterUpdate();
        ///�г���Ʒ�ĸ���
        MarketGoodsUpdate();
    }

    private void NpcAfterUpdate()
    {

    }

    private void BuildingUpdate()
    {
        foreach (SceneObj scene in mapSystem.scenes)//���³���
        {
            foreach(var building in scene.buildings)
            {
                building.Update();
            }
        }
    }

    private void PathUpdate()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// ˼��
    /// </summary>
    private void NpcThink()
    {
        foreach(var scene in mapSystem.scenes)
        {
            foreach(var npc in scene.npcs)
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
        foreach (SceneObj scene in mapSystem.scenes)//���³���
        {
            if (true)//��������,�������·��,�����·��
            {

            }
        }
    }


    /// <summary>
    /// ������ǰ���и���
    /// </summary>
    public void BuildingThink()
    {
        foreach (var scene in mapSystem.scenes)
        {
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
