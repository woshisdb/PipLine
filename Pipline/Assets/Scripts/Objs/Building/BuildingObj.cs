using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Accord.MachineLearning;
using UnityEngine;

public abstract class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
    public string name="建筑";
    /// <summary>
    /// 管线管理器，管理商品
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// 原材料的资源
    /// </summary>
    public Resource resource;
    /// <summary>
    /// 生产的商品
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
    /// <summary>
    /// 场景对象
    /// </summary>
    public SceneObj scene;
    /// <summary>
    /// 需要的商品
    /// </summary>
    public Money money;

    public T GetPipline<T>()
    {
        return default(T);
    }

    public BuildingObj(Pipline[] e)
    {
        pipLineManager = new PipLineManager(this);
        resource = new Resource(this);
        goodsRes = new Resource(this);
        jobManager = new JobManager(this);
        goodsManager = new GoodsManager(goodsRes);
        money = new Money();
        money.money = 1000000;
        ai = new BuildingAI(this);
        foreach (var pip in e)//每个管线的更新
        {
            if(pip is GenGoodsPipline)
            {
                GenGoodsPipline= (GenGoodsPipline)pip;
            }
            if(pip is GoodsPricePipline)
            {
                GoodsPricePipline = (GoodsPricePipline)pip;
            }
            if (pip is CarrayPipline)
            {
                CarrayPipline = (CarrayPipline)pip;
            }
            pip.Init(this);
        }
        this.Register<EndUpdateEvent>(
            (e) => {
                money.Update();//更新信息
            }
        );
    }
    public void InitJob(params Job[] jobs)
    {
        foreach(Job job in jobs)
        {
            this.jobManager.jobs.Add(job.GetType(), job);
            GameArchitect.get.npcManager.jobContainer.Add(job);
        }
    }
    /// <summary>
    /// 每天一开始的更新
    /// </summary>
    public void AiUpdate()
    {
        ///ai更新信息
        ai.Update();
    }
    public virtual IEnumerator Update()
    {
        var line = pipLineManager.pairs;
        foreach(var x in line)///更新管线的信息
        {
            x.Update();
        }
        yield return null;
    }
    /// <summary>
    /// 所需要的原材料
    /// </summary>
    /// <param name="goodsEnums"></param>
    public void AddResources()
    {
        var source=(CarrySource) pipLineManager.GetTrans("搬运商品");
        this.outputGoods = pipLineManager.GetTrans(this.mainWorkName).trans.to.source[0].Item1;
        goodsRes.Add(outputGoods,0);
        if (source != null)
        {
            List<GoodsEnum> goodsEnums = new List<GoodsEnum>();
            foreach (var node in source.trans.from.source)
            {
                goodsEnums.Add(node.Item1);
            }
            this.inputGoods = goodsEnums.ToArray();
            foreach(var inr in this.inputGoods)
            {
                resource.Add(inr, 0);
            }
            this.Register<NewStepEvent>((e) =>{ FindResourceWay(); });
        }
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
            sb.Append(":" + goodsManager.Get(x.goodsInf.goodsEnum) + "$");
            sb.Append("\n");
        }
        sb.AppendLine("钱:");
        sb.AppendLine(money.money+"$");
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
            GameArchitect.get.economicSystem.AddSellB(requestGoods.cost,requestGoods.govCost,requestGoods.from,requestGoods.to,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.AddSell(requestGoods.cost,requestGoods.govCost,this.scene,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.Ec(requestGoods.cost*requestGoods.goods.sum, requestGoods.govCost * requestGoods.goods.sum, requestGoods.from.money,requestGoods.to.money);//交易
            requestGoods.to.pipLineManager.carrySource.allOrders[requestGoods.goods.goodsInf.goodsEnum].orderSum += requestGoods.goods.sum;
            scene.paths.PushOrder(requestGoods.from.goodsRes,requestGoods.to.resource,requestGoods.goods,requestGoods.wasterTime);
        }
        else
        {
            this.Execute(new UnSatifyGoods(requestGoods));//这个商品没有满足
        }
    }
    /// <summary>
    /// 获取一系列输出的商品
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] getOut();
    /// <summary>
    /// 获取一系列输入的商品
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] getIn();
    public GenGoodsPipline GenGoodsPipline;
    public GoodsPricePipline GoodsPricePipline;
    public CarrayPipline CarrayPipline;
}

/// <summary>
/// ToB的建筑
/// </summary>
public abstract class TobBuildingObj : BuildingObj
{
    /// <summary>
    /// 生成管线,生成工作
    /// </summary>
    /// <param name="e"></param>
    /// <param name="job"></param>
    public TobBuildingObj(params Pipline[] piplines) : base(piplines)
    {
        ai = new TobAi(this);
    }
}
/// <summary>
/// ToC的建筑,各种商场,流程:定价,进口
/// </summary>
public abstract class TocBuildingObj : BuildingObj
{
    public TocBuildingObj(Pipline[] piplines) : base(piplines)
    {
        ai = new TocAi(this);
    }
}