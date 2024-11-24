using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BuildingMeta : MetaI<BuildingEnum>
{
    /// <summary>
    /// ��Ʒ��Enum
    /// </summary>
    public GoodsStateEnum state;
    /// <summary>
    /// ��Ҫ���������б�
    /// </summary>
    public Tuple<ProdEnum, Int>[] prods;
    /// <summary>
    /// �����б�
    /// </summary>
    public Tuple<GoodsEnum, Int>[] inputs;
    /// <summary>
    /// ����б�
    /// </summary>
    public Tuple<GoodsEnum, Int> output;
    /// <summary>
    /// ��Ҫ���������б�
    /// </summary>
    public Tuple<ProdEnum, Int>[] pipProds;
    /// <summary>
    /// �����б�
    /// </summary>
    public Tuple<GoodsEnum, Int>[] pipInputs;
    /// <summary>
    /// ����б�
    /// </summary>
    public Tuple<GoodsEnum, Int> pipOutput;
    public int spendTime;
    public GameObject view;
    /// <summary>
    /// ��ȡ��Ʒ������Ϣ
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] GetGoods();
    public BuildingMeta()
    {
        
    }
    public abstract BuildingObj createBuildingObj();
    public abstract BuildingEnum ReturnEnum();

    public string RetText()
    {
        return ReturnEnum().ToString();
    }
}

public class Building1Meta : BuildingMeta
{
    /// <summary>
    /// ��ȡ��Ʒ������Ϣ
    /// </summary>
    /// <returns></returns>
    public override GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] { GoodsEnum.goods1, GoodsEnum.goods2 };
    }
    public override BuildingEnum ReturnEnum()
    {
        return BuildingEnum.building1;
    }

    public override BuildingObj createBuildingObj()
    {
        var b=new GoodsBuildingObj(ReturnEnum());
        return b;
    }

    public Building1Meta() : base()
    {
        view = (GameObject)Resources.Load("Prefab/building");
        state = GoodsStateEnum.process;
        prods = new Tuple<ProdEnum, Int>[]{
            new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
        };
        inputs = new Tuple<GoodsEnum, Int>[]{
            new Tuple<GoodsEnum, Int>(GoodsEnum.goods1,1)
        };
        output = new Tuple<GoodsEnum, Int>(GoodsEnum.goods2, 1);
        pipProds = new Tuple<ProdEnum, Int>[]{
            new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
        };
        pipInputs = new Tuple<GoodsEnum, Int>[]{
            new Tuple<GoodsEnum, Int>(GoodsEnum.goods1,1)
        };
        spendTime = 3;
        pipOutput = new Tuple<GoodsEnum, Int>(GoodsEnum.goods2, 1);
    }
}

public class Building0Meta : BuildingMeta
{
    /// <summary>
    /// ��ȡ��Ʒ������Ϣ
    /// </summary>
    /// <returns></returns>
    public override GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] { GoodsEnum.goods1,GoodsEnum.goods3};
    }
    public override BuildingEnum ReturnEnum()
    {
        return BuildingEnum.building0;
    }

    public override BuildingObj createBuildingObj()
    {
        var b = new GoodsBuildingObj(ReturnEnum());
        b.now.goodsManager.goods[GoodsEnum.goods3].sum = 10000;
        return b;
    }
    
    public Building0Meta() : base()
    {
        view = (GameObject)Resources.Load("Prefab/building");
        state = GoodsStateEnum.source;
        prods = new Tuple<ProdEnum, Int>[]{
            new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
        };
        inputs = new Tuple<GoodsEnum, Int>[]{
        };
        output = new Tuple<GoodsEnum, Int>(GoodsEnum.goods1, 1);

        pipProds = new Tuple<ProdEnum, Int>[]{
            new Tuple<ProdEnum, Int>(ProdEnum.prod1,1),
        };
        
        pipInputs = new Tuple<GoodsEnum, Int>[]{
            new Tuple<GoodsEnum, Int>(GoodsEnum.goods3,1)
        };
        spendTime = 3;
        pipOutput = new Tuple<GoodsEnum, Int>(GoodsEnum.goods1, 1);

    }
}