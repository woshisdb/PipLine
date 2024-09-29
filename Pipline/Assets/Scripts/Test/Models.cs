//using Accord.Statistics;
//using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Need
{
    /// <summary>
    /// NPC����
    /// </summary>
    public NpcObj npc;
    public double rate;
    /// <summary>
    /// ��ȡ����
    /// </summary>
    public abstract double FoodGoodsRate(SortGoods goods);
    /// <summary>
    /// ��ȡ�Ƿ���
    /// </summary>
    /// <returns></returns>
    public abstract bool IsOpen();
    public Need(NpcObj npc)
    {
        this.npc = npc;
        rate = 1;
    }
    public double GetRate()
    {
        return rate;
    }
}

/// <summary>
/// ����������Ҫ
/// </summary>
public class LifeNeed : Need
{
    /// <summary>
    /// ʣ�����Դ
    /// </summary>
    public double remainPower =7;
    public double maxPowner = 7;
    public LifeNeed(NpcObj npc) : base(npc)
    {
    }

    public override double FoodGoodsRate(SortGoods goods)
    {
        return 1;
    }

    public override bool IsOpen()
    {
        return remainPower > 0;
    }
    public bool isSatAmuse()
    {
        return remainPower > 5;
    }
}

/// <summary>
/// ����Դ����Ҫ
/// </summary>
public class ResourceNeed : Need
{
    public double ExponentialDecayFunction(double t, double s)
    {
        // ���� k = ln(2) / s
        double k = Math.Log(2) / s;
        // ����˥������ֵ
        return Math.Exp(-k * t);
    }
    public double midCost=2;
    public ResourceNeed(NpcObj npc) : base(npc)
    {
    }

    public override double FoodGoodsRate(SortGoods goods)
    {
        return ExponentialDecayFunction(goods.cost.money,GameArchitect.get.economicSystem.GetMinLifeCost()*midCost);
    }

    public override bool IsOpen()
    {
        return npc.GetNeedManager().lifeNeed.IsOpen();
    }
}

/// <summary>
/// �����ֵ���Ҫ
/// </summary>
public class AmuseNeed : Need
{
    public double style;
    /// <summary>
    /// Խ�߶Ե������ķ�ӦԽ��,Խ׷�������
    /// </summary>
    public double qualityRate;
    public AmuseNeed(NpcObj npc) : base(npc)
    {
    }
    public double Mod1Distance(double a, double b)
    {
        // �������ߵ����Ծ���ͻ��ƾ���
        double linearDistance = Math.Abs(a - b);
        double wrapAroundDistance = 1 - linearDistance;
        // ȡ��С�ľ���
        return Math.Min(linearDistance, wrapAroundDistance);
    }

    /// <summary>
    /// 0��1
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public double QualityRate(double a)
    {
        var rate=Math.Pow(a, qualityRate);
        return rate;
    }
    public override double FoodGoodsRate(SortGoods goods)
    {
        FoodInf foodInf = (FoodInf)goods.goodsObj.goodsInf;
        var rate= Mod1Distance(foodInf.style,style) + QualityRate(foodInf.quality);
        return rate;
    }

    public override bool IsOpen()
    {
        return npc.GetNeedManager().lifeNeed.isSatAmuse();
    }
}

/// <summary>
/// ��������������
/// </summary>
public abstract class Selector<T>
{
    public NpcObj npc;
    /// <summary>
    /// ��ȡ�����,(1,0.5)Ϊ��Ϊ����,(0.5,0)Ϊ�ϲ�����
    /// </summary>
    /// <returns></returns>
    public abstract double Rate(SortGoods sortGoods);
    public Selector(NpcObj npc)
    {
        this.npc = npc;
    }
    public abstract void Init();
    public abstract void Update(List<T> values);
}
/// <summary>
/// ѡ���ʳƷ
/// </summary>
public class FoodSelector : Selector<SortGoods>
{
    ResourceNeed resourceNeed;
    AmuseNeed amuseNeed;
    public FoodSelector(NpcObj npc) : base(npc)
    {
        
    }

    /// <summary>
    /// ��ȡʳ����������
    /// </summary>
    /// <param name="goods"></param>
    /// <returns></returns>
    public override double Rate(SortGoods sortGoods)
    {
        var v1= resourceNeed.FoodGoodsRate(sortGoods) * resourceNeed.rate;
        var v2= amuseNeed.FoodGoodsRate(sortGoods) * amuseNeed.rate;
        double allRate = 0;
        if (npc.GetNeedManager().resourceNeed.IsOpen())
        {
            allRate += v1;
        }
        if(npc.GetNeedManager().amuseNeed.IsOpen())
        {
            allRate += v2;
        }
        //��ȡ���е�����
        return allRate;
    }

    public static SortGoods GetMaxValue(List<SortGoods> numbers, Func<SortGoods, bool> condition,Func<SortGoods,double> val)
    {
        SortGoods maxValue = null; // ʹ�ÿɿ������Դ���δ�ҵ������

        foreach (var number in numbers)
        {
            // ����Ƿ���������
            if (condition(number))
            {
                // ����ǵ�һ������������ֵ��ǰֵ���ڵ�ǰ���ֵ�������
                if (maxValue == null || val(number) > val(maxValue))
                {
                    maxValue = number;
                }
            }
        }

        return maxValue; // �����ҵ������ֵ�����û���ҵ��򷵻� null
    }

    /// <summary>
    /// ѡ����Ʒ,�Ѿ��ź����
    /// </summary>
    public override void Update(List<SortGoods> sortGoods)
    {
        var money=npc.money.money;//������
        var ret=GetMaxValue(sortGoods, (goods) => { return goods.cost.money <= money && goods.goodsObj.sum > 0; }, goods => { return Rate(goods); });
        //Debug.Log(ret);
        if(ret==null)//û����Ʒ������
        {

        }
        else//����Ʒ����
        {
            var ec = GameArchitect.get.economicSystem;
            ec.Ec(ret, npc, 1);//��Ʒ�뾭��
        }
    }

    public override void Init()
    {
        resourceNeed = npc.GetNeedManager().resourceNeed;
        amuseNeed = npc.GetNeedManager().amuseNeed;
    }
}


