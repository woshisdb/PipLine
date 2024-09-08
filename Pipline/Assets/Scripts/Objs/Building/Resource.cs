using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 容纳一系列商品
/// </summary>
public class Resource
{
    /// <summary>
    /// 容纳的物品
    /// </summary>
    public int maxSize;
    /// <summary>
    /// 当前使用的大小
    /// </summary>
    public int nowSize;

    public HashSet<GoodsObj> goods;

    protected Action<GoodsObj> remove;
    protected Action<GoodsObj> add;
    public BuildingObj building;
    
    public void Add(GoodsEnum goodsEnum,int sum)
    {
        //Debug.Log(goodsEnum);
        var goodsObj=GoodsGen.GetGoodsObj(goodsEnum);
        goodsObj.sum = sum;
        GoodsObj val = null;
        goods.TryGetValue(goodsObj, out val);
        if (val == null)
        {
            add(goodsObj);
            goods.Add(goodsObj);
        }
        else
        {
            val.sum +=sum;
        }
    }
    public void Remove(GoodsEnum goodsEnum,int sum)
    {
        var goodsObj = GoodsGen.GetGoodsObj(goodsEnum);
        goodsObj.sum = sum;
        GoodsObj val = null;
        goods.TryGetValue(goodsObj, out val);
        if (val == null)
        {
            //goods.Add(goodsObj);
        }
        else
        {
            val.sum -= sum;
            //if (val.sum == 0)
            //{
            //    remove(goodsObj);
            //    goods.Remove(goodsObj);
            //}
        }
    }

    public void Add(GoodsObj goodsObj)
    {
        GoodsObj val=null;
        goods.TryGetValue(goodsObj, out val);
        if (val == null)
        {
            Debug.Log(goodsObj);
            if(add != null)
            add(goodsObj);
            goods.Add(goodsObj);
        }
        else
        {
            val.sum += goodsObj.sum;
        }
    }
    public Resource(BuildingObj building)
    {
        goods = new HashSet<GoodsObj>();
        this.building = building;
    }
    public void Remove(GoodsObj goodsObj)
    {
        GoodsObj val = null;
        goods.TryGetValue(goodsObj, out val);
        if (val == null)
        {
            //goods.Add(goodsObj);
        }
        else
        {
            val.sum -= goodsObj.sum;
            if (val.sum == 0)
            {
                if(remove != null)
                remove(goodsObj);
                goods.Remove(goodsObj);
            }
        }
    }
    public void AddAddFunc(Action<GoodsObj> action)
    {
        if (add == null)
        {
            add = action;
        }
        else
        {
            var tempAct = add;
            add = (obj) =>
            {
                tempAct(obj);
                action(obj);
            };
        }
    }
    public void AddRemoveFunc(Action<GoodsObj> action)
    {
        if (remove == null)
        {
            remove = action;
        }
        else
        {
            var tempAct = remove;
            remove = (obj) =>
            {
                tempAct(obj);
                action(obj);
            };
        }
    }
    public void Add(Resource resource)
    {
        foreach (var x in resource.goods)
        {
            Add(x);
        }
    }
    public void Remove(Resource resource)
    {
        foreach (var x in resource.goods)
        {
            Remove(x);
        }
    }

    public int Get(GoodsObj obj)
    {
        GoodsObj val = null;
        goods.TryGetValue(obj, out val);
        if (val == null)
        {
            return 0;
            //goods.Add(goodsObj);
        }
        else
        {
            return val.sum;
        }
    }
    public int Get(GoodsEnum goodsEnum)
    {
        GoodsObj val = null;
        var obj=GoodsGen.GetGoodsObj(goodsEnum);
        goods.TryGetValue(obj, out val);
        if (val == null)
        {
            return 0;
            //goods.Add(goodsObj);
        }
        else
        {
            return val.sum;
        }
    }
    public T GetGoods<T>(GoodsEnum goodsEnum)
    where T:GoodsObj
    {
        var obj = GoodsGen.GetGoodsObj(goodsEnum);
        GoodsObj val = null;
        goods.TryGetValue(obj, out val);
        if(val == null)
        {
            val = GoodsGen.GetGoodsObj(goodsEnum);
            val.sum = 0;
            goods.Add(val);
        }
        return (T)val;
    }
    //r1->r2
    public static void Trans(GoodsObj obj, Resource r1, Resource r2)
    {
        r1.Remove(obj);
        r2.Add(obj);
    }
}