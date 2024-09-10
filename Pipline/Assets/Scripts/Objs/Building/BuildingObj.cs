using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
/// <summary>
/// һ������滮��
/// </summary>
public interface IAiRequest
{
    /// <summary>
    /// ע��滮��
    /// </summary>
    public void RegisterPlan();
    /// <summary>
    /// ����滮
    /// </summary>
    public void RequestPlan();
    public void CollectData();
}
/// <summary>
/// ����AI
/// </summary>
public class BuildingAI
{
    public BuildingObj BuildingObj;
    public List<IAiRequest> RequestList;
    public BuildingAI(BuildingObj buildingObj)
    {
        BuildingObj = buildingObj;
        RequestList = new List<IAiRequest>();
    }
    public void AddAi(IAiRequest ai)
    {
        RequestList.Add(ai);
    }
}

public class BuildingObj :BaseObj,ISendEvent,ISendCommand
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
    public BuildingAI ai;
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
    /// <summary>
    /// ��������
    /// </summary>
    public void ReceiveRes(RequestGoodsCommand requestGoods)
    {
        ///�ܹ�����
        if (requestGoods.goods.sum <= goodsRes.Get(requestGoods.goods))
        {
            GameArchitect.get.economicSystem.AddSell(requestGoods.cost,this.scene,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.Ec(requestGoods.cost*requestGoods.goods.sum,requestGoods.from.money,requestGoods.to.money);//����
            scene.paths.PushOrder(requestGoods.from.goodsRes,requestGoods.to.resource,requestGoods.goods,requestGoods.wasterTime);
        }
        else
        {
            this.Execute(new UnSatifyGoods(requestGoods));//�����Ʒû������
        }
    }
}


