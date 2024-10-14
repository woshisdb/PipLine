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
/// ����������Ʒ�ľ�����Ϣ
/// </summary>
public class GoodsInf : IEqualityComparer<GoodsInf>
{
    [ReadOnly]
    public int no;
    public string name;
    /// <summary>
    /// ��ʼ��ֵ
    /// </summary>
    public int price;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public double quality;
    /// <summary>
    /// ��Ʒ���
    /// </summary>
    [HideInInspector]
    public int goodNo
    {
        get
        {
            if (Enum.TryParse<GoodsEnum>(name, true, out GoodsEnum enumValue))
            {
                // ��������ɹ������ض�Ӧ������ֵ
                return Convert.ToInt32(enumValue);
            }
            else
            {
                // �������ʧ�ܣ����� null ���߿���ѡ�񷵻�һ��Ĭ��ֵ
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
    /// <summary>
    /// �۸�
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