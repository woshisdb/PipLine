using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaolu : BuildingObj
{
    public Gaolu():base()
    {
        name = "��¯";
        //AddResources(GoodsEnum.����ʯ);
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.����ʯ,0);//Ѱ������
        var obj1 = GoodsGen.GetGoodsObj(GoodsEnum.ú̿, 0);
        resource.Add(obj);
        resource.Add(obj1);
        InitJob(new LianZhiJob(this), new CarryJob(this));
        ///��������
        InitTrans("��������");
    }
}
