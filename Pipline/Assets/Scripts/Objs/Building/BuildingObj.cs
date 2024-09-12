using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
/// <summary>
/// 一个请求规划类
/// </summary>
public interface IAiRequest
{
    /// <summary>
    /// 注册规划器
    /// </summary>
    public void RegisterPlan();
    /// <summary>
    /// 请求规划
    /// </summary>
    public void RequestPlan();
    /// <summary>
    /// 获取Ai的数据
    /// </summary>
    public string CollectData();
}
/// <summary>
/// 建筑AI
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
    public string name="建筑";
    /// <summary>
    /// 管线管理器，管理商品
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// 流水线中间产物
    /// </summary>
    public Resource resource;
    /// <summary>
    /// 商品资源
    /// </summary>
    public Resource goodsRes;
    /// <summary>
    /// 商品管理器
    /// </summary>
    public GoodsManager goodsManager;
    /// <summary>
    /// 工作系统
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
    public void InitTrans(string workname,bool needTrans=true)
    {
        var t = GameArchitect.get.objAsset.FindTrans(workname);
        if (needTrans)
        {
            var v = new CarryTrans();
            v.title = "搬运商品";
            v.maxTrans = 2;
            v.wasterTimes = 1;
            foreach (var sor in t.from.source)
            {
                v.from.source.Add(new Pair<GoodsEnum, int>(sor.Item1, sor.Item2));
                v.to.source.Add(new Pair<GoodsEnum, int>(sor.Item1, sor.Item2));
            }
            pipLineManager.SetTrans(
            new List<TransNode>()
            {
                new TransNode(t,resource,goodsRes),
                new TransNode(v,null,resource)
            });
        }
        else
        {
            pipLineManager.SetTrans(
            new List<TransNode>()
            {
                new TransNode(t,resource,goodsRes)
            });
        }
        AddResources();
    }
    public void InitJob(params Job[] jobs)
    {
        foreach(Job job in jobs)
        {
            this.jobManager.jobs.Add(job.GetType(), job);
        }
    }
    public virtual IEnumerator Update()
    {
		for (var i = 0; i < pipLineManager.piplines.Count; i++)
		{
			var line = pipLineManager.piplines[i];
			line.Update();//更新每一条管线
		}
		return null;
    }
    /// <summary>
    /// 所需要的原材料
    /// </summary>
    /// <param name="goodsEnums"></param>
    public void AddResources()
    {
        var source=(CarrySource) pipLineManager.GetTrans("搬运商品");
        if (source != null)
        {
            List<GoodsEnum> goodsEnums = new List<GoodsEnum>();
            foreach (var node in source.trans.from.source)
            {
                goodsEnums.Add(node.Item1);
            }
            this.goodsEnums = goodsEnums.ToArray();
        }
        //source.UpdateAllResource();//更新所有的资源
    }
    public void FindResourceWay()
    {
        var source = (CarrySource)pipLineManager.GetTrans("搬运商品");
        if (source != null)
        source.UpdateAllResource();//更新所有的资源
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
        sb.AppendLine("原料:");
        foreach(var x in resource.goods)
        {
            sb.Append(x.goodsInf.name);
            sb.Append(":");
            sb.Append(x.sum);
            sb.Append("\n");
        }
        sb.AppendLine("产品:");
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
    /// 接受请求
    /// </summary>
    public void ReceiveRes(RequestGoodsCommand requestGoods)
    {
        ///能够满足
        if (requestGoods.goods.sum <= goodsRes.Get(requestGoods.goods))
        {
            GameArchitect.get.economicSystem.AddSellB(requestGoods.cost,requestGoods.from,requestGoods.to,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.AddSell(requestGoods.cost,this.scene,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.Ec(requestGoods.cost*requestGoods.goods.sum,requestGoods.from.money,requestGoods.to.money);//交易
            scene.paths.PushOrder(requestGoods.from.goodsRes,requestGoods.to.resource,requestGoods.goods,requestGoods.wasterTime);
        }
        else
        {
            this.Execute(new UnSatifyGoods(requestGoods));//这个商品没有满足
        }
    }
}