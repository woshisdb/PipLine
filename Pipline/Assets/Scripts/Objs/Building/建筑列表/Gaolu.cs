using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaolu : BuildingObj
{
    public Gaolu():base()
    {
        name = "高炉";
        //AddResources(GoodsEnum.铁矿石);
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.铁矿石,0);//寻找铁矿
        var obj1 = GoodsGen.GetGoodsObj(GoodsEnum.煤炭, 0);
        resource.Add(obj);
        resource.Add(obj1);
        InitJob(new LianZhiJob(this), new CarryJob(this));
        ///构建管线
        InitTrans("炼制铁矿");
    }
}
