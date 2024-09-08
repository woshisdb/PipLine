using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaolu : BuildingObj
{
    public Gaolu():base()
    {
        name = "¸ßÂ¯";
        AddResources(GoodsEnum.Ìú¿óÊ¯);
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.Ìú¿óÊ¯);//Ñ°ÕÒÌú¿ó
        obj.sum = 0;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(LianZhiJob), new LianZhiJob(this)
        );
        var t = GameArchitect.get.objAsset.FindTrans("Á¶ÖÆÌú¿ó");
        this.pipLineManager.SetTrans(
        new List<TransNode>()
        {
            new TransNode(t,resource,goodsRes)
        });
    }
}
