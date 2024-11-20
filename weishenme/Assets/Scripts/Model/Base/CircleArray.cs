using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularQueue<T>
{
    public T[] _queue;
    public int _head;
    public int _tail;
    public int _size;
    public int _capacity;

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
            throw new InvalidOperationException("��������");
        }
        _tail = (_tail + 1) % _capacity;
        _size++;
    }
    // ���Ԫ�ص�����
    public void Enqueue(T item)
    {
        if (_size == _capacity)
        {
            throw new InvalidOperationException("��������");
        }

        _queue[_tail] = item;
        _tail = (_tail + 1) % _capacity;
        _size++;
    }

    // �Ӷ������Ƴ�Ԫ��
    public T Dequeue()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("����Ϊ��");
        }

        T item = _queue[_head];
        //_queue[_head] = default(T); // �������
        _head = (_head + 1) % _capacity;
        _size--;
        return item;
    }
    public T Peek()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("����Ϊ��");
        }

        return _queue[_head];
    }
    // ���Ҿ������β��n��λ�õ�Ԫ��
    public T FindTail(int n)
    {
        if (n >= _size || n < 0)
        {
            return default(T); // ����null
        }

        int index = (_tail - n - 1 + _capacity) % _capacity;
        return _queue[index];
    }
    // ���Ҿ����ͷ���s��Ԫ��
    public T FindFront(int s)
    {
        if (s >= _size || s < 0)
        {
            throw new InvalidOperationException("�����������з�Χ");
        }

        // �����ͷ��� s ��Ԫ�ص�λ��
        int index = (_head + s) % _capacity;
        return _queue[index];
    }
    public void Clear()
    {
        for(int i = 0; i < _capacity; i++)
        {
            _queue[i] = default(T);
        }
    }
    // ��ȡ���д�С
    public int Size()
    {
        return _size;
    }
}
