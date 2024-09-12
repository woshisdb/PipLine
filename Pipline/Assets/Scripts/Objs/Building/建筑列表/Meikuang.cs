using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meikuang : BuildingObj
{
    public Meikuang() : base()
    {
        name = "煤场";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.煤矿, 100000000);
        resource.Add(obj);
        InitJob(new LianZhiJob(this));
        ///构建管线
        InitTrans("开采煤炭");
    }
}
