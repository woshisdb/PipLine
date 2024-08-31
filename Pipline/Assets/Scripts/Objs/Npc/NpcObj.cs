using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObj : BaseObj
{
	public Act befAct;
	public Act endAct;
	/// <summary>
	/// ���ʽ
	/// </summary>
	public LifeStyle lifeStyle;
	/// <summary>
	/// ���ڵĳ���
	/// </summary>
	public SceneObj belong;
}

public class SpareTimeWork
{
	public Act spareTimeAct;
	public NpcObj npc;
	public SpareTimeWork(NpcObj npc)
    {
        this.npc = npc;
    }
    public int getWasterTime()
    {
		return spareTimeAct.WasterTime();
    }
}

/// <summary>
/// ���ʽ
/// </summary>
public class LifeStyle
{
	public Job job;
	public SpareTimeWork timeWork;
	public NpcObj npc;
	public LifeStyle(NpcObj npc)
    {
		this.npc=npc;
		this.job = new Job(npc);
		this.timeWork = new SpareTimeWork(npc);
    }
}