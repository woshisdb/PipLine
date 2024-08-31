using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseToolAct : Act
{
    int wasterTime;
    ToolObj tool;
    NpcObj npc;
    public override IEnumerator Run()
    {
        tool.UseTool(npc,1,WasterTime());
        yield break;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public UseToolAct(int wasterTime, ToolObj tool, NpcObj npc)
    {
        this.wasterTime = wasterTime;
        this.tool = tool;
        this.npc = npc;
    }
}

public class ReleaseToolAct : Act
{
    int wasterTime;
    ToolObj tool;
    NpcObj npc;
    public override IEnumerator Run()
    {
        tool.ReleaseTool(npc,1,WasterTime());
        yield break;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public ReleaseToolAct(int wasterTime, ToolObj tool, NpcObj npc)
    {
        this.wasterTime = wasterTime;
        this.tool = tool;
        this.npc = npc;
    }
}
