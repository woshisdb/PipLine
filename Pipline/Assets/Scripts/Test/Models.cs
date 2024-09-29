//using Accord.Statistics;
//using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Need
{
    /// <summary>
    /// NPC对象
    /// </summary>
    public NpcObj npc;
    public double rate;
    /// <summary>
    /// 获取比例
    /// </summary>
    public abstract double FoodGoodsRate(SortGoods goods);
    /// <summary>
    /// 获取是否开启
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
/// 对生命的需要
/// </summary>
public class LifeNeed : Need
{
    /// <summary>
    /// 剩余的资源
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
/// 对资源的需要
/// </summary>
public class ResourceNeed : Need
{
    public double ExponentialDecayFunction(double t, double s)
    {
        // 计算 k = ln(2) / s
        double k = Math.Log(2) / s;
        // 返回衰减函数值
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
/// 对享乐的需要
/// </summary>
public class AmuseNeed : Need
{
    public double style;
    /// <summary>
    /// 越高对低质量的反应越低,越追求高质量
    /// </summary>
    public double qualityRate;
    public AmuseNeed(NpcObj npc) : base(npc)
    {
    }
    public double Mod1Distance(double a, double b)
    {
        // 计算两者的线性距离和环绕距离
        double linearDistance = Math.Abs(a - b);
        double wrapAroundDistance = 1 - linearDistance;
        // 取较小的距离
        return Math.Min(linearDistance, wrapAroundDistance);
    }

    /// <summary>
    /// 0到1
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
/// 对需求的满足情况
/// </summary>
public abstract class Selector<T>
{
    public NpcObj npc;
    /// <summary>
    /// 获取满意度,(1,0.5)为较为满足,(0.5,0)为较不满足
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
/// 选择的食品
/// </summary>
public class FoodSelector : Selector<SortGoods>
{
    ResourceNeed resourceNeed;
    AmuseNeed amuseNeed;
    public FoodSelector(NpcObj npc) : base(npc)
    {
        
    }

    /// <summary>
    /// 获取食物的满足情况
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
        //获取所有的需求
        return allRate;
    }

    public static SortGoods GetMaxValue(List<SortGoods> numbers, Func<SortGoods, bool> condition,Func<SortGoods,double> val)
    {
        SortGoods maxValue = null; // 使用可空类型以处理未找到的情况

        foreach (var number in numbers)
        {
            // 检查是否满足条件
            if (condition(number))
            {
                // 如果是第一个满足条件的值或当前值大于当前最大值，则更新
                if (maxValue == null || val(number) > val(maxValue))
                {
                    maxValue = number;
                }
            }
        }

        return maxValue; // 返回找到的最大值，如果没有找到则返回 null
    }

    /// <summary>
    /// 选择商品,已经排好序的
    /// </summary>
    public override void Update(List<SortGoods> sortGoods)
    {
        var money=npc.money.money;//总收入
        var ret=GetMaxValue(sortGoods, (goods) => { return goods.cost.money <= money && goods.goodsObj.sum > 0; }, goods => { return Rate(goods); });
        //Debug.Log(ret);
        if(ret==null)//没有商品可以买
        {

        }
        else//有商品则买
        {
            var ec = GameArchitect.get.economicSystem;
            ec.Ec(ret, npc, 1);//商品与经济
        }
    }

    public override void Init()
    {
        resourceNeed = npc.GetNeedManager().resourceNeed;
        amuseNeed = npc.GetNeedManager().amuseNeed;
    }
}


