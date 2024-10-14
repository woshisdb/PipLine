using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsEnum
{
    goods1,
    goods2,
}
public enum ProdEnum
{
    prod1,
}

public interface MetaI
{

}

public class GoodsStateMeta : MetaI
{
    /// <summary>
    /// 需要的生产力列表
    /// </summary>
    public Tuple<ProdEnum, Int>[] prods = {
        new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
    };
    /// <summary>
    /// 输入列表
    /// </summary>
    public Tuple<GoodsEnum, Int>[] inputs = {
        new Tuple<GoodsEnum, Int>(GoodsEnum.goods1,1)
    };
    /// <summary>
    /// 输出列表
    /// </summary>
    public Tuple<GoodsEnum, Int> output = new Tuple<GoodsEnum, Int>(GoodsEnum.goods2, 1);
}

public class Meta
{
    public static Dictionary<GoodsEnum, GoodsInf> goodsInfs;
    public static Dictionary<Type, MetaI> pairs;
    public static MetaI GetMeta<T>()
    {
        return pairs[typeof(T)];
    }

    public Meta()
    {
        goodsInfs = new Dictionary<GoodsEnum, GoodsInf>();//商品信息
        goodsInfs.Add(GoodsEnum.goods1, new GoodsInf(1, GoodsEnum.goods1, () => { return new Goods1Obj(); }));
        goodsInfs.Add(GoodsEnum.goods2, new GoodsInf(1, GoodsEnum.goods2, () => { return new Goods2Obj(); }));
        pairs = new Dictionary<Type, MetaI>();//Meta数据
        pairs[typeof(GoodsBuildingState)]= new GoodsStateMeta();
    }
}
