using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMiningObj : BuildingObj
{
    public IronMiningObj():base()
    { 
        name = "Ìú¿ó";
        var obj=GoodsGen.GetGoodsObj(GoodsEnum.´øÌú¿óÊ¯);
        obj.sum = 10000000;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(CaiKuangJob),new CaiKuangJob(this)
        );
    }
}
