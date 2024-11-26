using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building0Meta : BuildingMeta
{
    /// <summary>
    /// 获取商品关联信息
    /// </summary>
    /// <returns></returns>
    public override GoodsEnum[] GetGoods()
    {
        return new GoodsEnum[] { GoodsEnum.goods1, GoodsEnum.goods3 };
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
        pipOutput = new Tuple<GoodsEnum, Int>(GoodsEnum.goods1, 1);
        spendTime = 3;
    }
}