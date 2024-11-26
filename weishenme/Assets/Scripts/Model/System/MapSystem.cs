using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 转移商品的对象
/// </summary>
public class TransGoodsItem
{
    public NeedGoods needGoods;
    public SendGoods sendGoods;
    public INeedGoods needer;
    public ISendGoods sender;
    public GoodsEnum goodsEnum;
    public int goodsCount;
    public void DealWithGoods()//处理交易
    {
        needer.GetGoodsProcess(goodsEnum,goodsCount);
    }
}

public class MapSystem:Singleton<MapSystem>
{
    public CircularQueue<List<TransGoodsItem>> cirQueue { get { return SaveSystem.Instance.saveData.cirQueue; } }
    /// <summary>
    /// 一系列的场景
    /// </summary>
    public List<List<SceneObj>> scenes { get { return SaveSystem.Instance.saveData.SceneObjects; } }
    public HashSet<NpcObj> npcs { get { return SaveSystem.Instance.saveData.npcs; } }
    /// <summary>
    /// 在给定花费的最小时间
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public int WasterTime(SceneObj a,SceneObj b)
    {
        return 5*24;
    }

    public int WasterTime(IWorldPosition pos1,IWorldPosition pos2)
    {
        if(pos1.GetSceneObj()==pos2.GetSceneObj())//需要长途运输
        {
            return pos1.GetSceneObj().GetTime(pos1.GetWorldPos(),pos2.GetWorldPos());
        }
        else//不需要长途运输
        {
            return WasterTime(pos1.GetSceneObj(),pos2.GetSceneObj())+1;
        }
    }

    public int WasterMoney(IWorldPosition a, IWorldPosition b)
    {
        return WasterTime(a,b);
    }

    private MapSystem()
    {
    }
    public void Update()
    {
        var cx=cirQueue.Peek();
        foreach(var x in cx)
        {
            x.DealWithGoods();
        }
        cx.Clear();
        cirQueue.Dequeue();
        cirQueue.Enqueue();
    }
}
