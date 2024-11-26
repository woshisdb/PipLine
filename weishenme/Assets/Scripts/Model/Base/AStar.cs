using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarSearcher
{
    // �ڵ���
    private class Node
    {
        public int X, Y;
        public int G; // ����㵽��ǰ�ڵ�Ĵ���
        public int H; // ��ǰ�ڵ㵽Ŀ��ڵ����������
        public int F => G + H; // �ܴ���
        public Node Parent;

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
        // ��д Equals ����
        public override bool Equals(object obj)
        {
            if (obj is Node other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        // ��д GetHashCode ����
        public override int GetHashCode()
        {
            // ���� Name �� Age ����Ψһ��ϣֵ
            return HashCode.Combine(X, Y);
        }
    }

    // �����ӿڷ���
    public static int FindPath(
        Vector2Int start,
        Vector2Int target,
        Func<int, int, bool> IsVis,
        Action<int, int> SetVis,
        List<List<int>> distance)
    {
        // ��ͼ���
        int width = distance.Count;
        int height = distance[0].Count;

        // ��ʼ�����ź͹ر��б�
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        // ���������յ�
        Node startNode = new Node(start.x, start.y);
        Node targetNode = new Node(target.x, target.y);

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // ��ȡFֵ��С�Ľڵ�
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

            //// �������Ŀ���
            //if (currentNode.X == targetNode.X && currentNode.Y == targetNode.Y)
            //{
            //    // ����·������
            //    int totalCost = currentNode.G;
            //    RetracePath(currentNode, SetVis);
            //    return totalCost;
            //}

            // ��ȡ��ǰ�ڵ���������ڽڵ�
            foreach (Node neighbor in GetNeighbors(currentNode, width, height, distance))
            {
                // �������Ŀ���
                int newCostToNeighbor = currentNode.G + distance[neighbor.X][neighbor.Y];
                neighbor.G = newCostToNeighbor;
                if (neighbor.X == targetNode.X && neighbor.Y == targetNode.Y)
                {
                    // ����·������
                    int totalCost = currentNode.G;
                    return totalCost;
                }
                // ������ɷ��ʻ����ڹر��б�����
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

        // û��·��
        return -1;
    }

    // ��ȡ�ھӽڵ�
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

    // ����·�������÷���״̬
    private static void RetracePath(Node endNode, Action<int, int> SetVis)
    {
        Node currentNode = endNode;
        while (currentNode != null)
        {
            SetVis(currentNode.X, currentNode.Y);
            currentNode = currentNode.Parent;
        }
    }

    // �����پ���
    private static int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
    }
}
