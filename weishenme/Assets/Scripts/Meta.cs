using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsEnum
{
    goods1,
    goods2,
}
public enum ProdEnum
{
    prod1,
}

public interface MetaI
{

}

public enum GoodsStateEnum
{
    source,//ԭ����������:����Ҫ����ֻ��Ҫ����
    process,//�м��������
    final,//����������Ʒ
}

public class GoodsStateMeta : MetaI
{
    public GoodsStateEnum state=GoodsStateEnum.process;
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
}

public class Meta
{
    public static int dayTime;
    public static Dictionary<GoodsEnum, GoodsInf> goodsInfs;
    public static Dictionary<Type, MetaI> pairs;
    public static MetaI GetMeta<T>()
    {
        return pairs[typeof(T)];
    }

    public Meta()
    {
        goodsInfs = new Dictionary<GoodsEnum, GoodsInf>();//��Ʒ��Ϣ
        goodsInfs.Add(GoodsEnum.goods1, new GoodsInf(1, GoodsEnum.goods1, () => { return new Goods1Obj(); }));
        goodsInfs.Add(GoodsEnum.goods2, new GoodsInf(1, GoodsEnum.goods2, () => { return new Goods2Obj(); }));
        pairs = new Dictionary<Type, MetaI>();//Meta����
        pairs[typeof(GoodsBuildingState)]= new GoodsStateMeta();
    }
}
