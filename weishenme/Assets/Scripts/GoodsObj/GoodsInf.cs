using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GoodsInf : IEqualityComparer<GoodsInf>
{
    [ReadOnly]
    public int no;
    public string name;
    /// <summary>
    /// 初始价值
    /// </summary>
    public double price;
    /// <summary>
    /// 产品质量
    /// </summary>
    public double quality;
    public GoodsEnum goodsEnum;
    public Func<GoodsObj> func;
    public GoodsInf(double quality,GoodsEnum goodsEnum,Func<GoodsObj> func)
    {
        this.name = goodsEnum.ToString();
        this.quality = quality;
        this.goodsEnum = goodsEnum;
        this.func = func;
    }

    public bool Equals(GoodsInf x, GoodsInf y)
    {
        return x.goodsEnum == y.goodsEnum;
    }
    public override bool Equals(object obj)
    {
        if (obj is GoodsInf other)
        {
            return other.goodsEnum == ((GoodsInf)obj).goodsEnum;
        }
        else
        {
            return false;
        }
    }
    public override int GetHashCode()
    {
        return this.goodsEnum.GetHashCode();
    }
    public int GetHashCode(GoodsInf obj)
    {
        return obj.goodsEnum.GetHashCode();
    }
    public GoodsObj RetGoods()
    {
        return func();
    }
}