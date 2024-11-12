using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStyle
{
    /// <summary>
    /// 一系列的商品
    /// </summary>
    public List<GoodsEnum> goods;
}

///// <summary>
///// 劳动力资源的收入
///// </summary>
//public abstract class Job
//{
//    public NpcState npcState;
//    /// <summary>
//    /// 根据劳动力时间和状态获取生产力列表
//    /// </summary>
//    /// <returns></returns>
//    public abstract Tuple<ProdEnum, Int>[] GenProd(NpcState npcState,int t);
//}
///// <summary>
///// 奴隶阶级,无任何收入强制劳动
///// </summary>
//public class NothingStyle:LifeStyle
//{
//    public float PredicateEarn()
//    {
//        return 0;
//    }
//}
///// <summary>
///// 非奴隶阶级,收入分为劳动力收入与非劳动力收入
///// </summary>
//public class HasThingLifeStyle: LifeStyle
//{
//    /// <summary>
//    /// 根据当前状态预测自己未来和现在的收入,(一般人会根据当前的收入情况预测未来的收入)
//    /// </summary>
//    /// <returns></returns>
//    public float PredicateEarn()
//    {
//        ///需要不工作则只计算非工作收入
//        if(maxWorkTime==0)
//        {

//        }
//        else//需要计算工资收入
//        {

//        }
//        return 1;
//    }
//}