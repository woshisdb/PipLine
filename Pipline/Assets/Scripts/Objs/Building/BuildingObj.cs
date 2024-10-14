using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Accord.MachineLearning;
using UnityEngine;

public abstract class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
    public string name="����";
    /// <summary>
    /// ���߹�������������Ʒ
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// ԭ���ϵ���Դ
    /// </summary>
    public Resource resource;
    /// <summary>
    /// ��������Ʒ
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
    /// <summary>
    /// ��������
    /// </summary>
    public SceneObj scene;
    /// <summary>
    /// ��Ҫ����Ʒ
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
        foreach (var pip in e)//ÿ�����ߵĸ���
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
                money.Update();//������Ϣ
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
    /// ÿ��һ��ʼ�ĸ���
    /// </summary>
    public void AiUpdate()
    {
        ///ai������Ϣ
        ai.Update();
    }
    public virtual IEnumerator Update()
    {
        var line = pipLineManager.pairs;
        foreach(var x in line)///���¹��ߵ���Ϣ
        {
            x.Update();
        }
        yield return null;
    }
    /// <summary>
    /// ����Ҫ��ԭ����
    /// </summary>
    /// <param name="goodsEnums"></param>
    public void AddResources()
    {
        var source=(CarrySource) pipLineManager.GetTrans("������Ʒ");
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
        var source = (CarrySource)pipLineManager.GetTrans("������Ʒ");
        if (source != null)
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
            sb.Append(":" + goodsManager.Get(x.goodsInf.goodsEnum) + "$");
            sb.Append("\n");
        }
        sb.AppendLine("Ǯ:");
        sb.AppendLine(money.money+"$");
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
            GameArchitect.get.economicSystem.AddSellB(requestGoods.cost,requestGoods.govCost,requestGoods.from,requestGoods.to,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.AddSell(requestGoods.cost,requestGoods.govCost,this.scene,requestGoods.goods.sum,requestGoods.goods.goodsInf.goodsEnum);
            GameArchitect.get.economicSystem.Ec(requestGoods.cost*requestGoods.goods.sum, requestGoods.govCost * requestGoods.goods.sum, requestGoods.from.money,requestGoods.to.money);//����
            requestGoods.to.pipLineManager.carrySource.allOrders[requestGoods.goods.goodsInf.goodsEnum].orderSum += requestGoods.goods.sum;
            scene.paths.PushOrder(requestGoods.from.goodsRes,requestGoods.to.resource,requestGoods.goods,requestGoods.wasterTime);
        }
        else
        {
            this.Execute(new UnSatifyGoods(requestGoods));//�����Ʒû������
        }
    }
    /// <summary>
    /// ��ȡһϵ���������Ʒ
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] getOut();
    /// <summary>
    /// ��ȡһϵ���������Ʒ
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] getIn();
    public GenGoodsPipline GenGoodsPipline;
    public GoodsPricePipline GoodsPricePipline;
    public CarrayPipline CarrayPipline;
}

/// <summary>
/// ToB�Ľ���
/// </summary>
public abstract class TobBuildingObj : BuildingObj
{
    /// <summary>
    /// ���ɹ���,���ɹ���
    /// </summary>
    /// <param name="e"></param>
    /// <param name="job"></param>
    public TobBuildingObj(params Pipline[] piplines) : base(piplines)
    {
        ai = new TobAi(this);
    }
}
/// <summary>
/// ToC�Ľ���,�����̳�,����:����,����
/// </summary>
public abstract class TocBuildingObj : BuildingObj
{
    public TocBuildingObj(Pipline[] piplines) : base(piplines)
    {
        ai = new TocAi(this);
    }
}