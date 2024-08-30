using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : Resource
{
	/// <summary>
	/// ��Ʒ�ͼ۸�
	/// </summary>
	public Dictionary<GoodsObj, int> goodslist;
	public Resource resource;
	public GoodsManager(Resource resource)
	{
		this.resource = resource;
		resource.add = (obj) =>//����
		{
			goodslist.Add(obj, 1);
		};
		resource.remove = (obj) =>//ɾ��
		{
			goodslist.Remove(obj);
		};
		goodslist = new Dictionary<GoodsObj, int>();
		foreach (var x in resource.goods)
		{
			goodslist.Add(x, 1);
		}
	}
}

public abstract class Source
{
	public Trans trans;
	public Resource from;
	public Resource to;
	/// <summary>
	/// ������Դ,����ɱ�
	/// </summary>
	public abstract void Update();

	public Source(Resource from, Resource to, Trans trans)
	{
		this.from = from;
		this.to = to;
		this.trans = trans;
	}
}
public class OnceSource:Source
{
	public Trans trans;
	public LinkedList<int> nums;
	public Resource from;
	public Resource to;
	public int maxnCount = 99999;
	/// <summary>
	/// ������Դ,����ɱ�
	/// </summary>
	public override void Update()
	{
		int count = maxnCount;
		foreach (var t in trans.edge.tras)
		{
			count = Math.Min(obj.rates[t.x].tempCount / t.y, count);
		}
		for (var k = nums.Last; k != null; k = k.Previous)
		{
			int sum = Math.Min(count, k.Value);
			count -= sum;
			foreach (var t in trans.edge.tras)
			{
				obj.rates[t.x].tempCount -= sum * t.y;
			}
			k.Value -= sum;
			if (k == nums.Last)
			{
				foreach (var data in trans.to.source)
				{
					to.Add(data.x, data.y * sum);
				}
			}
			else
			{
				k.Next.Value += sum;
			}
		}
		int maxC = 9999999;
		foreach (var data in trans.from.source)
		{
			maxC = Math.Min(maxC, resource.GetRemain(data.x) / data.y);
		}
		count = Math.Min(maxC, count);
		maxnCount -= count;
		if (count != 0)
			foreach (var data in trans.from.source)
			{
				from.Remove(data.x, data.y * count);
			}
		nums.First.Value = count;
	}

	public OnceSource(Resource from, Resource to, Trans trans):base(from,to,trans)
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
	/// ������Դ,����ɱ�
	/// </summary>
	public override void Update()
	{
		//int count = maxnCount;
		//foreach (var t in trans.edge.tras)//ת��ʱ��
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
//��Դ��Ҫ�����ṩ
public class IterSource : Source
{
	/// <summary>
	/// ������Դ,����ɱ�
	/// </summary>
	public override void Update()
	{
		//int count = maxnCount;
		//int maxC = 9999999;
		//foreach (var data in trans.from.source)
		//{
		//	maxC = Math.Min(maxC, from.GetRemain(data.x) / data.y);
		//}
		int count = maxnCount;
		int maxC = 9999999;
		foreach (var data in trans.from.source)
		{
			maxC = Math.Min(maxC, from.Get(data) / data.y);
		}
		count = Math.Min(maxC, count);
		foreach (var t in trans.edge.tras)//ת��ʱ��
		{
			count = Math.Min(obj.rates[t.x].tempCount / t.y, count);
		}
		for (var k = nums.Last; k != null; k = k.Previous)
		{
			int sum = Math.Min(count, k.Value);
			count -= sum;
			foreach (var t in trans.edge.tras)
			{
				obj.rates[t.x].tempCount -= sum * t.y;
			}
			foreach (var data in trans.from.source)
			{
				to.Remove(data.x, data.y * sum);
			}
			k.Value -= sum;
			if (k == nums.Last)
			{
				foreach (var data in trans.to.source)
				{
					to.Add(data.x, data.y * sum);
				}
			}
			else
			{
				k.Next.Value += sum;
			}

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
	/// һ������������
	/// </summary>
	one,
	/// <summary>
	/// ���Գ�������
	/// </summary>
	conti
}
/// <summary>
/// ����ת�ƹ�ϵ
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
///// ������Ҫ��Դ��ת�ƹ�ϵ
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
	/// ���ʳ��
	/// </summary>
	cook,
	/// <summary>
	/// ����ũ����
	/// </summary>
	gengZhong,
	/// <summary>
	/// ��ժ����
	/// </summary>
	shouHuo,
	/// <summary>
	/// ����ֲ��
	/// </summary>
	zaiZhong,
	/// <summary>
	/// �������ǲ�ж
	/// </summary>
	qieGe,
	/// <summary>
	/// �����
	/// </summary>
	daJian,
	/// <summary>
	/// �滮�����豸
	/// </summary>
	guiHua,
	/// <summary>
	/// ��װ�豸
	/// </summary>
	anZhuang,
	/// <summary>
	/// ��������
	/// </summary>
	zhiZuo,
	/// <summary>
	/// ����
	/// </summary>
	kaiCai,
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
	public List<GoodsObj> source;//������Դ
}
[System.Serializable]
public class Edge
{
	/// <summary>
	/// һϵ��ת�ƹ���
	/// </summary>
	[SerializeField]
	public List<EdgeItem> tras;
	/// <summary>
	/// ���ѵ�ʱ��
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
/// ���߹�����������������Ʒ
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

