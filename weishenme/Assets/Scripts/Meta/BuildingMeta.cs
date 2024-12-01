using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BuildingMeta : MetaI<BuildingEnum>
{
    ///// <summary>
    ///// 商品的Enum
    ///// </summary>
    //public GoodsStateEnum state;
    /// <summary>
    /// 需要的生产力列表
    /// </summary>
    public Tuple<ProdEnum, Int>[] prods;
    /// <summary>
    /// 输入列表
    /// </summary>
    public Tuple<GoodsEnum, Int>[] inputs;
    /// <summary>
    /// 输出列表
    /// </summary>
    public Tuple<GoodsEnum, Int> output;
    /// <summary>
    /// 需要的生产力列表
    /// </summary>
    public Tuple<ProdEnum, Int>[] pipProds;
    /// <summary>
    /// 输入列表
    /// </summary>
    public Tuple<GoodsEnum, Int>[] pipInputs;
    /// <summary>
    /// 输出列表
    /// </summary>
    public Tuple<GoodsEnum, Int> pipOutput;
    public int spendTime;
    public GameObject view;
    /// <summary>
    /// 获取商品关联信息
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] GetGoods();
    public BuildingMeta()
    {
        
    }
    public abstract BuildingObj createBuildingObj();
    public abstract BuildingEnum ReturnEnum();

    public string RetText()
    {
        return ReturnEnum().ToString();
    }
}


