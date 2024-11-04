using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    public MapSystem mapSystem;
    /// <summary>
    /// �ܹ���ȡ�����������Ķ�����
    /// </summary>
    public HashSet<ICanReceiveProdOrder> receiveProdOrder;
    /// <summary>
    /// �ܹ���ȡ��ͨ��Ʒ�����Ķ�����
    /// </summary>
    public HashSet<ICanReceiveNormalGoodsOrder> receiveNormalGoodsOrder;
    /// <summary>
    /// һϵ�еĳ���
    /// </summary>
    public List<SceneObj> scenes;
    public GameArchitect()
    {

    }
    public List<NpcObj> npcs;

    /// <summary>
    /// ����ѭ��
    /// </summary>
    public void Update()
    {
        ///�Գ�������һϵ�еĳ�ʼ��,������Щ��·����ǰ��
        InitScene();
        ///����ÿ��NPC�ļƻ�,��������Է��Ͷ�������,������Ƹ��Ա����
        //UpdateNpcsPlan();
        //��NPC�����������͸���˾
        NpcBefGen();
        //����ÿ����·,�������
        PathUpdate();
        //�������������ĸ���
        BuildingUpdate();
        //�˸��¹������Ʒ 
        NpcBuyUpdate();
    }
    /// <summary>
    /// ���³�����ÿһ����·,���ж��Ƿ������ͨ
    /// </summary>
    public void InitScene()
    {
        foreach (SceneObj scene in scenes)//���³���
        {
            if (true)//��������,�������·��,�����·��
            {

            }
        }
    }
    /// <summary>
    /// ���¹滮
    /// </summary>
    public void UpdateNpcsPlan()
    {
        foreach(var npc in npcs)
        {
            npc.ReThinkContract(npc.now);
        }
        ///��npc���и���
        foreach (var npc in npcs)
        {
            npc.Plan(npc.now);
        }
    }
    /// <summary>
    /// ����������
    /// </summary>
    public void NpcBefGen()
    {
        for (int i = 0; i < npcs.Count; i++)//�ʱ�����,���ǿ��ʱ����������
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.CapitalGains)
                npc.BefCapitalGains();
        }

        for (int i = 0; i < npcs.Count; i++)//��Ӷ������
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.SelfEmployment)
                npc.BefSelfEmployment();
        }

        for (int i = 0; i < npcs.Count; i++)//�Լ�������ͬʱ��Ӷ����
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.SelfAndOtherEmployment)
                npc.BefSelfAndOtherEmployment();
        }

        for (int i = 0; i < npcs.Count; i++)//�̶�����
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.FixedSalary)
                npc.BefFixedSalaryUpdate();
        }
    }
    /// <summary>
    /// ��·��˾��ϸ���ǵĶ���
    /// </summary>
    public void PathUpdate()
    {
        foreach(var scene in scenes)
        {
            scene.UpdatePathOrder();
        }
    }
    /// <summary>
    /// ��ÿ���������и���
    /// </summary>
    public void BuildingUpdate()
    {
        foreach(var s in scenes)
        {
            foreach(var b in s.buildingObjs)
            {
                b.Update(b.getNow());
            }
        }    
    }
    /// <summary>
    /// �����˹������Ʒ
    /// </summary>
    public void NpcBuyUpdate()
    {
        foreach( var npcS in npcs)
        {
            var npc = npcS.getNow() as NpcState;
            foreach(var x in )//������Ʒ
            npc.ecState.buyGoods;
        }
    }
}
