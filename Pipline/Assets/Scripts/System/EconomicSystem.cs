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
    public Pair<List<Path>,int> FindWay(SceneObj start, SceneObj end,Func<Path,int> cost)
    {
        // 初始化
        foreach (var node in paths.Keys)
        {
            distances[node] = int.MaxValue;
            unvisited.Add(node);
        }

        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            var current = unvisited.OrderBy(n => distances[n]).First();

            if (current.Equals(end))
                break;

            unvisited.Remove(current);

            if (paths.ContainsKey(current))
            {
                foreach (var neighborPath in paths[current])
                {
                    var neighbor = neighborPath.to;
                    var tentativeDist = distances[current] + cost(neighborPath);

                    if (tentativeDist < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDist;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        // 回溯路径
        var pathStack = new Stack<SceneObj>();
        var currentPathObj = end;

        while (previous.ContainsKey(currentPathObj))
        {
            pathStack.Push(currentPathObj);
            currentPathObj = previous[currentPathObj];
        }

        if (!currentPathObj.Equals(start)) return null;

        pathStack.Push(start);

        var resultPath = new List<Path>();
        int totalWastTime = 0;
        SceneObj fromNode = pathStack.Pop();

        while (pathStack.Count > 0)
        {
            var toNode = pathStack.Pop();
            var path = paths[fromNode].FirstOrDefault(p => p.to.Equals(toNode));
            if (path != null)
            {
                resultPath.Add(path);
                totalWastTime += path.wastTime;  // 计算总的wastTime
                fromNode = toNode;
            }
        }

        return new Pair<List<Path>, int>(resultPath, totalWastTime);
    }
}


/// <summary>
/// 经济系统
/// </summary>
public class EconomicSystem
{
    /// <summary>
    /// 获取最好的商品
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public List<Tuple<BuildingObj, List<Path>, int>> GetGoods(GoodsObj goods,SceneObj aim)
    {
        var ret = new List<Tuple<BuildingObj, List<Path>, int>>();
        foreach(var item in GameArchitect.get.scenes)
        {
            foreach(var building in item.buildings)
            {
                if(building.goodsManager.goodslist.ContainsKey(goods))
                {
                    var d=GameArchitect.get.pathFinder.FindWay(item, aim, (e) => { return 10; });//每过一个场景需要花费的价格
                    d.Item2 += building.goodsManager.goodslist[goods];//添加商品价格
                    ret.Add(new Tuple<BuildingObj,List<Path>, int>(building, d.Item1,d.Item2));
                }
            }
        }
        return ret;
    }
}
