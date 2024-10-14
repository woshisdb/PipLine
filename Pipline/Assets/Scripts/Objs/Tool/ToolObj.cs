using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolInf : GoodsInf
{
    [SerializeField]
	public Dictionary<ProductivityEnum, int> dics;
	public ToolInf()
    {
		dics = new Dictionary<ProductivityEnum, int>();
    }
}
public class ToolObj:GoodsObj
{
	public Resource resource;
	public Source source;
	public virtual Dictionary<ProductivityEnum, int> GetProducs()
    {
		return ((ToolInf)get()).dics;
	}
	public virtual void UseTool(NpcObj npc,int wasterTime, ref Dictionary<ProductivityEnum, int> ret)
	{
		foreach (var eng in GetProducs())
		{
			ret[eng.Key] += eng.Value* wasterTime;
		}
	}
	public virtual void ReleaseTool(NpcObj npc,ref Dictionary<ProductivityEnum, int> ret)
	{
		foreach (var eng in GetProducs())
		{
			ret[eng.Key] += eng.Value;
		}
	}
	/// <summary>
	/// ��ʼ��Ҫ�����Ĺ���
	/// </summary>
	public void Init(Resource resource,Source source)
    {
		this.resource = resource;
		this.source = source;
    }
}

/// <summary>
/// ����������
/// </summary>
public class Productivity
{
	/// <summary>
	/// ʹ�ù�����
	/// </summary>
	public ToolObj toolObj;
	public BuildingObj building;
	public Dictionary<ProductivityEnum, int> productivities;//��ǰʱ�䲽������������
	public Productivity(BuildingObj building)
	{
		productivities = new Dictionary<ProductivityEnum, int>();
		foreach (var x in Enum.GetValues(typeof(ProductivityEnum)))
		{
			productivities.Add((ProductivityEnum)x,0);
		}
		this.building = building;
	}
	/// <summary>
	/// NPC��ʹ�ù���
	/// </summary>
	/// <param name="npc"></param>
	public void UseTools(List<NpcObj> npcs,int wasterTime=8)
    {		
		for(int i = 0; i < npcs.Count; i++)
        {
			var npc = npcs[i];
			toolObj.UseTool(npc,wasterTime,ref productivities);
        }
	}

	public int this[ProductivityEnum index]
	{
		get
		{
			if(productivities.ContainsKey(index))
            {
				return productivities[index];
            }
			else
            {
				return 0;
            }
		}
		set
        {
			productivities[index] = value;
        }
	}

}