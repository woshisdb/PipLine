using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoodsStateMeta : MetaI
{
    /// <summary>
    /// ��Ʒ��Enum
    /// </summary>
    public GoodsStateEnum state = GoodsStateEnum.process;
    /// <summary>
    /// ��Ҫ���������б�
    /// </summary>
    public Tuple<ProdEnum, Int>[] prods = {
        new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
    };
    /// <summary>
    /// �����б�
    /// </summary>
    public Tuple<GoodsEnum, Int>[] inputs = {
        new Tuple<GoodsEnum, Int>(GoodsEnum.goods1,1)
    };
    /// <summary>
    /// ����б�
    /// </summary>
    public Tuple<GoodsEnum, Int> output = new Tuple<GoodsEnum, Int>(GoodsEnum.goods2, 1);
    public GameObject obj;
    /// <summary>
    /// ��ȡ��Ʒ������Ϣ
    /// </summary>
    /// <returns></returns>
    public GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] { GoodsEnum.goods1, GoodsEnum.goods2 };
    }
}