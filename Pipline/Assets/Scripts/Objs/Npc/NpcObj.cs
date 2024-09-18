using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色集合
/// </summary>
public class NpcObj : BaseObj,IRegisterEvent
{
	/// <summary>
	/// 生活方式
	/// </summary>
	public LifeStyle lifeStyle;
	/// <summary>
	/// 属于的场景
	/// </summary>
	public SceneObj belong;
	public string name;
	public Money money;
	public NpcObj()
    {
		money = new Money(10);
		GameArchitect.get.npcs.Add(this);
		name = "N" + GameArchitect.get.npcs.Count;
		lifeStyle = new LifeStyle(this);
    }
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
/// 生活方式
/// </summary>
public class LifeStyle
{
	public Job job;
	public SpareTimeWork timeWork;
	/// <summary>
	/// 当前的npc0
	/// </summary>
	public NpcObj npc;
	/// <summary>
	/// 所属的家
	/// </summary>
	public SceneObj belong { get { return npc.belong; } }
	public LifeStyle(NpcObj npc)
    {
		this.npc=npc;
		this.job = null;
		//job.npc = npc;
		this.timeWork = new SpareTimeWork(npc);
    }
}