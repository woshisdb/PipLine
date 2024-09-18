using Accord.Statistics;
using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 对需求的满足情况
/// </summary>
public interface INeed
{
    /// <summary>
    /// 更新满意度
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public double CalNext(double input);
    /// <summary>
    /// 获取满意度,(1,0.5)为较为满足,(0.5,0)为较不满足
    /// </summary>
    /// <returns></returns>
    public double get();
}
/// <summary>
/// 对食物的需求
/// </summary>
public class FoodNeed : INeed
{
    double maxn = 5;
    double count;//饿了多少天
    /// <summary>
    /// 有吃的为1,没吃的为0
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public double CalNext(double input)
    {
        if(input>0.5)
        {
            count = 0;
        }
        else
        {
            count = Math.Min(count+1, maxn);
        }
        return get();
    }

    public double get()
    {
        return 1-(count/maxn);
    }
    public FoodNeed()
    {
        count = 0;
    }
}


/// <summary>
/// 对当下享乐需求
/// </summary>
[Serializable]
public class JoyNeed : INeed
{
    public int size = 30;
    /// <summary>
    /// 刺激值
    /// </summary>
    public LinkedList<double> activ;
    public double[] input;
    /// <summary>
    /// 当前的满足感.(1,0.5)为较为满足,(0.5,0)为较不满足
    /// </summary>
    public double befActiv;
    public double upRate;
    public double downRate;
    public JoyNeed()
    {
        activ = new LinkedList<double>();
        for (int i = 0; i < size; i++)
        {
            activ.AddLast(0.5);
        }
        input = new double[size];
        for (int i = 0; i < size; i++)
        {
            input[i] = i;
        }
        befActiv = 0.5;
    }
    // 训练和预测方法
    public double PredictNextValue()
    {
        int n = activ.Count;
        // 将数据转换为双精度数组
        double[] inputs = input.ToArray();
        double[] outputs = activ.ToArray();

        // 创建并训练简单线性回归模型
        var regression = new SimpleLinearRegression();
        regression.Regress(inputs, outputs);

        // 预测下一个值
        double nextIndex = n; // 下一个索引
        double predictedValue = regression.Transform(nextIndex);

        return predictedValue;
    }
    public double CalNext(double input)
    {
        double val = PredictNextValue();
        Debug.Log(val);
        if (input > val)
            befActiv = 0.5 + upRate * (input - val);
        else
            befActiv = 0.5 + downRate * (input - val);
        befActiv = Math.Clamp(befActiv, 0, 1);
        activ.AddLast(input);
        activ.RemoveFirst();
        return befActiv;
    }

    public double get()
    {
        return befActiv;
    }
}

/// <summary>
/// 对生存安全的满足情况
/// </summary>
public class SafetyNeed : INeed
{
    public double CalNext(double input)//每天能分配的收入
    {
        double val=(input-GameArchitect.get.economicSystem.GetMinLifeCost());
        return val;
    }

    public double get()
    {
        return 1;
    }
}