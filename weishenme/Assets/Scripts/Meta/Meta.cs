using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsEnum
{
    goods1,
    goods2,
    goods3,
}
public enum ProdEnum
{
    prod1,
}


public enum BuildingEnum
{
    building1,
    building0,
    houseBuilding,
}

public enum PathEnum
{
    path0
}

public interface MetaI
{
    public string RetText();
}

public interface MetaI<T>: MetaI
{
    public T ReturnEnum();
}

//public enum GoodsStateEnum
//{
//    source,//原材料生产型:不需要进口只需要出口
//    process,//中间产物生成
//    final,//最终生产商品
//}

public abstract class PathMeta : MetaI<PathEnum>
{
    public GameObject view;
    public PathEnum pathEnum;
    public PathMeta(PathEnum pathEnum)
    {
        this.pathEnum = pathEnum;
    }
    public string RetText()
    {
        return pathEnum.ToString();
    }

    public PathEnum ReturnEnum()
    {
        return this.pathEnum;
    }
    public abstract PathObj createPathObj();
}

public class Path1Meta : PathMeta
{
    public Path1Meta() : base(PathEnum.path0)
    {
        view = (GameObject)Resources.Load("Prefab/path");
    }

    public override PathObj createPathObj()
    {
        return new PathObj(ReturnEnum());
    }
}

public class Meta:Singleton<Meta>
{
    public static int dayTime;
    /// <summary>
    /// 商品的信息
    /// </summary>
    public Dictionary<GoodsEnum, GoodsInf> goodsInfs;
    public Dictionary<BuildingEnum, BuildingMeta> buildingInfs;
    public Dictionary<PathEnum, PathMeta> pathInfs;
    public BuildingMeta getMeta(BuildingEnum buildingEnum)
    {
        return buildingInfs[buildingEnum];
    }

    public GoodsInf getMeta(GoodsEnum goodsEnum)
    {
        return goodsInfs[goodsEnum];
    }
    public PathMeta getMeta(PathEnum pathEnum)
    {
        return pathInfs[pathEnum];
    }
    private Meta()
    {
        goodsInfs = new Dictionary<GoodsEnum, GoodsInf>();//商品信息
        goodsInfs.Add(GoodsEnum.goods1, new GoodsInf(1, GoodsEnum.goods1));
        goodsInfs.Add(GoodsEnum.goods2, new GoodsInf(1, GoodsEnum.goods2));
        buildingInfs = new Dictionary<BuildingEnum, BuildingMeta>();//Meta数据
        buildingInfs[BuildingEnum.building1] = new Building1Meta();
        buildingInfs[BuildingEnum.building0]=new Building0Meta();
        buildingInfs[BuildingEnum.houseBuilding] = new HouseBuildingMeta();
        pathInfs=new Dictionary<PathEnum, PathMeta>();
        pathInfs.Add(PathEnum.path0, new Path1Meta());
    }
}
