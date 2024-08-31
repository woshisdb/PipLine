using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// ����������Ʒ�ľ�����Ϣ
/// </summary>
public class GoodsInf : IEqualityComparer<GoodsInf>
{
    public string name;
    /// <summary>
    /// ��Ʒ���
    /// </summary>
    [SerializeField]
    public int goodNo;
    [SerializeField]
    public int goodSize;
    public bool Equals(GoodsInf x, GoodsInf y)
    {
        return x.goodNo == y.goodNo;
    }

    public int GetHashCode(GoodsInf obj)
    {
        return obj.goodNo.GetHashCode();
    }
    public virtual GoodsObj RetGoods()
    {
        var t=new GoodsObj();
        t.goodsInf = this;
        return t;
    }
}

/// <summary>
/// ��Ʒ�Ļ���,��������
/// </summary>
[Serializable]
public class GoodsObj : BaseObj,IEqualityComparer<GoodsObj>
{
    [ShowInInspector,ValueDropdown("GoodsInf")]
    public GoodsInf goodsInf;
    [ShowInInspector,ReadOnly]
    public string NameGet { 
        get {
            if (goodsInf == null || goodsInf.name == null)
                return "";
            return goodsInf.name;
        } 
    }
    /// <summary>
    /// ��Ʒ����Ŀ
    /// </summary>
    [SerializeField]
    public int sum;
    public bool Equals(GoodsObj x, GoodsObj y)
    {
        return x.goodsInf == y.goodsInf;
    }

    private ValueDropdownList<GoodsInf> GoodsInf
    {
        get
        {
            var asset = Resources.Load<ObjAsset>("NewObjAsset");
            var ret = new ValueDropdownList<GoodsInf>();
            foreach (var item in asset.goodsInfs)
            ret.Add(item.name,item);
            return ret;
        }
    }
    public int GetHashCode(GoodsObj obj)
    {
        return obj.goodsInf.GetHashCode();
    }
    public GoodsInf get()
    {
        return (GoodsInf)goodsInf;
    }
}