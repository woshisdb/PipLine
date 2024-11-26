using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarSearcher
{
    // 节点类
    private class Node
    {
        public int X, Y;
        public int G; // 从起点到当前节点的代价
        public int H; // 当前节点到目标节点的启发代价
        public int F => G + H; // 总代价
        public Node Parent;

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
        // 重写 Equals 方法
        public override bool Equals(object obj)
        {
            if (obj is Node other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        // 重写 GetHashCode 方法
        public override int GetHashCode()
        {
            // 根据 Name 和 Age 生成唯一哈希值
            return HashCode.Combine(X, Y);
        }
    }

    // 公共接口方法
    public static int FindPath(
        Vector2Int start,
        Vector2Int target,
        Func<int, int, bool> IsVis,
        Action<int, int> SetVis,
        List<List<int>> distance)
    {
        // 地图宽高
        int width = distance.Count;
        int height = distance[0].Count;

        // 初始化开放和关闭列表
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        // 创建起点和终点
        Node startNode = new Node(start.x, start.y);
        Node targetNode = new Node(target.x, target.y);

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // 获取F值最小的节点
            Node currentNode = openList[0];
            foreach (Node node in openList)
            {
                if (node.F < currentNode.F || (node.F == currentNode.F && node.H < currentNode.H))
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //// 如果到达目标点
            //if (currentNode.X == targetNode.X && currentNode.Y == targetNode.Y)
            //{
            //    // 计算路径代价
            //    int totalCost = currentNode.G;
            //    RetracePath(currentNode, SetVis);
            //    return totalCost;
            //}

            // 获取当前节点的所有相邻节点
            foreach (Node neighbor in GetNeighbors(currentNode, width, height, distance))
            {
                // 如果到达目标点
                int newCostToNeighbor = currentNode.G + distance[neighbor.X][neighbor.Y];
                neighbor.G = newCostToNeighbor;
                if (neighbor.X == targetNode.X && neighbor.Y == targetNode.Y)
                {
                    // 计算路径代价
                    int totalCost = currentNode.G;
                    return totalCost;
                }
                // 如果不可访问或已在关闭列表，跳过
                if (closedList.Contains(neighbor) || distance[neighbor.X][neighbor.Y] <= 0)
                    continue;
                if (newCostToNeighbor < neighbor.G || !openList.Contains(neighbor))
                {
                    neighbor.H = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        // 没有路径
        return -1;
    }

    // 获取邻居节点
    private static List<Node> GetNeighbors(Node node, int width, int height, List<List<int>> distance)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = {
            new Vector2Int(-1, 0), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(0, 1)
        };

        foreach (Vector2Int dir in directions)
        {
            int newX = node.X + dir.x;
            int newY = node.Y + dir.y;
            if (newX >= 0 && newX < width && newY >= 0 && newY < height)
            {
                neighbors.Add(new Node(newX, newY));
            }
        }

        return neighbors;
    }

    // 回溯路径并设置访问状态
    private static void RetracePath(Node endNode, Action<int, int> SetVis)
    {
        Node currentNode = endNode;
        while (currentNode != null)
        {
            SetVis(currentNode.X, currentNode.Y);
            currentNode = currentNode.Parent;
        }
    }

    // 曼哈顿距离
    private static int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
    }
}
