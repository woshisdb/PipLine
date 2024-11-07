using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsObj
{
    /// <summary>
    /// 商品属于哪里
    /// </summary>
    public NpcObj belong;
    /// <summary>
    /// 所在的建筑
    /// </summary>
    public BuildingObj building;
    /// <summary>
    /// 总的数目
    /// </summary>
    public Int sum;
    /// <summary>
    /// 价格
    /// </summary>
    public Float cost;
    /// <summary>
    /// 商品的信息
    /// </summary>
    public GoodsEnum goods;
    public GoodsInf getGoodsInf()
    {
        return null;
    }
}
