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
		resource.AddAddFunc((obj) =>//����
		{
			goodslist.Add(obj, 1);
		});
		resource.AddRemoveFunc( (obj) =>//ɾ��
		{
			goodslist.Remove(obj);
		});
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
	public LinkedList<int> nums;
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
	/// <summary>
	/// ������Դ,����ɱ�
	/// </summary>
	public override void Update()
	{
	}

	public OnceSource(Resource from, Resource to, Trans trans):base(from,to,trans)
	{
		nums = new LinkedList<int>();
		for (int i = 0; i < trans.edge.time; i++)
		{
			nums.AddFirst(0);
		}
	}
}
public class IterSource : Source
{
	public override void Update()
	{
	}
	public IterSource(Resource from, Resource to, Trans trans):base(from, to, trans)
	{
	}
}
public enum TransEnum
{
	one,
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

[Serializable]
public class EdgeItem
{
	[SerializeField]
	public ProductivityEnum x;
	[SerializeField]
	public int y;
}

[System.Serializable]
public class Node
{
	[SerializeField]
	public List<GoodsObj> source;
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

