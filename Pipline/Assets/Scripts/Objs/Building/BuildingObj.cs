using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AI
{

}

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
    /// ����ϵͳ
    /// </summary>
    public JobManager jobManager;
    public AI ai;
    public SceneObj scene;
    public GoodsEnum[] goodsEnums;
    public Money money;
    public BuildingObj()
    {
        pipLineManager = new PipLineManager(this);
        resource = new Resource(this);
        goodsRes = new Resource(this);
        goodsManager = new GoodsManager(goodsRes);
        jobManager = new JobManager(this);
    }
    public virtual IEnumerator Update()
    {
		for (var i = 0; i < pipLineManager.piplines.Count; i++)
		{
			var line = pipLineManager.piplines[i];
			line.Update();//����ÿһ������
		}
		return null;
    }
    /// <summary>
    /// ����Ҫ��ԭ����
    /// </summary>
    /// <param name="goodsEnums"></param>
    public void AddResources(params GoodsEnum[] goodsEnums)
    {
        this.goodsEnums = goodsEnums;
        var source=(CarrySource) pipLineManager.GetTrans("������Ʒ");
        source.UpdateAllResource();//�������е���Դ
    }
    public IEnumerator LaterUpdate()
    {
        UpdateEvent();
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
        sb.AppendLine("ԭ��:");
        foreach(var x in resource.goods)
        {
            sb.Append(x.goodsInf.name);
            sb.Append(":");
            sb.Append(x.sum);
            sb.Append("\n");
        }
        sb.AppendLine("��Ʒ:");
        foreach (var x in goodsRes.goods)
        {
            sb.Append(x.goodsInf.name);
            sb.Append(":");
            sb.Append(x.sum);
            sb.Append("\n");
        }

        return sb.ToString();
    }
}


