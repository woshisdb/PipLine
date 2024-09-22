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
/// 需求的更新
/// </summary>
public class NeedTrait
{
	public NeedManager needs;
	public NeedTrait(NeedManager needManager)
    {
		this.needs = needManager;
	}
	/// <summary>
	/// 更新需求
	/// </summary>
	public void Update()
    {
		needs.lifeNeed.rate = 0.4;//对生命的需求
		needs.resourceNeed.rate = 0.3;//对资源的需求
		needs.amuseNeed.rate = 0.3;//对娱乐的需求
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
	public NeedManager needManager;
	/// <summary>
	/// 所属的家
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
