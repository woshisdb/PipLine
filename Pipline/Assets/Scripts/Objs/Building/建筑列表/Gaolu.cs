using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaolu : BuildingObj
{
    public Gaolu():base()
    {
        name = "��¯";
        AddResources(GoodsEnum.����ʯ);
        var obj = GoodsGen.GetGoodsObj(GoodsEnum.����ʯ);//Ѱ������
        obj.sum = 0;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(LianZhiJob), new LianZhiJob(this)
        );
        var t = GameArchitect.get.objAsset.FindTrans("��������");
        this.pipLineManager.SetTrans(
        new List<TransNode>()
        {
            new TransNode(t,resource,goodsRes)
        });
    }
}
