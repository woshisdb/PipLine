using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : TobBuildingObj
{
    static GoodsEnum[] ret = new GoodsEnum[] { GoodsEnum.������ };
    public NongChangObj() : base(
        new GenGoodsPipline(Meta.trans[TransEnum.���ʳ��], typeof(ZuoFanJob)),//��Ʒ����
        new GoodsPricePipline()//��Ʒ����
    )
    {
        name = "ũ��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.����, 100000000);
        resource.Add(obj);
        var retobj = GoodsGen.GetGoodsObj(GoodsEnum.������,0);
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