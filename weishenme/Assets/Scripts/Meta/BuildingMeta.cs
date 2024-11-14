using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingMeta : MetaI<BuildingEnum>
{
    /// <summary>
    /// 商品的Enum
    /// </summary>
    public GoodsStateEnum state = GoodsStateEnum.process;
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
    public GameObject view;
    /// <summary>
    /// 获取商品关联信息
    /// </summary>
    /// <returns></returns>
    public GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] { GoodsEnum.goods1, GoodsEnum.goods2 };
    }
    public BuildingMeta()
    {
        view = (GameObject)Resources.Load("Prefab/building");
    }
    public BuildingObj createBuildingObj()
    {
        return new GoodsBuildingObj();
    }
    public BuildingEnum ReturnEnum()
    {
        return BuildingEnum.building1;
    }
}