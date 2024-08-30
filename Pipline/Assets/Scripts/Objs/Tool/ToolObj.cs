using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductivityEnum
{
	UseHand,
}
public class ToolInf : GoodsInf
{
	public Dictionary<ProductivityEnum, int> dics;
}
public abstract class ToolObj<T>:GoodsObj<T>
where T:ToolInf
{
	public Productivity productivity;
	public Resource resource;
	public abstract Dictionary<ProductivityEnum, int> GetProducs();
	public virtual void UseTool(NpcObj npcObj, int num = 1,int time=1)
	{
		foreach (var eng in GetProducs())
		{
			productivity.productivities[eng.Key] += eng.Value*num*time;
			sum -= num;
		}
	}
	public virtual void ReleaseTool(NpcObj npcObj, int num = 1,int time=1)
	{
		foreach (var eng in GetProducs())
		{
			productivity.productivities[eng.Key] -= eng.Value*num*time;
			sum += num;
		}
	}
}

/// <summary>
/// 生产力类型
/// </summary>
public class Productivity
{
	public Resource resource;
	public BuildingObj building;
	public Dictionary<ProductivityEnum, int> productivities;
	public Productivity(Resource resource,BuildingObj building)
	{
		productivities = new Dictionary<ProductivityEnum, int>();
		foreach (var x in Enum.GetValues(typeof(ProductivityEnum)))
		{
			productivities.Add((ProductivityEnum)x,0);
		}
		this.resource = resource;
		this.building = building;
		resource.AddAddFunc(
			(e) =>
			{
				var tool = e as ToolObj;
				if (tool != null)
				{
					tool.productivity = building.productivity;
					tool.resource = resource;
				}
			}
		);
		resource.AddRemoveFunc(
			(e) =>
			{
				var tool = e as ToolObj;
				if (tool != null)
				{
					tool.productivity = null;
					tool.resource = null;
				}
			}
		);
	}
}