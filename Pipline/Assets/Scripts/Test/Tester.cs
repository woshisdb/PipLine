using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XCharts.Runtime;


public class CircularArray<T>
{
    private T[] array;
    private int size;
    private int currentIndex;

    // 构造函数：初始化并自动填充数组
    public CircularArray(int size, Func<T> initializer)
    {
        this.size = size;
        array = new T[size];
        currentIndex = 0;

        // 使用 initializer 函数来填充数组
        for (int i = 0; i < size; i++)
        {
            array[i] = initializer(); // 例如：initializer(i) 返回 T 类型的值
        }
    }

    // 获取当前索引的元素
    public T GetCurrent()
    {
        return array[currentIndex];
    }

    // 移动到下一个元素
    public void Add(T x)
    {
        currentIndex = (currentIndex + 1) % size;
        array[currentIndex] = x;
    }

    // 获取指定索引的元素（以循环方式处理）
    public T this[int index]
    {
        get
        {
            return array[index % size];
        }
        set
        {
            array[index % size] = value;
        }
    }

    // 输出数组中所有元素
    public void PrintArray()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}


public class Tester : MonoBehaviour
//{
//    [ShowInInspector]
//    public Need need;
//    [Button]
//    public void Add(double rate)
//    {
//        need.CalNext(rate);
//    }
//}
{
    public LineChart chart;
    [ShowInInspector]
    public JoyNeed satisfaction;
public LinkedList<double> sat;
public LinkedList<double> act;
int maxsum = 30;
// Start is called before the first frame update
void Start()
{
    sat = new LinkedList<double>();
    act = new LinkedList<double>();
    for (int i = 0; i < maxsum; i++)
    {
        sat.AddLast(0);
        act.AddLast(0);
    }
    for (int i = 0; i < maxsum; i++)
    {
        chart.AddXAxisData("x" + i);
    }
    // 昨天的刺激程度是 0.4，今天是 0.6
    //satisfaction.CalNext(0.6);
    chart.RemoveData();
    chart.AddSerie<Line>("lineSat");
    // 获取第一个节点
    LinkedListNode<double> currentNode = sat.First;
    while (currentNode != null)
    {
        chart.AddData(0, currentNode.Value);
        currentNode = currentNode.Next;  // 移动到下一个节点
    }
    chart.AddSerie<Line>("lineAct");
    currentNode = act.First;
    for (int i = 0; i < maxsum; i++)
    {
        chart.AddData(1, currentNode.Value);
        currentNode = currentNode.Next;  // 移动到下一个节点
    }
}
[Button]
public void Add(float sati)
{
    var level=satisfaction.CalNext(sati);
    sat.AddLast(level);
    sat.RemoveFirst();
    act.AddLast(sati);
    act.RemoveFirst();
    LinkedListNode<double> currentNode = sat.First;
    for (int i = 0; i < maxsum; i++)
    {
        chart.UpdateData(0, i, currentNode.Value);
        currentNode = currentNode.Next;  // 移动到下一个节点
    }
    currentNode = act.First;
    for (int i = 0; i < maxsum; i++)
    {
        chart.UpdateData(1, i, currentNode.Value);
        currentNode = currentNode.Next;  // 移动到下一个节点
    }
}

}
