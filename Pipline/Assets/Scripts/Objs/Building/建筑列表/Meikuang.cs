using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meikuang : BuildingObj
{
    public Meikuang() : base()
    {
        name = "ú��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.ú��, 100000000);
        resource.Add(obj);
        InitJob(new LianZhiJob(this));
        ///��������
        InitTrans("����ú̿");
    }
}
