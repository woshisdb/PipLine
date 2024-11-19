using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsObj
{
    /// <summary>
    /// 所属建筑
    /// </summary>
    public BuildingObj building;
    /// <summary>
    /// 总的数目
    /// </summary>
    public Int sum;
    /// <summary>
    /// 商品的信息
    /// </summary>
    public GoodsEnum goods;
    public GoodsObj(BuildingObj building, Int sum, GoodsEnum goods)
    {
        this.building = building;
        this.sum = sum;
        this.goods = goods;
    }
}
