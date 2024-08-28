using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������Ʒ�ľ�����Ϣ
/// </summary>
public class GoodsInf : IEqualityComparer<GoodsInf>
{
    /// <summary>
    /// ��Ʒ���
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
/// ��Ʒ�Ļ���,��������
/// </summary>
public class GoodsObj : BaseObj,IEqualityComparer<GoodsObj>
{
    public GoodsInf goodsInf;
    /// <summary>
    /// ��Ʒ����Ŀ
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
