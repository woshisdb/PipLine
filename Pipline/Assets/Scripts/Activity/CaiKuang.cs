using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiKuangBeginAct : Act<CaiKuangJob,CaiKuangJobInstance> {
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.Add(GoodsEnum.返,2);
        building.resource.GetGoods<HandObj>(GoodsEnum.返).UseTool(npc,2,wasterTime);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiKuangBeginAct(Job job,int wasterTime):base(job,null)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiKuangEndAct : Act<CaiKuangJob, CaiKuangJobInstance>
{
    public int wasterTime;
    public override IEnumerator Run()
    {
        building.resource.GetGoods<HandObj>(GoodsEnum.返).ReleaseTool(npc, 2, wasterTime);
        building.resource.Remove(GoodsEnum.返,2);
        yield return null;
    }

    public override int WasterTime()
    {
        return wasterTime;
    }
    public CaiKuangEndAct(Job job, int wasterTime) : base(job, null)
    {
        this.wasterTime = wasterTime;
    }
}

public class CaiKuangJobInstance : JobInstance
{
    public CaiKuangJobInstance(Job job, NpcObj npc) : base(job, npc)
    {
    }
}

public class CaiKuangJob : Job
{
    public CaiKuangJob(BuildingObj building) : base((e,f) => { return new CaiKuangJobInstance(e, f); },building)
    {
        dayWorks = new List<DayWork>();
        var workday=new DayWork();
        workday.preAct= new CaiKuangBeginAct(this,8);
        workday.endAct = new CaiKuangEndAct(this,8);
        dayWorks.Add(workday);
        sum = 0;
    }
}
