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
		money = new Money();
		GameArchitect.get.npcs.Add(this);
		name = "N" + GameArchitect.get.npcs.Count;
		lifeStyle = new LifeStyle(this);
		lifeStyle.needManager.Init();
	}
	public NeedManager GetNeedManager()
    {
		return lifeStyle.needManager;
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
/// ����ĸ���
/// </summary>
public class NeedTrait
{
	public NeedManager needs;
	public NeedTrait(NeedManager needManager)
    {
		this.needs = needManager;
	}
	/// <summary>
	/// ��������
	/// </summary>
	public void Update()
    {
		needs.lifeNeed.rate = 0.4;//������������
		needs.resourceNeed.rate = 0.3;//����Դ������
		needs.amuseNeed.rate = 0.3;//�����ֵ�����
    }
}

public class NeedManager
{
	public NpcObj npc;
	//********************************
	public LifeNeed lifeNeed;
	public ResourceNeed resourceNeed;
	public AmuseNeed amuseNeed;
	//********************************
	public NeedTrait needTrait;
	//********************************
	public FoodSelector foodSelector;
	public NeedManager(NpcObj npc)
    {
		this.npc = npc;
		lifeNeed = new LifeNeed(npc);
		resourceNeed = new ResourceNeed(npc);
		amuseNeed = new AmuseNeed(npc);
		foodSelector = new FoodSelector(npc);
		needTrait = new NeedTrait(this);
	}
	public void Init()
    {
		foodSelector.Init();
	}
	public void Update()
    {
		needTrait.Update();
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
	public NeedManager needManager;
	/// <summary>
	/// �����ļ�
	/// </summary>
	public SceneObj belong { get { return npc.belong; } }
	public LifeStyle(NpcObj npc)
    {
		this.npc=npc;
		this.job = null;
		this.timeWork = new SpareTimeWork(npc);
		needManager=new NeedManager(npc);
    }
}
