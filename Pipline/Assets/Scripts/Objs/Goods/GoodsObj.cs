using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 保存这类商品的具体信息
/// </summary>
public class GoodsInf : IEqualityComparer<GoodsInf>
{
    /// <summary>
    /// 商品序号
    /// </summary>
    public int goodNo;
    public int goodSize;
    public bool Equals(GoodsInf x, GoodsInf y)
    {
        return x.goodNo == y.goodNo;
    }

    public int GetHashCode(GoodsInf obj)
    {
        return obj.goodNo.GetHashCode();
    }
}

/// <summary>
/// 商品的基类,批量生产
/// </summary>
public class GoodsObj : BaseObj,IEqualityComparer<GoodsObj>
{
    public GoodsInf goodsInf;
    /// <summary>
    /// 商品的数目
    /// </summary>
    public int sum;

    public bool Equals(GoodsObj x, GoodsObj y)
    {
        return x.goodsInf == y.goodsInf;
    }

    public int GetHashCode(GoodsObj obj)
    {
        return obj.goodsInf.GetHashCode();
    }
}
