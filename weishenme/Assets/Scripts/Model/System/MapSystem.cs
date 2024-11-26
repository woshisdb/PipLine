using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ת����Ʒ�Ķ���
/// </summary>
public class TransGoodsItem
{
    public NeedGoods needGoods;
    public SendGoods sendGoods;
    public INeedGoods needer;
    public ISendGoods sender;
    public GoodsEnum goodsEnum;
    public int goodsCount;
    public void DealWithGoods()//������
    {
        needer.GetGoodsProcess(goodsEnum,goodsCount);
    }
}

public class MapSystem:Singleton<MapSystem>
{
    public CircularQueue<List<TransGoodsItem>> cirQueue { get { return SaveSystem.Instance.saveData.cirQueue; } }
    /// <summary>
    /// һϵ�еĳ���
    /// </summary>
    public List<List<SceneObj>> scenes { get { return SaveSystem.Instance.saveData.SceneObjects; } }
    public HashSet<NpcObj> npcs { get { return SaveSystem.Instance.saveData.npcs; } }
    /// <summary>
    /// �ڸ������ѵ���Сʱ��
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
        if(pos1.GetSceneObj()==pos2.GetSceneObj())//��Ҫ��;����
        {
            return pos1.GetSceneObj().GetTime(pos1.GetWorldPos(),pos2.GetWorldPos());
        }
        else//����Ҫ��;����
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
