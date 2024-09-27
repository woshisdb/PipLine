using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base("制作土豆块",false, typeof(ZuoFanJob))
    {
        name = "农场";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.土豆, 1000);
        resource.Add(obj);
        var retobj = GoodsGen.GetGoodsObj(GoodsEnum.土豆块,0);
        goodsRes.Add(retobj);
        //InitJob(new ZuoFanJob(this));
        //InitTrans("制作土豆块", false);
    }
}