using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PathFinder
{
    private Dictionary<SceneObj, List<Path>> paths;
    Dictionary<SceneObj, int>  distances;
    Dictionary<SceneObj, SceneObj> previous;
    HashSet<SceneObj> unvisited;
    public PathFinder(Dictionary<SceneObj, List<Path>> paths)
    {
        this.paths = paths;
        distances = new Dictionary<SceneObj, int>();
        previous = new Dictionary<SceneObj, SceneObj>();
        unvisited = new HashSet<SceneObj>();
    }

    // 修改返回类型为FindWayResult
    public GoodPath FindWay(SceneObj start, SceneObj end, Func<Path, int> cost)
    {
        foreach (var node in paths.Keys)
        {
            distances[node] = int.MaxValue;
            unvisited.Add(node);
        }

        distances[start] = 0;

        // 总时间和总成本
        int totalTime = 0;
        int totalCost = 0;

        while (unvisited.Count > 0)
        {
            // 获取距离最小的未访问节点
            var current = unvisited.OrderBy(n => distances[n]).First();

            if (current.Equals(end))
                break;

            unvisited.Remove(current);

            // 处理当前节点的邻居
            if (paths.ContainsKey(current))
            {
                foreach (var neighborPath in paths[current])
                {
                    var neighbor = neighborPath.to;
                    var tentativeDist = distances[current] + cost(neighborPath);

                    // 如果找到更短的路径，则更新距离和前置节点
                    if (tentativeDist < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDist;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        // 计算总时间和总成本
        SceneObj currentScene = end;

        while (previous.ContainsKey(currentScene))
        {
            var prevScene = previous[currentScene];
            var pathSegment = paths[prevScene].First(p => p.to.Equals(currentScene));

            // 累积总时间和总成本
            totalTime += pathSegment.wastTime;
            totalCost += cost(pathSegment);

            currentScene = prevScene;
        }

        // 如果目标节点不可达，总时间和总成本设为-1或其他标记
        if (distances[end] == int.MaxValue)
        {
            totalCost = -1;
            totalTime = -1;
        }

        // 返回总成本和总时间
        return new GoodPath(null,totalCost, totalTime);
    }

}

public abstract class Ec
{
    public Ec()
    {
    }
    public abstract void Update();
    public abstract HistoryItem RetNow(int n = 0);
}

public class GoodPath
{
    public BuildingObj from;
    public int cost;//要支付给公司的钱
    public int govcost;//要支付给政府的钱
    public int wasterTime; 
    public GoodPath(BuildingObj from, int cost, int wasterTime)
    {
        this.from = from;
        this.cost = cost;
        this.wasterTime = wasterTime;
    }
}

public abstract class HistoryItem
{
    public int Day { get; set; }
    public virtual void Update()
    {
        Day = GameArchitect.get.timeSystem.GetDay();
    }
}

/// <summary>
/// 经济系统
/// </summary>
public class GoodsHistory: HistoryItem
{
    public int BuySum { get; set; }//购买的数量
    public float BuyCost { get; set; }//买入的平均价格
    public int SellSum { get; set; }//卖出的数量
    public float SellCost { get; set; }//卖出的平均价格
    public override void Update()
    {
        base.Update();
        BuySum = 0;//购买的数量
        BuyCost = 0;//买入的平均价格
        SellSum = 0;//卖出的数量
        SellCost = 0;//卖出的平均价格
    }
}
/// <summary>
/// 生产管线的历史记录
/// </summary>
public class PipLineHistory:HistoryItem
{
    /// <summary>
    /// 创建的产品的数量
    /// </summary>
    public int GoodsCreate { get; set; }
    /// <summary>
    /// 下令生产的数目
    /// </summary>
    public int OrderSum { get; set; }
    public int CarraySum { get; set; }
    /// <summary>
    /// 所有的商品数目
    /// </summary>
    public int AllGoods { get; set; }

    public override void Update()
    {
        base .Update();
        OrderSum = 0;
        GoodsCreate = 0;
        AllGoods = 0;
    }
}
public class EarnHistory:HistoryItem
{
    public int Cost { get; set; }
    public EarnHistory()
    {
        Cost = 0;
    }
    public override void Update()
    {
        base.Update();
        Cost = 0;
    }
}
public class JobHistory:HistoryItem
{
    public int JobSum { get; set; }
    public int JobCost { get; set; }
    public override void Update()
    {
        JobSum = 0;
        JobCost = 0;
    }

}


public class History<T> : CircularQueue<T> where T:HistoryItem,new()
{
    public Action<T> action;
    List<T> list;
    public History(int n,Action<T> action=null) : base(n)
    {
        list = new List<T>();
        this.action = action;
        for(int i=0; i<n; i++)
        {
            var data = new T();
            Enqueue(data);
            list.Add(data);
        }
    }
    public List<T> values { get
        {
            for(int i=0;i< _capacity; i++)
            {
                list[i]= FindFront(i);
            }
            return list; 
        }
    }
    public void Update()
    {
        if(action!=null)
        {
            action(Find(0));
        }
        Dequeue();
        Enqueue();
        Find(0).Update();
    }
}


public class BuildingItem:HistoryItem
{
    public Dictionary<GoodsEnum, GoodsHistory> buildingGoodsPrices;
    public PipLineHistory pipLineHistory;
    public EarnHistory earnHistory;
    public Dictionary<Job, JobHistory> jobHis;
    public BuildingItem()
    {
        buildingGoodsPrices = new Dictionary<GoodsEnum, GoodsHistory>();
        pipLineHistory = new PipLineHistory();
        earnHistory = new EarnHistory();
        jobHis = new Dictionary<Job, JobHistory>();
    }
    public override void Update()
    {
        base.Update();
        foreach(var item in buildingGoodsPrices)
        {
            item.Value.Update();
        }
        pipLineHistory.Update();
        earnHistory.Update();
        foreach(var item in jobHis)
        {
            item.Value.Update();
        }
    }
}


public class BuildingEc: Ec
{
    public BuildingObj building;
    public History<BuildingItem> history;
    public BuildingEc(BuildingObj building):base()
    {
        history = new History<BuildingItem>(Meta.historySum, e =>
        {
            e.earnHistory.Cost = building.money.money;
            foreach (var x in building.jobManager.jobs)
            {
                e.jobHis[x.Value].JobSum = building.ai.jobSums[x.Value].jobSum;
                e.jobHis[x.Value].JobCost = building.ai.jobSums[x.Value].jobCost;
            }
            e.pipLineHistory.OrderSum = building.ai.piplineSum;
            e.pipLineHistory.AllGoods = building.goodsRes.Get(building.outputGoods);
            if (building.hasTrans)
                e.pipLineHistory.CarraySum = building.ai.carraySum;
        });
        for(int i=0;i<Meta.historySum;i++)
        {
            var x=(BuildingItem)RetNow(i);
            foreach(var job in building.jobManager.jobs)
            {
                x.jobHis[job.Value] = new JobHistory();
            }
            if (building.inputGoods != null)
                foreach (var y in building.inputGoods)
                {
                    var value = (GoodsEnum)y;
                    x.buildingGoodsPrices[value]= new GoodsHistory();//每天的价格
                }
            x.buildingGoodsPrices[building.outputGoods] = new GoodsHistory();//每天的价格
        }
        this.building = building;
    }
    public override void Update()
    {
        history.Update();
    }

    public override HistoryItem RetNow(int n = 0)
    {
        return history.Find(n);
    }
}
public class SortGoods
{
    public GoodsObj goodsObj;
    public Money cost;
    public BuildingObj building;
    public Func<int> SortVal;
    public SortGoods(BuildingObj buildingObj)
    {
        this.building = buildingObj;
        GoodsObj goods = building.goodsRes.GetGoods<GoodsObj>(building.outputGoods);
        cost = buildingObj.goodsManager.goodslist[building.outputGoods];
        goodsObj = goods;
        SortVal = () => { return cost.money; };
    }
}


public class SortManager
{
    /// <summary>
    /// 食物最小价格
    /// </summary>
    public double foodminVal;
    public List<SortGoods> foodSorter;
    public SceneObj scene;
    public void Reg(BuildingObj building)
    {
        //GoodsObj goods = building.goodsManager.GetGoods<GoodsObj>(building.outputGoods);
        if(EconomicSystem.IsFood( building.outputGoods))
        {
            foodSorter.Add(new SortGoods(building));
        }
    }
    public void UnReg(BuildingObj building)
    {
        foodSorter.RemoveAll(e => { return e.building == building; });
    }
    public void Update()
    {
        foodSorter.Sort((x, y) => x.cost.money.CompareTo(y.cost.money));//更新商品价格
    }
    public void UpdateMinFoodVal()
    {
        int allPerson = scene.npcs.Count;
        
    }
    /// <summary>
    /// 能获得食物的最低价格
    /// </summary>
    /// <returns></returns>
    public double getMinFoodVal()
    {
        return foodminVal;
    }
    public SortManager(SceneObj scene)
    {
        foodSorter = new List<SortGoods>();
    }
}
/// <summary>
/// 钱的历史
/// </summary>
public class MoneyHistory : HistoryItem
{
    public int Money { get; set; }

    public override void Update()
    {
        base.Update();
        Money = 0;
    }
}

public class NpcItem:HistoryItem
{
    public MoneyHistory moneyHistory;
    public NpcItem()
    {
        moneyHistory = new MoneyHistory();
    }
    public override void Update()
    {
        base.Update();
        moneyHistory.Update();
    }
}

public class NpcEc:Ec
{
    public NpcObj npc;
    public History<NpcItem> history;
    public NpcEc(NpcObj npc):base()
    {
        history = new History<NpcItem>(Meta.historySum, e =>
        {
            e.moneyHistory.Money = npc.money.money;
        });
        this.npc = npc;
        for (int i = 0; i < Meta.historySum; i++)
        {
            RetNow(i);
        }
    }

    public override HistoryItem RetNow(int n=0)
    {
        return history.Find(n);
    }
    public override void Update()
    {
        history.Update();
    }
}

public class SceneItem:HistoryItem
{
    public Dictionary<GoodsEnum, GoodsHistory>  goodsPrice;
    public SceneItem()
    {
        goodsPrice = new Dictionary<GoodsEnum, GoodsHistory>();
        foreach (var y in Enum.GetValues(typeof(GoodsEnum)))
        {
            goodsPrice.Add((GoodsEnum)y, new GoodsHistory());//每天的价格
        }
    }
    public override void Update()
    {
        base.Update();
        foreach (var y in Enum.GetValues(typeof(GoodsEnum)))
        {
            goodsPrice[(GoodsEnum)y].Update();
        }
    }
}

public class SceneEc : Ec
{
    public SceneObj scene;
    public History<SceneItem> history;
    public SceneEc(SceneObj scene):base()
    {
        this.scene = scene;
        history = new History<SceneItem>(Meta.historySum);
    }

    public override HistoryItem RetNow(int n = 0)
    {
        return history.Find(0);
    }

    public override void Update()
    {
        history.Update();
    }
}

public class EconomicSystem:IRegisterEvent
{
    public static bool IsFood(GoodsEnum goodsEnum)
    {
        if (GoodsGen.GetGoodsInf(goodsEnum) is FoodInf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 场景的信息.以一天为单位
    /// </summary>
    public Dictionary<SceneObj,SceneEc > sceneGoodsPrices;
    /// <summary>
    /// 建筑商品的价格生产量等信息
    /// </summary>
    public Dictionary<BuildingObj, BuildingEc> buildingGoodsPrices;

    public Dictionary<NpcObj, NpcEc> npcPrices;

    /// <summary>
    /// 哪些建筑会生产这种商品
    /// </summary>
    public Dictionary<GoodsEnum,List<BuildingObj>> output2building;
    /// <summary>
    /// 哪些建筑会进口这种商品
    /// </summary>
    public Dictionary<GoodsEnum,List<BuildingObj>> input2building;
    /// <summary>
    /// 商品排序
    /// </summary>
    public List<SortGoods> sortGoods;
    /// <summary>
    /// 商品的平均售出价格
    /// </summary>
    /// <returns></returns>
    public int GoodsAveCost(GoodsEnum goods)
    {
        float cost = 0;
        float sum = 0;
        List<BuildingObj> output;
        output2building.TryGetValue(goods, out output);
        if(output == null)
        {
            //返回默认价格
        }
        else
        {
            foreach(var x in output)
            {
                var tempCost=x.goodsManager.FindGoodsCost(goods);
                if(tempCost!=0)
                {
                    cost += tempCost;
                    sum++;
                }
            }
        }
        if (cost==0)//没有初始价格,则调用初始
        {
            return GoodsGen.GetGoodsInf(goods).price;
        }
        else
        {
            return (int) (cost / sum)+1;
        }
    }
    /// <summary>
    /// 获取一系列商品中价格最低的商品
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public List<GoodPath> GetGoods(GoodsObj goods,SceneObj aim)
    {
        var ret = new List<GoodPath>();
        foreach(var item in GameArchitect.get.scenes)
        {
            foreach(var building in item.buildings)
            {
                if(building.goodsManager.goodslist.ContainsKey(goods.goodsInf.goodsEnum))
                {
                    var d=GameArchitect.get.pathFinder.FindWay(item, aim, (e) => { return 10; });//每过一个场景需要花费10的价格给政府
                    d.cost += GameArchitect.get.shuiWuJu.exchangeCost;
                    d.govcost = d.cost;
                    d.cost += building.goodsManager.goodslist[goods.goodsInf.goodsEnum].money;//添加商品价格
                    d.from = building;
                    ret.Add(d);
                }
            }
        }
        return ret;
    }
    public EconomicSystem()
    {
        sceneGoodsPrices = new Dictionary<SceneObj, SceneEc>();
        buildingGoodsPrices = new Dictionary<BuildingObj, BuildingEc>();
        output2building = new Dictionary<GoodsEnum, List<BuildingObj>>();
        input2building = new Dictionary<GoodsEnum, List<BuildingObj>>();
        npcPrices = new Dictionary<NpcObj, NpcEc>();
        foreach (GoodsEnum x in Enum.GetValues(typeof(GoodsEnum)))
        {
            output2building.Add(x, new List<BuildingObj>());
            input2building.Add(x, new List<BuildingObj>());
        }
        foreach (var x in GameArchitect.get.scenes)
        {
            sceneGoodsPrices.Add(x,new SceneEc(x));
            
        }
        foreach (var x in GameArchitect.get.buildings)
        {
            buildingGoodsPrices.Add(x, new BuildingEc(x));
        }
        foreach(var x in GameArchitect.get.npcs)
        {
            npcPrices.Add(x, new NpcEc(x));
        }
        this.Register<PassDayEvent>(
        (e) =>
        {
            GameArchitect.get.gameLogic.StartCoroutine(
            this.Update());
        }
        );
    }
    public void RegBuildingOut(BuildingObj building)
    {
        var x=building.outputGoods;
        output2building[x].Add(building);
    }
    public void RegBuildingIn(BuildingObj building)
    {
        if(building.inputGoods != null)
        foreach(var x in building.inputGoods)
        {
            input2building[x].Add(building);
        }
    }
    public void AddBuyB(int cost,int govCost, BuildingObj from,BuildingObj to, int sum, GoodsEnum goods)
    {
        var t = buildingGoodsPrices[to].history.Find(0).buildingGoodsPrices[goods];
        t.BuyCost = (t.BuyCost * t.BuySum + cost * sum) / (t.BuySum + sum);
        t.BuySum += sum;

    }
    //记录公司售卖商品价格
    public void AddSellB(int cost, int govCost, BuildingObj from, BuildingObj to, int sum, GoodsEnum goods)
    {
        var t = buildingGoodsPrices[from].history.Find(0).buildingGoodsPrices[goods];
        t.SellCost = (t.SellCost * t.SellSum + cost * sum) / (t.SellSum + sum);
        t.SellSum += sum;
    }
    public void AddBuy(int cost, int govCost, SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene].history.Find(0).goodsPrice[goods];
        t.BuyCost= (t.BuyCost*t.BuySum+cost*sum)/(t.BuySum+sum);
        t.BuySum += sum;
    }
    public void AddSell(int cost, int govCost, SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene].history.Find(0).goodsPrice[goods];
        t.SellCost = (t.SellCost * t.SellSum + cost * sum) / (t.SellSum + sum);
        t.SellSum += sum;
    }
    /// <summary>
    /// 购买商品
    /// </summary>
    /// <param name="sort"></param>
    /// <param name="npcObj"></param>
    public void Ec(SortGoods sort,NpcObj npcObj,int sum)
    {
        sort.goodsObj.sum -= sum;
        AddBuy(sort.cost.money, 0, npcObj.belong, 1, sort.goodsObj.goodsInf.goodsEnum);
        AddSell(sort.cost.money, 0, npcObj.belong, 1, sort.goodsObj.goodsInf.goodsEnum);
        AddSellB(sort.cost.money, 0, sort.building,null, 1, sort.goodsObj.goodsInf.goodsEnum);
        Ec(sort.cost.money, 0, npcObj.money, sort.building.money);
    }
    /// <summary>
    /// 交易价格
    /// </summary>
    public void Ec(int money,int govCost,Money from,Money to)
    {
        from.money += money-govCost;
        GameArchitect.get.shuiWuJu.money.money += govCost;//待优化
        to.money -= money;
    }
    /// <summary>
    /// 获得最低生活开销
    /// </summary>
    /// <returns></returns>
    public double GetMinLifeCost()
    {
        return 1;
    }
    public IEnumerator Update()
    {
        ///场景的信息
        foreach (var x in GameArchitect.get.scenes)
        {
            Debug.Log(x);
            var data = sceneGoodsPrices[x];
            yield return GameArchitect.get.client.SendSceneRequest(x.id,(SceneItem)data.RetNow());
            data.Update();
        }
        ///建筑的信息
        foreach(var x in GameArchitect.get.buildings)
        {
            var t=buildingGoodsPrices[x];
            yield return GameArchitect.get.client.SendBuildingRequest(x.id, (BuildingItem)t.RetNow());
            t.Update();
        }
        ///npc的信息
        foreach(var x in npcPrices)
        {
            //yield return GameArchitect.get.client.SendNpcRequest(x.Key.id, (NpcItem)x.Value.RetNow());
            x.Value.Update();
        }
        yield return null;
    }
}
