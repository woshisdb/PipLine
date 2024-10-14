using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 建筑AI
/// </summary>
public abstract class BuildingAI
{
    //**********************能够更新的********************************

    //**********************建筑
    public BuildingObj building;
    ///// <summary>
    ///// 总收入(Input)
    ///// </summary>
    //public Money money
    //{
    //    get { return building.money; }
    //}
    //public Dictionary<Job, JobCal> jobSums;
    public BuildingEc BuildingEc
    {
        get
        {
            ///经济系统的模型
            return GameArchitect.get.economicSystem.buildingGoodsPrices[building];
        }
    }//建筑模型
    //public class JobCal
    //{
    //    public Job job;
    //    public int jobSum
    //    {
    //        get
    //        {
    //            var pip = job.sum;
    //            return pip;
    //        }
    //        set
    //        {
    //            job.sum = value;//管线管理
    //        }
    //    }//提供工作的数目
    //    public int jobCost
    //    {
    //        get
    //        {
    //            return job.money;
    //        }
    //        set
    //        {
    //            job.money = value;
    //        }
    //    }//一个工人的销量
    //    public JobCal(Job job)
    //    {
    //        this.job = job;
    //    }
    //}
    //******************************************************
    public BuildingAI(BuildingObj buildingObj)
    {
        building = buildingObj;
    }
    /// <summary>
    /// 目标是最大化收入
    /// </summary>
    /// <returns></returns>
    public abstract void MaxInCome();//建筑会修改建筑的规划,来使得收入最大
}



///// <summary>
///// 面向企业,即工厂的Ai,流程为:接单,生产.(订单数量固定,生产尽可能的快)
///// </summary>
//public class TobAi : BuildingAI
//{
//    public int maxPipSum;//工厂最大产能
//    public int historySum;//历史工厂最大产出
//    public TobAi(BuildingObj buildingObj) : base(buildingObj)
//    {
//    }
//    /// <summary>
//    /// 企业拿到新的订单要更新制作规划时调用
//    /// </summary>
//    public void onAddOrder(Order theOrder)
//    {
//        var ins = building.getIn();//获取一系列的输入
//        foreach(var item in building.GenGoodsPipline.trans.from.source)
//        {
//            var order=new Order(theOrder.Day,item.Item1,,theOrder.sum*item.Item2);
//        }
//    }
//    /// <summary>
//    /// 企业想要获得
//    /// </summary>
//    public void requestOrder(Order order)
//    {
//        foreach(var building in GameArchitect.get.buildings)//一系列的建筑
//        {
//            ((TobAi)(building.ai)).canGetOrder(theOrder);
//        }
//    }
//    /// <summary>
//    /// 获取一段时间的总产能数量
//    /// </summary>
//    /// <returns></returns>
//    public int GetAllGoodsSum(int time)
//    {
//        //通过预测来解决.
//        //理论每天最大产量
//        //每天能够
//        // 理论每天最大产量（可配置，或由工厂设定）
//        int maxDailyProduction = maxPipSum;

//        // 实际每天可用产能，受限于资源、设备状态等（可根据具体需求调整）
//        int actualDailyProduction = GetAvailableDailyProduction(time);

//        // 计算理论总产能
//        int theoreticalTotalProduction = maxDailyProduction * time;

//        // 实际总产能，受限于实际每天的可用产能
//        int actualTotalProduction = actualDailyProduction * time;

//        // 返回理论最大总产能和实际总产能中的较小值
//        return Math.Min(theoreticalTotalProduction, actualTotalProduction);
//    }
//    /// <summary>
//    /// 获取一段时间的工厂已经放出的订单
//    /// </summary>
//    /// <param name="time"></param>
//    /// <returns></returns>
//    public int GetOrderGoodsSum(int time)
//    {
//        return 10;
//    }
//    public int GetAvailableDailyProduction(int time)
//    {
//        if (UsePast(0.7))
//            return historySum*time;
//        else
//            return (historySum+historySum/10)*time;//1.1的增长
//    }
//    /// <summary>
//    /// 返回是否能
//    /// </summary>
//    /// <param name="order"></param>
//    /// <returns></returns>
//    public Pair<bool,int> canGetOrder(Order order)//单价,总数,如果可以则注册
//    {
//        int time = order.Day;
//        int money = order.price;
//        int sum = order.sum;
//        var mincost = GetMinGoodsCost(sum);
//        return new Pair<bool,int>((GetAllGoodsSum(time)-GetOrderGoodsSum(time))>=sum&& mincost<= money,mincost);
//    }
//    /// <summary>
//    /// 获取能购买的最低成本价格
//    /// </summary>
//    /// <returns></returns>
//    public int GetMinGoodsCost(int sum)
//    {
//        var trans = building.GenGoodsPipline.trans;//商品
//        int sumCost =0;
//        //商品没有进口价格
//        if(building.getIn()!=null)
//        foreach(var ins in trans.from.source)
//        {
//            var minCost=GameArchitect.get.economicSystem.GetMinGoodsCost(building,sum);
//            sumCost+=minCost;
//        }
//        sumCost += 10;
//        return sumCost;
//    }

//    public bool UsePast(double x)
//    {
//        System.Random random = new System.Random();
//        // 生成一个0到1之间的随机数
//        double randomProbability = random.NextDouble();

//        // 以 x 的概率返回历史最大值
//        if (randomProbability < x)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//}