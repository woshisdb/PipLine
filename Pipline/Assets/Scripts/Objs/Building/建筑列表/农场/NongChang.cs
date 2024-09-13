using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base()
    {
        name = "农场";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.土豆, 10000000);
        resource.Add(obj);
        InitJob(new ZuoFanJob(this));
        InitTrans("制作土豆块", false);
    }
}