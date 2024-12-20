using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GoodsInf : IEqualityComparer<GoodsInf>,MetaI<GoodsEnum>
{
    /// <summary>
    /// 商品的名字
    /// </summary>
    public string name;
    /// <summary>
    /// 初始价值
    /// </summary>
    public double price;
    /// <summary>
    /// 产品质量
    /// </summary>
    public double quality;
    /// <summary>
    /// 商品的标签
    /// </summary>
    public GoodsEnum goodsEnum;
    public GoodsInf(double quality,GoodsEnum goodsEnum)
    {
        this.name = goodsEnum.ToString();
        this.quality = quality;
        this.goodsEnum = goodsEnum;
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

    public string RetText()
    {
        return name;
    }

    public GoodsEnum ReturnEnum()
    {
        return goodsEnum;
    }
}