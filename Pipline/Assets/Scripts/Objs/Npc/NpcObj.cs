using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ɫ����
/// </summary>
public class NpcObj : BaseObj,IRegisterEvent
{
	/// <summary>
	/// ���ʽ
	/// </summary>
	public LifeStyle lifeStyle;
	/// <summary>
	/// ���ڵĳ���
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
/// ���ʽ
/// </summary>
public class LifeStyle
{
	public Job job;
	public SpareTimeWork timeWork;
	/// <summary>
	/// ��ǰ��npc0
	/// </summary>
	public NpcObj npc;
	/// <summary>
	/// �����ļ�
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