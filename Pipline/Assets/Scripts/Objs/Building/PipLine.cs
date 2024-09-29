using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

public class GoodsManager : Resource,IRegisterEvent
{
	public Dictionary<GoodsEnum, Money> goodslist;
	public Resource resource;
	public int FindGoodsCost(GoodsEnum goodsObj)
    {
		if(goodslist.ContainsKey(goodsObj))
        {
			return goodslist[goodsObj].money;
        }
		else
        {
			return 0;
        }
    }
	public GoodsManager(Resource resource) : base(resource.building)
	{
		this.resource = resource;
		var from =building.pipLineManager.piplineSource.trans.from.source;
		goodslist = new Dictionary<GoodsEnum, Money>();
		goodslist.Add(building.outputGoods, new Money());
		goodslist[building.outputGoods].money = InitGoodsMoney(building.outputGoods);
	}
	/// <summary>
	/// 商品的最低价格
	/// </summary>
	/// <param name="goods"></param>
	/// <returns></returns>
	public int InitGoodsMoney(GoodsEnum goods)
	{
		if (GameArchitect.get.economicSystem==null)
        {
			return GoodsGen.GetGoodsInf(goods).price;//价格围绕价值上下波动
        }
		var ave=GameArchitect.get.economicSystem.GoodsAveCost(goods);
		return ave;
	}
}
public class Pair<T,F>
{
	public T Item1;
	public F Item2;
	public Pair()
    {

    }
	public Pair(T item1, F item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}
public abstract class Source
{
	public Trans trans;//商品间的转移关系
	public Resource from;
	public Resource to;
	public Productivity productivity;
	public BuildingObj belong;
    public abstract void Update();
}
public class PipLineSource:Source
{
	public int maxSum= 99999999;//每回合生产资源的数目
	public CircularQueue<Pair<Edge, int>> trasSource;
	public override void Update()
    {
		foreach(var x in productivity.productivities)
        {
			productivity.remain[x.Key] = x.Value;
        }
		int sum = maxSum;
		foreach(var x in trans.edge.tras)
        {
			sum = Math.Min(sum, productivity[x.Key] /x.Value);
        }
		var sourceMax = 99999999;
		foreach(var x in trans.from.source)
        {
            sum = Math.Min(sum, from.Get(x.Item1) / x.Item2);
            sourceMax = Math.Max(sourceMax, from.Get(x.Item1) / x.Item2);
        }
		foreach (var x in trans.from.source)
		{
			int sumV = x.Item2 * sum;
			from.Remove(x.Item1, sumV);
		}
		foreach (var x in trans.edge.tras)
		{
			productivity.remain[x.Key] -= sum * x.Value;
		}
        GameArchitect.get.economicSystem.buildingGoodsPrices[belong].history.Find(0).pipLineHistory.GoodsCreate += sum;
		if (trans.wasterTimes == 1)
		{
			//添加到里面
			foreach (var retS in trans.to.source)
			{
				int sumV = retS.Item2 *sum;
				to.Add(retS.Item1, sumV);//将商品添加到里面商品列表
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
				productivity.remain[x.Key] -= val;
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
					x.Item2 *= sum;
                    from.Remove(x.Item1,x.Item2);
                    x.Item2 /= sum;
				}
			}
			if(i==0)
            {
				//添加到里面
				foreach(var retS in trans.to.source)
                {
					retS.Item2 *= retsum;
                    to.Add(retS.Item1,retS.Item2);
                    retS.Item2 /= retsum;
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
	public PipLineSource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
    {
		this.belong = building;
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
	public Source AddSource(Resource from,Resource to,BuildingObj building)
	{
		return TransEnum.AddSource(title,from, to, this,building);
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
	public List<Pair<GoodsEnum,int>> source;
	public Node()
    {
		source=new List<Pair<GoodsEnum, int>>();
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
	public TransNode(Trans trans,Resource from,Resource to)
    {
		this.trans = trans;
		this.from = from;
		this.to = to;
    }
}

/// <summary>
/// ���߹�����������������Ʒ
/// </summary>
public class PipLineManager
{
	public BuildingObj buildingObj;
	public List<Source> piplines;
	public Dictionary<string, Source> pairs;
	/// <summary>
	/// 管线
	/// </summary>
	public PipLineSource piplineSource { get { return (PipLineSource)GetTrans(buildingObj.mainWorkName); } }
	public CarrySource carrySource { get { return (CarrySource)GetTrans("搬运商品"); } }
	public void SetTrans(List<TransNode> trans)
	{
		piplines.Clear();
		pairs.Clear();
		foreach (var x in trans)
		{
			var data = x.trans.AddSource(x.from, x.to, buildingObj);
			piplines.Add(data);
			pairs.Add(x.trans.title,data);
		}
	}
	public Source GetTrans(string name)
    {
		if (pairs.ContainsKey(name))
			return pairs[name];
		else
			return null;
    }
	public PipLineManager(BuildingObj buildingObj)
    {
        piplines = new List<Source>();
		pairs = new Dictionary<string, Source>();
        this.buildingObj = buildingObj;
    }
	public Trans FindInput(GoodsEnum goods)
    {
		foreach(var x in piplines)
        {
			var ret=x.trans.to.source.Find(e => { return e.Item1 == goods; });
			if (ret!=null)
            {
				return x.trans;
            }
        }
		return null;
    }
}

public enum ProductivityEnum
{
	KaiCai,
}