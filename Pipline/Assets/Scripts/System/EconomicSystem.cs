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

    // �޸ķ�������ΪFindWayResult
    public GoodPath FindWay(SceneObj start, SceneObj end, Func<Path, int> cost)
    {
        foreach (var node in paths.Keys)
        {
            distances[node] = int.MaxValue;
            unvisited.Add(node);
        }

        distances[start] = 0;

        // ��ʱ����ܳɱ�
        int totalTime = 0;
        int totalCost = 0;

        while (unvisited.Count > 0)
        {
            // ��ȡ������С��δ���ʽڵ�
            var current = unvisited.OrderBy(n => distances[n]).First();

            if (current.Equals(end))
                break;

            unvisited.Remove(current);

            // ����ǰ�ڵ���ھ�
            if (paths.ContainsKey(current))
            {
                foreach (var neighborPath in paths[current])
                {
                    var neighbor = neighborPath.to;
                    var tentativeDist = distances[current] + cost(neighborPath);

                    // ����ҵ����̵�·��������¾����ǰ�ýڵ�
                    if (tentativeDist < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDist;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        // ������ʱ����ܳɱ�
        SceneObj currentScene = end;

        while (previous.ContainsKey(currentScene))
        {
            var prevScene = previous[currentScene];
            var pathSegment = paths[prevScene].First(p => p.to.Equals(currentScene));

            // �ۻ���ʱ����ܳɱ�
            totalTime += pathSegment.wastTime;
            totalCost += cost(pathSegment);

            currentScene = prevScene;
        }

        // ���Ŀ��ڵ㲻�ɴ��ʱ����ܳɱ���Ϊ-1���������
        if (distances[end] == int.MaxValue)
        {
            totalCost = -1;
            totalTime = -1;
        }

        // �����ܳɱ�����ʱ��
        return new GoodPath(null,totalCost, totalTime);
    }

}

public class RetEc
{
    public Dictionary<string,object> ret=new Dictionary<string, object>();
}

public interface Ec
{
    public RetEc RetHis();
}

public class GoodPath
{
    public BuildingObj from;
    public int cost;//Ҫ֧������˾��Ǯ
    public int govcost;//Ҫ֧����������Ǯ
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
    public abstract void Update();
}

/// <summary>
/// ����ϵͳ
/// </summary>
public class GoodsHistory: HistoryItem
{
    public int buySum=0;//���������
    public float buyCost=0;//�����ƽ���۸�
    public int sellSum= 0;//����������
    public float sellCost = 0;//������ƽ���۸�
    public override void Update()
    {
        buySum = 0;//���������
        buyCost = 0;//�����ƽ���۸�
        sellSum = 0;//����������
        sellCost = 0;//������ƽ���۸�
    }
}
/// <summary>
/// �������ߵ���ʷ��¼
/// </summary>
public class PipLineHistory:HistoryItem
{
    /// <summary>
    /// �����Ĳ�Ʒ������
    /// </summary>
    public int goodsCreate=0;
    /// <summary>
    /// ������������Ŀ
    /// </summary>
    public int orderSum=0;
    public int carraySum = 0;
    /// <summary>
    /// ���е���Ʒ��Ŀ
    /// </summary>
    public int allGoods=0;

    public override void Update()
    {
        orderSum = 0;
        goodsCreate = 0;
        allGoods = 0;
    }
}
public class EarnHistory:HistoryItem
{
    public int cost;
    public EarnHistory()
    {
        cost = 0;
    }
    public override void Update()
    {
        cost = 0;
    }
}
public class JobHistory:HistoryItem
{
    public int jobSum = 0;
    public int jobCost = 0;
    public override void Update()
    {
        jobSum = 0;
        jobCost = 0;
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

public class BuildingEc: Ec
{
    RetEc retEc;
    public BuildingObj building;
    public Dictionary<GoodsEnum, History<GoodsHistory>> buildingGoodsPrices;//��Ʒ���׼۸�(��)
    public History<PipLineHistory> outputPipline;//�����Ĺ���(��)
    public History<EarnHistory> moneyHis;//�������(��)
    public Dictionary<Job, History<JobHistory>> jobHis;
    public BuildingEc(BuildingObj building)
    {
        buildingGoodsPrices = new Dictionary<GoodsEnum, History<GoodsHistory>>();
        if(building.inputGoods != null)
        foreach (var y in building.inputGoods)
        {
                var value = (GoodsEnum)y;
            buildingGoodsPrices.Add(value, new History<GoodsHistory>(Meta.historySum));//ÿ��ļ۸�
        }
        buildingGoodsPrices.Add(building.outputGoods, new History<GoodsHistory>(Meta.historySum));//ÿ��ļ۸�
        outputPipline = new History<PipLineHistory>(Meta.historySum, e =>
        {
            e.orderSum=building.ai.piplineSum;
            e.allGoods = building.goodsRes.Get(building.outputGoods);
            //e.goodsCreate =;
            if(building.hasTrans)
            e.carraySum = building.ai.carraySum;
        });
        moneyHis=new History<EarnHistory>(Meta.historySum, e =>
        {
            e.cost = building.money.money;
        });
        jobHis = new Dictionary<Job,History<JobHistory>>();
        foreach(var x in building.jobManager.jobs)
        {
            jobHis.Add(x.Value, new History<JobHistory>(Meta.historySum,
                e=>
                {
                    e.jobSum = building.ai.jobSums[x.Value].jobSum;
                    e.jobCost = building.ai.jobSums[x.Value].jobCost;
                }));
        }
        this.building = building;
    }
    public void Update()
    {
        foreach(var x in buildingGoodsPrices)
        {
            x.Value.Update();
        }
        outputPipline.Update();
        moneyHis.Update();
    }

    //public dynamic GetChart()
    //{
    //    return new
    //    {
    //        outputPipline = outputPipline.values
    //        ,moneyHis =moneyHis.values
    //        job
    //    };
    //}

    public RetEc RetHis()
    {
        if (retEc == null)
        {
            retEc = new RetEc();
            retEc.ret.Add("moneyHis", moneyHis.values);
            retEc.ret.Add("outputPipline", outputPipline.values);
            foreach (var x in buildingGoodsPrices)
            {
                retEc.ret.Add(x.Key.ToString(), x.Value.values);
            }
            foreach (var x in jobHis)
            {
                retEc.ret.Add(x.Key.GetType().Name, x.Value.values);
            }
        }
        else
        {
            retEc.ret["moneyHis"]= moneyHis.values;
            retEc.ret["outputPipline"]=outputPipline.values;
            foreach (var x in buildingGoodsPrices)
            {
                retEc.ret[x.Key.ToString()]= x.Value.values;
            }
            foreach (var x in jobHis)
            {
                retEc.ret[x.Key.GetType().Name]= x.Value.values;
            }
        }
        return retEc;
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
    /// ʳ����С�۸�
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
        foodSorter.Sort((x, y) => x.cost.money.CompareTo(y.cost.money));//������Ʒ�۸�
    }
    public void UpdateMinFoodVal()
    {
        int allPerson = scene.npcs.Count;
        
    }
    /// <summary>
    /// �ܻ��ʳ�����ͼ۸�
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
/// Ǯ����ʷ
/// </summary>
public class MoneyHistory : HistoryItem
{
    public int money=0;

    public override void Update()
    {
        money = 0;
    }
}
public class NpcEc:Ec
{
    RetEc retEc;
    public NpcObj npc;
    public History<MoneyHistory> moneyHistory;
    public NpcEc(NpcObj npc)
    {
        moneyHistory = new History<MoneyHistory>(Meta.historySum, e =>
        {
            e.money = npc.money.money;
        });
        this.npc = npc;
    }

    public RetEc RetHis()
    {
        if (retEc == null)
        {
            retEc = new RetEc();
            retEc.ret.Add("moneyHistory", moneyHistory.values);
        }
        else
        {
            retEc.ret["moneyHistory"] = moneyHistory.values;
        }
        return retEc;
    }

    public void Update()
    {
        moneyHistory.Update();
    }
}

public class SceneEc : Ec
{
    public RetEc retEc;
    public SceneObj scene;
    public Dictionary<GoodsEnum, History<GoodsHistory>> goodsPrice;
    public SceneEc(SceneObj scene)
    {
        this.scene = scene;
        goodsPrice = new Dictionary<GoodsEnum, History<GoodsHistory>>();
        foreach (var y in Enum.GetValues(typeof(GoodsEnum)))
        {
            goodsPrice.Add((GoodsEnum)y, new History<GoodsHistory>(Meta.historySum));//ÿ��ļ۸�
        }
    }
    public RetEc RetHis()
    {
        if (retEc == null)
        {
            retEc = new RetEc();
            foreach(var x in goodsPrice)
            {
                retEc.ret.Add(x.Key.ToString(), x.Value.values);
            }
        }
        else
        {
            foreach (var x in goodsPrice)
            {
                retEc.ret.Add(x.Key.ToString(), x.Value.values);
            }
        }
        return retEc;
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
    /// ��������Ϣ.��һ��Ϊ��λ
    /// </summary>
    public Dictionary<SceneObj,SceneEc > sceneGoodsPrices;
    /// <summary>
    /// ������Ʒ�ļ۸�����������Ϣ
    /// </summary>
    public Dictionary<BuildingObj, BuildingEc> buildingGoodsPrices;

    public Dictionary<NpcObj, NpcEc> npcPrices;

    /// <summary>
    /// ��Щ����������������Ʒ
    /// </summary>
    public Dictionary<GoodsEnum,List<BuildingObj>> output2building;
    /// <summary>
    /// ��Щ���������������Ʒ
    /// </summary>
    public Dictionary<GoodsEnum,List<BuildingObj>> input2building;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public List<SortGoods> sortGoods;
    /// <summary>
    /// ��Ʒ��ƽ���۳��۸�
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
            //����Ĭ�ϼ۸�
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
        if (cost==0)//û�г�ʼ�۸�,����ó�ʼ
        {
            return GoodsGen.GetGoodsInf(goods).price;
        }
        else
        {
            return (int) (cost / sum)+1;
        }
    }
    /// <summary>
    /// ��ȡһϵ����Ʒ�м۸���͵���Ʒ
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
                    var d=GameArchitect.get.pathFinder.FindWay(item, aim, (e) => { return 10; });//ÿ��һ��������Ҫ����10�ļ۸������
                    d.cost += GameArchitect.get.shuiWuJu.exchangeCost;
                    d.govcost = d.cost;
                    d.cost += building.goodsManager.goodslist[goods.goodsInf.goodsEnum].money;//�����Ʒ�۸�
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
            this.Update();
            //GameArchitect.get.gameLogic.client.SendRequest();
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
        var t = buildingGoodsPrices[to].buildingGoodsPrices[goods].Find(0);
        t.buyCost = (t.buyCost * t.buySum + cost * sum) / (t.buySum + sum);
        t.buySum += sum;

    }
    //��¼��˾������Ʒ�۸�
    public void AddSellB(int cost, int govCost, BuildingObj from, BuildingObj to, int sum, GoodsEnum goods)
    {
        var t = buildingGoodsPrices[from].buildingGoodsPrices[goods].Find(0);
        t.sellCost = (t.sellCost * t.sellSum + cost * sum) / (t.sellSum + sum);
        t.sellSum += sum;
    }
    public void AddBuy(int cost, int govCost, SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene].goodsPrice[goods].Find(0);
        t.buyCost= (t.buyCost*t.buySum+cost*sum)/(t.buySum+sum);
        t.buySum += sum;
    }
    public void AddSell(int cost, int govCost, SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene].goodsPrice[goods].Find(0);
        t.sellCost = (t.sellCost * t.sellSum + cost * sum) / (t.sellSum + sum);
        t.sellSum += sum;
    }
    /// <summary>
    /// ������Ʒ
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
    /// ���׼۸�
    /// </summary>
    public void Ec(int money,int govCost,Money from,Money to)
    {
        from.money += money-govCost;
        GameArchitect.get.shuiWuJu.money.money += govCost;//���Ż�
        to.money -= money;
    }
    /// <summary>
    /// �����������
    /// </summary>
    /// <returns></returns>
    public double GetMinLifeCost()
    {
        return 1;
    }
    public void Update()
    {
        foreach (var x in GameArchitect.get.scenes)
        {
            foreach(var y in sceneGoodsPrices[x].goodsPrice)
            {
                y.Value.Update();
            }
        }
        foreach(var x in GameArchitect.get.buildings)
        {
            var t=buildingGoodsPrices[x];
            t.Update();
        }
        foreach(var x in npcPrices)
        {
            x.Value.Update();
        }
    }
}
