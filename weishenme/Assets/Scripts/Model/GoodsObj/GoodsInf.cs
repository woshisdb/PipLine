using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GoodsInf : IEqualityComparer<GoodsInf>,MetaI<GoodsEnum>
{
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public string name;
    /// <summary>
    /// ��ʼ��ֵ
    /// </summary>
    public double price;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public double quality;
    /// <summary>
    /// ��Ʒ�ı�ǩ
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

    public GoodsEnum ReturnEnum()
    {
        return goodsEnum;
    }
}