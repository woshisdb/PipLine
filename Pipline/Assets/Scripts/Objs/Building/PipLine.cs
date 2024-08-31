using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
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
public class Pair<T,F>
{
	public T Item1;
	public F Item2;
}
public class Source
{
	public Trans trans;//商品间的转移关系
	public Resource from;
	public Resource to;
	/// <summary>
	/// 表示
	/// </summary>
	public CircularQueue<Pair<Edge, int>> trasSource;
	public Productivity productivity;
	public void Update()
    {
		int sum = 999999999;
		foreach(var x in trans.edge.tras)
        {
			sum = Math.Min(sum, productivity[x.Key] /x.Value);
        }
		var sourceMax = 99999999;
		foreach(var x in trans.from.source)
        {
			sum = Math.Min(sum,from.Get(x)/x.sum);
			sourceMax = Math.Max(sourceMax, from.Get(x) / x.sum);
        }
		foreach(var x in trans.from.source)
        {
			x.sum *= sum;
			from.remove(x);
			x.sum /= sum;
        }
		foreach (var x in trans.edge.tras)
		{
			productivity.productivities[x.Key] -= sum * x.Value;
		}
		if (trans.wasterTimes == 1)
		{
			//添加到里面
			foreach (var retS in trans.to.source)
			{
				retS.sum *= sum;
				to.Add(retS);
				retS.sum /= sum;
			}
			return;
		}
		for (int i=1;i<trans.wasterTimes;i++)//倒数某个元素
        {
			var node=trasSource.Find(trans.wasterTimes-i-1);//拿到节点
			int retsum = Math.Min(node.Item2,sum);//先升级
			bool canPro = true;
			foreach(var x in node.Item1.tras)
            {
                int val = Math.Min(trans.edge[x.Key] - productivity[x.Key], productivity[x.Key]);
				node.Item1.tras[x.Key] += val;
				productivity.productivities[x.Key] -= val;
                if (node.Item1.tras[x.Key]< trans.edge[x.Key])
                {
					canPro = false;
                }
			}
			sum -= retsum;
			node.Item2 = retsum;
			if (canPro&& sourceMax>0)
            {
				retsum++;
				sourceMax--;
				foreach (var x in node.Item1.tras)
				{
					node.Item1.tras[x.Key] = 0;
				}
				foreach (var x in trans.from.source)
				{
					x.sum *= sum;
					from.remove(x);
					x.sum /= sum;
				}
			}
			if(i==0)
            {
				//添加到里面
				foreach(var retS in trans.to.source)
                {
					retS.sum*= retsum;
					to.Add(retS);
					retS.sum /= retsum;
                }
            }
			else
            {
				var after = trasSource.Find(trans.wasterTimes - i);
				after.Item2 += retsum;
			}
        }
		var last=trasSource.Find(0);
		last.Item2 += sum;
    }

	public Source(Resource from, Resource to, Trans trans, Productivity productivity)
    {
        this.from = from;
        this.to = to;
        this.trans = trans;
		trasSource = new CircularQueue<Pair<Edge, int>>(trans.wasterTimes-1);
        this.productivity = productivity;
    }
}
public class Trans
{
	[OdinSerialize]
	public string title;
	[OdinSerialize]
	public Node from;
	[OdinSerialize]
	public Node to;
	[OdinSerialize]
	public Edge edge;
	/// <summary>
	/// 所需要的时间
	/// </summary>
	public int wasterTimes;
	public Source AddSource(Resource from,Resource to,Productivity productivity)
	{
		return new Source(from,to, this,productivity);
	}
	public Trans()
    {
		from=new Node();
		to=new Node();
		edge=new Edge();
    }
}

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
	public Node()
    {
		source=new List<GoodsObj>();
    }
}
[System.Serializable]
public class Edge
{
	/// <summary>
	/// 
	/// </summary>
	[SerializeField]
	public Dictionary<ProductivityEnum,int> tras;
	public Edge()
    {
		tras = new Dictionary<ProductivityEnum, int>();
    }
	public int this[ProductivityEnum index]
	{
		get
		{
            if (tras.ContainsKey(index))
            {
				return tras[index];
            }
			else
            {
				return 0;
            }
		}
	}

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
	public BuildingObj buildingObj;
	public List<Source> piplines;
	public void SetTrans(List<TransNode> trans)
	{
		piplines.Clear();
		foreach (var x in trans)
		{
			piplines.Add(x.trans.AddSource(x.from,x.to,buildingObj.productivity));
		}
	}
	public PipLineManager(BuildingObj buildingObj)
    {
        piplines = new List<Source>();
        this.buildingObj = buildingObj;
    }
}

public enum ProductivityEnum
{
	KaiCai,
}