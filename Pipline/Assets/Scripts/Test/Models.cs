using Accord.Statistics;
using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��������������
/// </summary>
public interface INeed
{
    /// <summary>
    /// ���������
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public double CalNext(double input);
    /// <summary>
    /// ��ȡ�����,(1,0.5)Ϊ��Ϊ����,(0.5,0)Ϊ�ϲ�����
    /// </summary>
    /// <returns></returns>
    public double get();
}
/// <summary>
/// ��ʳ�������
/// </summary>
public class FoodNeed : INeed
{
    double maxn = 5;
    double count;//���˶�����
    /// <summary>
    /// �гԵ�Ϊ1,û�Ե�Ϊ0
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
/// �Ե�����������
/// </summary>
[Serializable]
public class JoyNeed : INeed
{
    public int size = 30;
    /// <summary>
    /// �̼�ֵ
    /// </summary>
    public LinkedList<double> activ;
    public double[] input;
    /// <summary>
    /// ��ǰ�������.(1,0.5)Ϊ��Ϊ����,(0.5,0)Ϊ�ϲ�����
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
    // ѵ����Ԥ�ⷽ��
    public double PredictNextValue()
    {
        int n = activ.Count;
        // ������ת��Ϊ˫��������
        double[] inputs = input.ToArray();
        double[] outputs = activ.ToArray();

        // ������ѵ�������Իع�ģ��
        var regression = new SimpleLinearRegression();
        regression.Regress(inputs, outputs);

        // Ԥ����һ��ֵ
        double nextIndex = n; // ��һ������
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
/// �����氲ȫ���������
/// </summary>
public class SafetyNeed : INeed
{
    public double CalNext(double input)//ÿ���ܷ��������
    {
        double val=(input-GameArchitect.get.economicSystem.GetMinLifeCost());
        return val;
    }

    public double get()
    {
        return 1;
    }
}