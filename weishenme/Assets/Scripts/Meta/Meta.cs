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


public enum BuildingEnum
{
    building1,
}

public interface MetaI
{

}

public enum GoodsStateEnum
{
    source,//原材料生产型:不需要进口只需要出口
    process,//中间产物生成
    final,//最终生产商品
}

public class Meta:Singleton<Meta>
{
    public static int dayTime;
    /// <summary>
    /// 商品的信息
    /// </summary>
    public static Dictionary<GoodsEnum, GoodsInf> goodsInfs;
    public static Dictionary<BuildingEnum, GoodsInf> buildingInfs;
    /// <summary>
    /// 生产商品的信息
    /// </summary>
    public static Dictionary<Type, MetaI> piplineInf;
    public static MetaI GetMeta<T>()
    {
        return piplineInf[typeof(T)];
    }

    public Meta()
    {
        goodsInfs = new Dictionary<GoodsEnum, GoodsInf>();//商品信息
        goodsInfs.Add(GoodsEnum.goods1, new GoodsInf(1, GoodsEnum.goods1, () => { return new Goods1Obj(); }));
        goodsInfs.Add(GoodsEnum.goods2, new GoodsInf(1, GoodsEnum.goods2, () => { return new Goods2Obj(); }));
        piplineInf = new Dictionary<Type, MetaI>();//Meta数据
        piplineInf[typeof(GoodsBuildingObj)]= new GoodsStateMeta();
    }
}
