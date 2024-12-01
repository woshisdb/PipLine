using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>尝试改变工作
/// </summary>
public class NpcSetWork : NpcAction
{
    public NpcObj npc;
    public NpcSetWork(NpcObj npc)
    {
        this.npc = npc;
    }

    public override bool Condition(NpcObj state)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(NpcObj state)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// 尝试改变购买的食物
/// </summary>
public class NpcSetByFood:NpcAction
{
    public NpcObj npc;
    public NpcSetByFood(NpcObj npc)
    {
        this.npc = npc;
    }

    public override bool Condition(NpcObj state)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(NpcObj state)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// 离开当前位置去其他的位置
/// </summary>
public class NpcMoveBelongScene : NpcAction
{
    public NpcObj npc;
    public NpcMoveBelongScene(NpcObj npc)
    {
        this.npc = npc;
    }

    public override bool Condition(NpcObj state)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(NpcObj state)
    {
        throw new System.NotImplementedException();
    }
}

