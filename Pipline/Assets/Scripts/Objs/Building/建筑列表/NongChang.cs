using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongChangObj : BuildingObj
{
    public NongChangObj() : base()
    {
        name = "ũ��";
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.������ʯ);
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
