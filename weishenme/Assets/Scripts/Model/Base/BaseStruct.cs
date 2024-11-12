using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Float
{
    private float value;

    // ���캯��
    public Float(float value=0)
    {
        this.value = value;
    }

    // ��ȡ������ֵ
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    // ��ʽת��: Float -> float
    public static implicit operator float(Float Float)
    {
        return Float.value;
    }

    // ��ʽת��: float -> Float
    public static implicit operator Float(float value)
    {
        return new Float(value);
    }

    // ���������
    public static Float operator +(Float a, Float b)
    {
        return new Float(a.value + b.value);
    }

    public static Float operator -(Float a, Float b)
    {
        return new Float(a.value - b.value);
    }

    public static Float operator *(Float a, Float b)
    {
        return new Float(a.value * b.value);
    }

    public static Float operator /(Float a, Float b)
    {
        return new Float(a.value / b.value);
    }

    // ������ȺͲ��������
    public static bool operator ==(Float a, Float b)
    {
        return a.value == b.value;
    }

    public static bool operator !=(Float a, Float b)
    {
        return a.value != b.value;
    }

    // ��д Equals �� GetHashCode
    public override bool Equals(object obj)
    {
        if (obj is Float)
        {
            return this.value == ((Float)obj).value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }
}

public class Int
{
    private int value;

    // ���캯��
    public Int(int value = 0)
    {
        this.value = value;
    }

    // ��ȡ������ֵ
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    // ��ʽת��: Int -> int
    public static implicit operator int(Int Int)
    {
        return Int.value;
    }

    // ��ʽת��: int -> Int
    public static implicit operator Int(int value)
    {
        return new Int(value);
    }

    // ���������
    public static Int operator +(Int a, Int b)
    {
        return new Int(a.value + b.value);
    }

    public static Int operator -(Int a, Int b)
    {
        return new Int(a.value - b.value);
    }

    public static Int operator *(Int a, Int b)
    {
        return new Int(a.value * b.value);
    }

    public static Int operator /(Int a, Int b)
    {
        return new Int(a.value / b.value);
    }

    // ������ȺͲ��������
    public static bool operator ==(Int a, Int b)
    {
        return a.value == b.value;
    }

    public static bool operator !=(Int a, Int b)
    {
        return a.value != b.value;
    }

    // ��д Equals �� GetHashCode
    public override bool Equals(object obj)
    {
        if (obj is Int)
        {
            return this.value == ((Int)obj).value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }
}


