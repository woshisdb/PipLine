using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Source
{
	public Trans trans;
	public LinkedList<int> nums;
	public Resource from;
	public Resource to;
	public int maxnCount = 99999;
	/// <summary>
	/// 更新资源,输入成本
	/// </summary>
	public abstract void Update();

	public Source(Resource from, Resource to, Trans trans)
	{
		this.trans = trans;
		this.from = from;
		this.to = to;
		maxnCount = 99999;
		nums = new LinkedList<int>();
		for (int i = 0; i < trans.edge.time; i++)
		{
			nums.AddFirst(0);
		}
	}
}
public class OnceSource:Source
{
	/// <summary>
	/// 更新资源,输入成本
	/// </summary>
	public override void Update()
	{
		//int count = maxnCount;
		//foreach (var t in trans.edge.tras)//转移时间
		//{
		//	count = Math.Min(obj.rates[t.x].tempCount / t.y, count);
		//}
		//for (var k = nums.Last; k != null; k = k.Previous)
		//{
		//	int sum = Math.Min(count, k.Value);
		//	count -= sum;
		//	foreach (var t in trans.edge.tras)
		//	{
		//		obj.rates[t.x].tempCount -= sum * t.y;
		//	}
		//	k.Value -= sum;
		//	if (k == nums.Last)
		//	{
		//		foreach (var data in trans.to.source)
		//		{
		//			to.Add(data.x, data.y * sum);
		//		}
		//	}
		//	else
		//	{
		//		k.Next.Value += sum;
		//	}
		//}
		//int maxC = 9999999;
		//foreach (var data in trans.from.source)
		//{
		//	maxC = Math.Min(maxC, resource.GetRemain(data.x) / data.y);
		//}
		//count = Math.Min(maxC, count);
		//maxnCount -= count;
		//if (count != 0)
		//	foreach (var data in trans.from.source)
		//	{
		//		from.Remove(data.x, data.y * count);
		//	}
		//nums.First.Value = count;
	}

    public OnceSource(Resource from, Resource to, Trans trans):base(from,to,trans)
	{
		//this.trans = trans;
		//this.from = from;
		//this.to = to;
		//maxnCount = 99999;
		//nums = new LinkedList<int>();
		//for (int i = 0; i < trans.edge.time; i++)
		//{
		//	nums.AddFirst(0);
		//}
	}
}
//资源需要持续提供
public class IterSource : Source
{
	/// <summary>
	/// 更新资源,输入成本
	/// </summary>
	public override void Update()
	{
		//int count = maxnCount;
		//int maxC = 9999999;
		//foreach (var data in trans.from.source)
		//{
		//	maxC = Math.Min(maxC, from.GetRemain(data.x) / data.y);
		//}
		//count = Math.Min(maxC, count);
		//foreach (var t in trans.edge.tras)//转移时间
		//{
		//	count = Math.Min(obj.rates[t.x].tempCount / t.y, count);
		//}
		//for (var k = nums.Last; k != null; k = k.Previous)
		//{
		//	int sum = Math.Min(count, k.Value);
		//	count -= sum;
		//	foreach (var t in trans.edge.tras)
		//	{
		//		obj.rates[t.x].tempCount -= sum * t.y;
		//	}
		//	foreach (var data in trans.from.source)
		//	{
		//		to.Remove(data.x, data.y * sum);
		//	}
		//	k.Value -= sum;
		//	if (k == nums.Last)
		//	{
		//		foreach (var data in trans.to.source)
		//		{
		//			to.Add(data.x, data.y * sum);
		//		}
		//	}
		//	else
		//	{
		//		k.Next.Value += sum;
		//	}

		//}
		//maxnCount -= count;
		//if (count != 0)
		//	foreach (var data in trans.from.source)
		//	{
		//		from.Remove(data.x, data.y * count);
		//	}
		//nums.First.Value = count;

	}
	public IterSource(Resource from, Resource to, Trans trans) : base(from, to, trans)
	{
	}
}
public enum TransEnum
{
	/// <summary>
	/// 一次性生产出来
	/// </summary>
	one,
	/// <summary>
	/// 可以持续产出
	/// </summary>
	conti
}
/// <summary>
/// 物体转移关系
/// </summary>
[System.Serializable]
public class Trans
{
	[SerializeField]
	public string title;
	[SerializeField]
	public Node from;
	[SerializeField]
	public Node to;
	[SerializeField]
	public Edge edge;
	[SerializeField]
	public TransEnum transEnum;
	public Source AddSource(Resource from,Resource to)
	{
		if (transEnum == TransEnum.one)
			return new OnceSource(from,to, this);
		else
			return new IterSource(from, to, this);
	}
}
///// <summary>
///// 持续需要资源的转移关系
///// </summary>
//[System.Serializable]
//public class IterTrans:Trans
//{
//	public override Source AddSource(Obj obj, Trans trans)
//	{
//		return new IterSource((BuildingObj)obj, ((BuildingObj)obj).resource, trans);
//	}
//}

public enum TransationEnum
{
	/// <summary>
	/// 烹饪食物
	/// </summary>
	cook,
	/// <summary>
	/// 耕种农作物
	/// </summary>
	gengZhong,
	/// <summary>
	/// 采摘作物
	/// </summary>
	shouHuo,
	/// <summary>
	/// 栽种植物
	/// </summary>
	zaiZhong,
	/// <summary>
	/// 砍树或是拆卸
	/// </summary>
	qieGe,
	/// <summary>
	/// 搭建建筑
	/// </summary>
	daJian,
	/// <summary>
	/// 规划建筑设备
	/// </summary>
	guiHua,
	/// <summary>
	/// 安装设备
	/// </summary>
	anZhuang,
	/// <summary>
	/// 制作工具
	/// </summary>
	zhiZuo,
	/// <summary>
	/// 开采
	/// </summary>
	kaiCai,
}
public enum SitEnum
{
	/// <summary>
	/// 床
	/// </summary>
	bed,
	/// <summary>
	/// 座位
	/// </summary>
	set
}


[Serializable]
public class EdgeItem
{
	[SerializeField]
	public TransationEnum x = TransationEnum.cook;
	[SerializeField]
	public int y;
}

[System.Serializable]
public class Node
{
	[SerializeField]
	public List<GoodsObj> source;//多少资源
}
[System.Serializable]
public class Edge
{
	/// <summary>
	/// 一系列转移规则
	/// </summary>
	[SerializeField]
	public List<EdgeItem> tras;
	/// <summary>
	/// 花费的时间
	/// </summary>
	public int time;
}

public struct TransNode
{
	public Trans trans;
	public Resource from;
	public Resource to;
}

/// <summary>
/// 管线管理器，管理生产商品
/// </summary>
public class PipLineManager
{
	public List<Source> piplines;
	public void SetTrans(List<TransNode> trans)
	{
		piplines.Clear();
		foreach (var x in trans)
		{
			piplines.Add(x.trans.AddSource(x.from,x.to));
		}
	}
	public PipLineManager()
	{
	}
}

