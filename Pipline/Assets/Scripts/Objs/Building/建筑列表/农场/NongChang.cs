using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base("����������",false, typeof(ZuoFanJob))
    {
        name = "ũ��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.����, 1000);
        resource.Add(obj);
        var retobj = GoodsGen.GetGoodsObj(GoodsEnum.������,0);
        goodsRes.Add(retobj);
        //InitJob(new ZuoFanJob(this));
        //InitTrans("����������", false);
    }
}