using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : TobBuildingObj
{
    static GoodsEnum[] ret = new GoodsEnum[] { GoodsEnum.土豆块 };
    public NongChangObj() : base(
        new GenGoodsPipline(Meta.trans[TransEnum.烹饪食物], typeof(ZuoFanJob)),//商品生产
        new GoodsPricePipline()//商品定价
    )
    {
        name = "农场";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.土豆, 100000000);
        resource.Add(obj);
        var retobj = GoodsGen.GetGoodsObj(GoodsEnum.土豆块,0);
        goodsRes.Add(retobj);
    }

    public override GoodsEnum[] getIn()
    {
        return null;
    }

    public override GoodsEnum[] getOut()
    {
        return ret;
    }
}