using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>���Ըı乤��
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
/// ���Ըı乺���ʳ��
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
/// �뿪��ǰλ��ȥ������λ��
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

