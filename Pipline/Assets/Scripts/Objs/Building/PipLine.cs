using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;
/// <summary>
/// 商品管理器
/// </summary>
public class GoodsManager : IRegisterEvent
{
	/// <summary>
	/// 一系列的商品
	/// </summary>
	public HashSet<GoodsObj> goodslist { get { return resource.goods; } }
	public Resource resource;
	public BuildingObj buildingObj;
	public int FindGoodsCost(GoodsEnum goodsObj)
    {
		var x=GoodsGen.GetGoodsObj(goodsObj);
		goodslist.TryGetValue(x, out var cost);
		if (cost == null)
			return cost.price.money;
		else
			return 0;
    }
	public GoodsManager(Resource resource)
	{
		this.buildingObj = resource.building;
		this.resource = resource;
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
    public int wasterTimes;
    public abstract void Update();
}
//public class PipLineSource:Source
//{
//    public override void Update()
//    {
//        double prodSum = maxSum;//生产力能生产的数目
//        // 计算最小的生产力比例
//        foreach (var edge in trans.edge.tras)
//        {
//			prodSum = Math.Min(prodSum, ((double)productivity[edge.Key]) / ((double)edge.Value));
//        }
//        int sourceMax = int.MaxValue;//能生产商品的数目
//        foreach (var source in trans.from.source)
//        {
//            sourceMax = Math.Min(sourceMax, from.Get(source.Item1) / source.Item2);
//        }
//        // 移除生产过程中的原材料
//        foreach (var source in trans.from.source)
//        {
//            int sumValue = source.Item2 * sourceMax;
//            from.Remove(source.Item1, sumValue);
//        }

//        // 减少生产力
//        foreach (var edge in trans.edge.tras)
//        {
//            productivity[edge.Key] -= (int)(prodSum * edge.Value);
//        }
//		piplineList[0] = sourceMax;//剩余产品的数量
//		// 循环处理每个传输阶段
//		for (int i = piplineList.Count-1; i>=1; i--)
//        {
//			var val=piplineList[i];
//			var temp= Math.Min(prodSum, val);
//			prodSum -= temp;
//			piplineList[i]-=temp;
//			var createSum = ((int)Math.Ceiling(val)-(int)Math.Ceiling(piplineList[i]));
//			if (i==piplineList.Count-1)//最后一个
//            {
//                to.Add(trans.to.source[0].Item1,createSum* trans.to.source[0].Item2);
//            }
//			else
//            {
//				piplineList[i + 1] += createSum; 
//            }
//        }
//    }

//    public PipLineSource(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
//    {
//		this.belong = building;
//        this.from = from;
//        this.to = to;
//        this.trans = trans;
//        wasterTimes =trans.wasterTimes;
//        piplineList = new List<double>();
//        for(int i=0;i<wasterTimes;i++)
//        {
//            piplineList.Add(0);
//        }
//        this.productivity = productivity;
//    }
//}
public class Trans
{
	[OdinSerialize]
	public string title;
	[OdinSerialize]
	public Node from;
	[OdinSerialize]
	public Pair<GoodsEnum, int> to;
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
		to=new Pair<GoodsEnum, int>();
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
	public Node(params Pair<GoodsEnum, int>[] pairs)
    {
		source=new List<Pair<GoodsEnum, int>>();
		source.AddRange(pairs);
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
	public Edge(params Pair<ProductivityEnum, int>[] pairs)
    {
		tras = new Dictionary<ProductivityEnum, int>();
		foreach(var x in pairs)
        {
			tras[x.Item1] = x.Item2;
        }
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
/// 管线管理器
/// </summary>
public class PipLineManager
{ 
	public BuildingObj buildingObj;
	public List<Pipline> pairs;
	public PipLineManager(BuildingObj buildingObj)
    {
		pairs = new List<Pipline>();
        this.buildingObj = buildingObj;
    }
}

public enum ProductivityEnum
{
	KaiCai,
}