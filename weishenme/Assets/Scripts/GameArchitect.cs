using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    public MapSystem mapSystem;
    /// <summary>
    /// 能够接取生产力订单的对象们
    /// </summary>
    public HashSet<ICanReceiveProdOrder> receiveProdOrder;
    /// <summary>
    /// 能够接取普通商品订单的对象们
    /// </summary>
    public HashSet<ICanReceiveNormalGoodsOrder> receiveNormalGoodsOrder;
    /// <summary>
    /// 一系列的场景
    /// </summary>
    public List<SceneObj> scenes;
    public GameArchitect()
    {

    }
    public List<NpcObj> npcs;

    /// <summary>
    /// 更新循环
    /// </summary>
    public void Update()
    {
        ///对场景进行一系列的初始化,更新那些道路可以前进
        InitScene();
        ///更新每个NPC的计划,在这里可以发送订单请求,更新招聘的员工等
        //UpdateNpcsPlan();
        //将NPC的生产力传送给公司
        NpcBefGen();
        //更新每个道路,货物搬运
        PathUpdate();
        //建筑进行生产的更新
        BuildingUpdate();
        //人更新购买的物品 
        NpcBuyUpdate();
    }
    /// <summary>
    /// 更新场景的每一条道路,来判断是否可以走通
    /// </summary>
    public void InitScene()
    {
        foreach (SceneObj scene in scenes)//更新场景
        {
            if (true)//场景更新,即添加新路径,或减少路径
            {

            }
        }
    }
    /// <summary>
    /// 更新规划
    /// </summary>
    public void UpdateNpcsPlan()
    {
        foreach(var npc in npcs)
        {
            npc.ReThinkContract(npc.now);
        }
        ///对npc进行更新
        foreach (var npc in npcs)
        {
            npc.Plan(npc.now);
        }
    }
    /// <summary>
    /// 运输生产力
    /// </summary>
    public void NpcBefGen()
    {
        for (int i = 0; i < npcs.Count; i++)//资本收入,我是靠资本进行收入的
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.CapitalGains)
                npc.BefCapitalGains();
        }

        for (int i = 0; i < npcs.Count; i++)//雇佣其他人
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.SelfEmployment)
                npc.BefSelfEmployment();
        }

        for (int i = 0; i < npcs.Count; i++)//自己工作的同时雇佣别人
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.SelfAndOtherEmployment)
                npc.BefSelfAndOtherEmployment();
        }

        for (int i = 0; i < npcs.Count; i++)//固定收入
        {
            var npc = npcs[i];
            if (npc.now.ecState.contracts.incomeType == IncomeType.FixedSalary)
                npc.BefFixedSalaryUpdate();
        }
    }
    /// <summary>
    /// 道路公司更细它们的订单
    /// </summary>
    public void PathUpdate()
    {
        foreach(var scene in scenes)
        {
            scene.UpdatePathOrder();
        }
    }
    /// <summary>
    /// 对每个建筑进行更新
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
    /// 更新人购买的商品
    /// </summary>
    public void NpcBuyUpdate()
    {
        foreach( var npcS in npcs)
        {
            var npc = npcS.getNow() as NpcState;
            foreach(var x in )//购买商品
            npc.ecState.buyGoods;
        }
    }
}
