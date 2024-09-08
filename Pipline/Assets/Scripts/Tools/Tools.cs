using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 循环队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class CircularQueue<T>
{
    public  T[] _queue;
    private int _head;
    private int _tail;
    private int _size;
    private int _capacity;

    public CircularQueue(int n)
    {
        _queue = new T[n];
        _head = 0;
        _tail = 0;
        _size = 0;
        _capacity = n;
    }
    public void Enqueue()
    {
        if (_size == _capacity)
        {
            throw new InvalidOperationException("队列已满");
        }
        _tail = (_tail + 1) % _capacity;
        _size++;
    }
    // 添加元素到队列
    public void Enqueue(T item)
    {
        if (_size == _capacity)
        {
            throw new InvalidOperationException("队列已满");
        }

        _queue[_tail] = item;
        _tail = (_tail + 1) % _capacity;
        _size++;
    }

    // 从队列中移除元素
    public T Dequeue()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("队列为空");
        }

        T item = _queue[_head];
        //_queue[_head] = default(T); // 清除引用
        _head = (_head + 1) % _capacity;
        _size--;
        return item;
    }
    public T Peek()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("队列为空");
        }

        return _queue[_head];
    }
    // 查找距离队列尾部n个位置的元素
    public T Find(int n)
    {
        if (n >= _size || n < 0)
        {
            return default(T); // 返回null
        }

        int index = (_tail - n - 1 + _capacity) % _capacity;
        return _queue[index];
    }

    // 获取队列大小
    public int Size()
    {
        return _size;
    }
}