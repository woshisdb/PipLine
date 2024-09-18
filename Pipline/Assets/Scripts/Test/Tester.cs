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

    // ���캯������ʼ�����Զ��������
    public CircularArray(int size, Func<T> initializer)
    {
        this.size = size;
        array = new T[size];
        currentIndex = 0;

        // ʹ�� initializer �������������
        for (int i = 0; i < size; i++)
        {
            array[i] = initializer(); // ���磺initializer(i) ���� T ���͵�ֵ
        }
    }

    // ��ȡ��ǰ������Ԫ��
    public T GetCurrent()
    {
        return array[currentIndex];
    }

    // �ƶ�����һ��Ԫ��
    public void Add(T x)
    {
        currentIndex = (currentIndex + 1) % size;
        array[currentIndex] = x;
    }

    // ��ȡָ��������Ԫ�أ���ѭ����ʽ����
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

    // �������������Ԫ��
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
    // ����Ĵ̼��̶��� 0.4�������� 0.6
    //satisfaction.CalNext(0.6);
    chart.RemoveData();
    chart.AddSerie<Line>("lineSat");
    // ��ȡ��һ���ڵ�
    LinkedListNode<double> currentNode = sat.First;
    while (currentNode != null)
    {
        chart.AddData(0, currentNode.Value);
        currentNode = currentNode.Next;  // �ƶ�����һ���ڵ�
    }
    chart.AddSerie<Line>("lineAct");
    currentNode = act.First;
    for (int i = 0; i < maxsum; i++)
    {
        chart.AddData(1, currentNode.Value);
        currentNode = currentNode.Next;  // �ƶ�����һ���ڵ�
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
        currentNode = currentNode.Next;  // �ƶ�����һ���ڵ�
    }
    currentNode = act.First;
    for (int i = 0; i < maxsum; i++)
    {
        chart.UpdateData(1, i, currentNode.Value);
        currentNode = currentNode.Next;  // �ƶ�����һ���ڵ�
    }
}

}
