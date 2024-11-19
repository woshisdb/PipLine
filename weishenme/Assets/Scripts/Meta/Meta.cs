using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsEnum
{
    goods1,
    goods2,
    goods3,
}
public enum ProdEnum
{
    prod1,
}


public enum BuildingEnum
{
    building1,
    building0,
}

public interface MetaI<T>
{
    public T ReturnEnum();
}

public enum GoodsStateEnum
{
    source,//ԭ����������:����Ҫ����ֻ��Ҫ����
    process,//�м��������
    final,//����������Ʒ
}

public class Meta:Singleton<Meta>
{
    public static int dayTime;
    /// <summary>
    /// ��Ʒ����Ϣ
    /// </summary>
    public Dictionary<GoodsEnum, GoodsInf> goodsInfs;
    public Dictionary<BuildingEnum, BuildingMeta> buildingInfs;

    public BuildingMeta getMeta(BuildingEnum buildingEnum)
    {
        return buildingInfs[buildingEnum];
    }

    public GoodsInf getMeta(GoodsEnum goodsEnum)
    {
        return goodsInfs[goodsEnum];
    }

    private Meta()
    {
        goodsInfs = new Dictionary<GoodsEnum, GoodsInf>();//��Ʒ��Ϣ
        goodsInfs.Add(GoodsEnum.goods1, new GoodsInf(1, GoodsEnum.goods1));
        goodsInfs.Add(GoodsEnum.goods2, new GoodsInf(1, GoodsEnum.goods2));
        buildingInfs = new Dictionary<BuildingEnum, BuildingMeta>();//Meta����
        buildingInfs[BuildingEnum.building1] = new Building1Meta();
        buildingInfs[BuildingEnum.building0]=new Building0Meta();
    }
}
