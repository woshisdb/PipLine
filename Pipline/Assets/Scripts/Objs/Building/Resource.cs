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

    public Action<GoodsObj> remove;
    public Action<GoodsObj> add;
    
    public void Add(GoodsObj goodsObj)
    {
        GoodsObj val=null;
        goods.TryGetValue(goodsObj, out val);
        if (val == null)
        {
            add(goodsObj);
            goods.Add(goodsObj);
        }
        else
        {
            val.sum += goodsObj.sum;
        }
    }
    public Resource()
    {
        goods = new HashSet<GoodsObj>();
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
    //r1->r2
    public static void Trans(GoodsObj obj, Resource r1, Resource r2)
    {
        r1.Remove(obj);
        r2.Add(obj);
    }
}