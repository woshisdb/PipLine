using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base()
    {
        name = "农场";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.带铁矿石);
        obj.sum = 10000000;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(CaiKuangJob), new CaiKuangJob(this)
        );
        this.jobManager.jobs.Add(
            typeof(CarryJob), new CarryJob(this)
        );
    }
}
