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
	public virtual Dictionary<ProductivityEnum, int> GetProducs()
    {
		return ((ToolInf)get()).dics;
	}
	public virtual void UseTool(NpcObj npcObj,PipLineManager pipLine,string name, int num = 1,int time=1)
	{
		var res = pipLine.GetTrans(name);
		foreach (var eng in GetProducs())
		{
			((Source)res).productivity.productivities[eng.Key] += eng.Value*num*time;
		}
		sum -= num;
	}
	public virtual void ReleaseTool(NpcObj npcObj, PipLineManager pipLine, string name, int num = 1,int time=1)
	{
		var res = pipLine.GetTrans(name);
		foreach (var eng in GetProducs())
		{
			((Source)res).productivity.productivities[eng.Key] += eng.Value * num * time;
		}
		sum += num;
	}
}

/// <summary>
/// 生产力类型
/// </summary>
public class Productivity
{
	public Resource resource;
	public BuildingObj building;
	public Dictionary<ProductivityEnum, int> productivities;//当前时间步产生的生产力
	public Dictionary<ProductivityEnum, int> remain;
	public Productivity(Resource resource,BuildingObj building)
	{
		productivities = new Dictionary<ProductivityEnum, int>();
		remain = new Dictionary<ProductivityEnum, int>();
		foreach (var x in Enum.GetValues(typeof(ProductivityEnum)))
		{
			productivities.Add((ProductivityEnum)x,0);
			remain.Add((ProductivityEnum)x,0);
		}
		this.resource = resource;
		this.building = building;
		building.resource.AddAddFunc(
			(e) =>
			{
				var tool = e as ToolObj;
				if (tool != null)
				{
					tool.resource = resource;
				}
			}
		);
		building.resource.AddRemoveFunc(
			(e) =>
			{
				var tool = e as ToolObj;
				if (tool != null)
				{
					tool.resource = null;
				}
			}
		);
	}
	public int this[ProductivityEnum index]
	{
		get
		{
			if(remain.ContainsKey(index))
            {
				return remain[index];
            }
			else
            {
				return 0;
            }
		}
	}

}