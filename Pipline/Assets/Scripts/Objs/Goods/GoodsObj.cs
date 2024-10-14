using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FoodInf:GoodsInf
{
    public double style;
}
public class FoodObj:GoodsObj
{

}
/// <summary>
/// 保存这类商品的具体信息
/// </summary>
public class GoodsInf : IEqualityComparer<GoodsInf>
{
    [ReadOnly]
    public int no;
    public string name;
    /// <summary>
    /// 初始价值
    /// </summary>
    public int price;
    /// <summary>
    /// 产品质量
    /// </summary>
    public double quality;
    /// <summary>
    /// 商品序号
    /// </summary>
    [HideInInspector]
    public int goodNo
    {
        get
        {
            if (Enum.TryParse<GoodsEnum>(name, true, out GoodsEnum enumValue))
            {
                // 如果解析成功，返回对应的整数值
                return Convert.ToInt32(enumValue);
            }
            else
            {
                // 如果解析失败，返回 null 或者可以选择返回一个默认值
                return -1;
            }
        }
    }
    public GoodsEnum goodsEnum { get { return (GoodsEnum)Enum.Parse<GoodsEnum>(name); } }
    [SerializeField]
    public int goodSize;
    public bool Equals(GoodsInf x, GoodsInf y)
    {
        return x.goodNo == y.goodNo;
    }
	public override bool Equals(object obj)
	{
        if (obj is GoodsInf other)
        {
            return other.goodNo == other.goodNo;
        }
        else
        {
            return false;
        }
	}
	public override int GetHashCode()
	{
		return this.goodNo.GetHashCode();
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
/// 商品的基类,批量生产
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
    /// 商品的数目
    /// </summary>
    [SerializeField]
    public int sum;
    /// <summary>
    /// 价格
    /// </summary>
    [SerializeField]
    public Money price;
    public bool Equals(GoodsObj x, GoodsObj y)
    {
        return x.goodsInf == y.goodsInf;
    }
	public override bool Equals(object obj)
	{
        if (obj is GoodsObj other)
        {
            return other.goodsInf == goodsInf;
        }
        else
        {
            return false;
        }
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

	public override int GetHashCode()
	{
		return goodsInf.GetHashCode();
    }
}