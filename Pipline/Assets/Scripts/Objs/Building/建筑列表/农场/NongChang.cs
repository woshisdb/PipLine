using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base()
    {
        name = "ũ��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.����, 10000000);
        resource.Add(obj);
        InitJob(new ZuoFanJob(this));
        InitTrans("����������", false);
    }
}