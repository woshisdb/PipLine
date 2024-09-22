using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 建筑AI
/// </summary>
public class BuildingAI
{
    public BuildingObj building;
    /// <summary>
    /// 总收入(Input)
    /// </summary>
    public Money money 
    { get { return building.money; } }
    /// <summary>
    /// 管线的生产数量
    /// </summary>
    public int piplineSum
    {
        get
        {
            var pip = (PipLineSource)building.GetMainWork();//管线管理
            return pip.maxSum;
        }
        set
        {
            var pip = (PipLineSource)building.GetMainWork();//管线管理
            pip.maxSum=value;
        }
    }
    /// <summary>
    /// 进口商品(Input)
    /// </summary>
    public int carraySum
    {
        get
        {
            var pip = (CarrySource)building.GetCarryWork();//管线管理
            return pip.resourceSum;
        }
        set
        {
            var pip = (CarrySource)building.GetCarryWork();//管线管理
            pip.resourceSum = value;
        }
    }
    /// <summary>
    /// 工作的信息
    /// </summary>
    public Dictionary<Job,JobCal> jobSums;


    public BuildingEc BuildingEc { get {
            ///经济系统的模型
            return GameArchitect.get.economicSystem.buildingGoodsPrices[building];
    } }

    public class JobCal
    {
        public Job job;
        /// <summary>
        /// 提供工作的数目
        /// </summary>
        public int jobSum
        {
            get
            {
                var pip = job.sum;
                return pip;
            }
            set
            {
                job.sum=value;//管线管理
            }
        }
        /// <summary>
        /// 一个工人的收入
        /// </summary>
        public int jobCost
        {
            get
            {
                return job.money;
            }
            set
            {
                job.money = value;
            }
        }
        public JobCal(Job job)
        {
            this.job = job;
        }
    }
    public BuildingAI(BuildingObj buildingObj)
    {
        building = buildingObj;
        jobSums = new Dictionary<Job, JobCal>();
        foreach(var x in buildingObj.jobManager.jobs)
        {
            jobSums.Add(x.Value,new JobCal(x.Value));
        }
    }
    public void Update()
    {
        
    }
}

public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
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
    public GoodsEnum[] inputGoods;
    public GoodsEnum outputGoods;
    public Money money;
    public string mainWorkName;
    public bool hasTrans;
    public Source GetMainWork()
    {
        return pipLineManager.GetTrans(mainWorkName);
    }

    public Source GetCarryWork()
    {
        return pipLineManager.GetTrans("搬运商品");
    }

    public BuildingObj(string mainWorkName,bool needTrans, params Type[] job)
    {
        hasTrans=needTrans;
        pipLineManager = new PipLineManager(this);
        resource = new Resource(this);
        goodsRes = new Resource(this);
        var jobs=new List<Job>();
        foreach(Type type in job)
        {
            var jobI = Activator.CreateInstance(type, this);
            jobs.Add((Job)jobI);
        }
        jobManager = new JobManager(this);
        InitJob(jobs.ToArray());
        InitTrans(mainWorkName,needTrans);
        goodsManager = new GoodsManager(goodsRes);
        money = new Money();
        money.money = 1000000;
        this.Register<EndUpdateEvent>(
            (e) => {
                money.Update();//更新信息
            }
        );
        ai = new BuildingAI(this);
    }
    public void InitTrans(string workname,bool needTrans=true)
    {
        mainWorkName=workname;
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
                new TransNode(t,resource,goodsRes),//生产
                new TransNode(v,null,resource)//搬运
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
        this.outputGoods = t.to.source[0].Item1;
        AddResources();
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
    public void DayUpdate()
    {

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
        this.outputGoods = pipLineManager.GetTrans(this.mainWorkName).trans.to.source[0].Item1;
        if (source != null)
        {
            List<GoodsEnum> goodsEnums = new List<GoodsEnum>();
            foreach (var node in source.trans.from.source)
            {
                goodsEnums.Add(node.Item1);
            }
            this.inputGoods = goodsEnums.ToArray();
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
}