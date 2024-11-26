using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building1Meta : BuildingMeta
{
    /// <summary>
    /// 获取商品关联信息
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
        var b = new GoodsBuildingObj(ReturnEnum());
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
        pipOutput = new Tuple<GoodsEnum, Int>(GoodsEnum.goods2, 1);
        spendTime = 3;
    }
}