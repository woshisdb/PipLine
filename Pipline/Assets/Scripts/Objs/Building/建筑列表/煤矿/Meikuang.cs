using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meikuang : BuildingObj
{
    public Meikuang() : base("����ú̿", false, typeof(CaiMeiJob))
    {
        name = "ú��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.ú��, 100000000);
        resource.Add(obj);
        //InitJob(new CaiMeiJob(this));
        /////��������
        //InitTrans("����ú̿",false);
    }
}
