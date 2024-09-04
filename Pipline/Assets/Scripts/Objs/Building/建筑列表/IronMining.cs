using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMiningObj : BuildingObj
{
    public IronMiningObj():base()
    { 
        name = "铁矿";
        var obj=GoodsGen.GetGoodsObj(GoodsEnum.带铁矿石);
        obj.sum = 10000000;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(CaiKuangJob),new CaiKuangJob(this)
        );
        var t=GameArchitect.get.objAsset.FindTrans("开采铁矿石");
        Debug.Log(t.title);
        this.pipLineManager.SetTrans(
        new List<TransNode>()
        {
            new TransNode(t,resource,goodsRes)
        });

    }
}
