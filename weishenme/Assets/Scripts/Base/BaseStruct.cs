using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Float
{
    private float value;

    // 构造函数
    public Float(float value=0)
    {
        this.value = value;
    }

    // 获取和设置值
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    // 隐式转换: Float -> float
    public static implicit operator float(Float Float)
    {
        return Float.value;
    }

    // 隐式转换: float -> Float
    public static implicit operator Float(float value)
    {
        return new Float(value);
    }

    // 重载运算符
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

    // 重载相等和不等运算符
    public static bool operator ==(Float a, Float b)
    {
        return a.value == b.value;
    }

    public static bool operator !=(Float a, Float b)
    {
        return a.value != b.value;
    }

    // 重写 Equals 和 GetHashCode
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

    // 构造函数
    public Int(int value = 0)
    {
        this.value = value;
    }

    // 获取和设置值
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    // 隐式转换: Int -> int
    public static implicit operator int(Int Int)
    {
        return Int.value;
    }

    // 隐式转换: int -> Int
    public static implicit operator Int(int value)
    {
        return new Int(value);
    }

    // 重载运算符
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

    // 重载相等和不等运算符
    public static bool operator ==(Int a, Int b)
    {
        return a.value == b.value;
    }

    public static bool operator !=(Int a, Int b)
    {
        return a.value != b.value;
    }

    // 重写 Equals 和 GetHashCode
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


