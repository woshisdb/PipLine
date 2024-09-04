using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BuildingObj :BaseObj,ISendEvent
{
    public string name="����";
    /// <summary>
    /// ���߹�������������Ʒ
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// ��ˮ���м����
    /// </summary>
    public Resource resource;
    /// <summary>
    /// ��Ʒ��Դ
    /// </summary>
    public Resource goodsRes;
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public GoodsManager goodsManager;
    /// <summary>
    /// �ṩ��������
    /// </summary>
    public Productivity productivity;
    /// <summary>
    /// ����ϵͳ
    /// </summary>
    public JobManager jobManager;

    public BuildingObj()
    {
        pipLineManager = new PipLineManager(this);
        resource = new Resource();
        goodsRes = new Resource();
        goodsManager = new GoodsManager(goodsRes);
        productivity = new Productivity(resource,this);
        jobManager = new JobManager(this);
    }
    public IEnumerator Update()
    {
        for(var i = 0; i <pipLineManager.piplines.Count;i++)
        {
            var line = pipLineManager.piplines[i];
            line.Update();
        }
        return null;
    }
    public IEnumerator LaterUpdate()
    {
        foreach (var x in productivity.productivities)
        {
            productivity.productivities[x.Key] = 0;
        }
        return null;
    }
    public void UpdateEvent()
    {
        this.SendEvent(new UpdateBuildingEvent());
    }
    public virtual string GetContent()
    {
        var sb = GameArchitect.get.sb;
        sb.Clear();
        foreach(var x in resource.goods)
        {
            sb.Append(x.goodsInf.name);
            sb.Append(":");
            sb.Append(x.sum);
            sb.Append("\n");
        }
        return sb.ToString();
    }
}


