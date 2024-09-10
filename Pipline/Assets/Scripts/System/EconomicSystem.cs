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


public class GoodPath
{
    public BuildingObj from;
    public int cost;//perGoods
    public int wasterTime; 
    public GoodPath(BuildingObj from, int cost, int wasterTime)
    {
        this.from = from;
        this.cost = cost;
        this.wasterTime = wasterTime;
    }
}

/// <summary>
/// 经济系统
/// </summary>
public class GoodsEC
{
    public int buySum=0;//购买的数量
    public float buyCost=0;//买入的价格
    public int sellSum= 0;//卖出的数量
    public float sellCost = 0;//卖出的价格
}
public class EconomicSystem
{
    public Dictionary<SceneObj, Dictionary<GoodsEnum, CircularQueue<GoodsEC>>> sceneGoodsPrices;
    public Dictionary<BuildingObj,Dictionary<>>
    /// <summary>
    /// 获取最好的商品
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
                if(building.goodsManager.goodslist.ContainsKey(goods))
                {
                    var d=GameArchitect.get.pathFinder.FindWay(item, aim, (e) => { return 10; });//每过一个场景需要花费的价格
                    d.cost += building.goodsManager.goodslist[goods];//添加商品价格
                    d.from = building;
                    ret.Add(d);
                }
            }
        }
        return ret;
    }
    public EconomicSystem()
    {
        sceneGoodsPrices= new Dictionary<SceneObj, Dictionary<GoodsEnum, CircularQueue<GoodsEC>>>();
        foreach(var x in GameArchitect.get.scenes)
        {
            sceneGoodsPrices.Add(x, new Dictionary<GoodsEnum, CircularQueue<GoodsEC>>());
            foreach(var y in Enum.GetValues(typeof(GoodsEnum)))
            {
                sceneGoodsPrices[x].Add((GoodsEnum)y,new CircularQueue<GoodsEC>(20));//每天的价格
                for(int i = 0; i < 20; i++)
                {
                    sceneGoodsPrices[x][(GoodsEnum)y].Enqueue(new GoodsEC());
                }
            }
        }
    }
    public void AddBuy(int cost,SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene][goods].Find(0);
        t.buyCost= (t.buyCost*t.buySum+cost*sum)/(t.buySum+sum);
        t.buySum += sum;


    }
    public void AddSell(int cost,SceneObj scene,int sum,GoodsEnum goods)
    {
        var t = sceneGoodsPrices[scene][goods].Find(0);
        t.sellCost = (t.sellCost * t.sellSum + cost * sum) / (t.sellSum + sum);
        t.sellSum += sum;
    }
    /// <summary>
    /// 交易价格
    /// </summary>
    public void Ec(int money,Money from,Money to)
    {
        from.money += money;
        to.money -= money;
    }
    public void Update()
    {
        foreach (var x in GameArchitect.get.scenes)
        {
            foreach(var y in sceneGoodsPrices[x])
            {
                y.Value.Dequeue();
                y.Value.Enqueue();
                y.Value.Find(0).sellSum = 0;
                y.Value.Find(0).sellCost=0;
                y.Value.Find(0).buySum = 0;
                y.Value.Find(0).buyCost = 0;
            }
        }
    }
}
