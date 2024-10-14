using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����AI
/// </summary>
public abstract class BuildingAI
{
    //**********************�ܹ����µ�********************************

    //**********************����
    public BuildingObj building;
    ///// <summary>
    ///// ������(Input)
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
            ///����ϵͳ��ģ��
            return GameArchitect.get.economicSystem.buildingGoodsPrices[building];
        }
    }//����ģ��
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
    //            job.sum = value;//���߹���
    //        }
    //    }//�ṩ��������Ŀ
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
    //    }//һ�����˵�����
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
    /// Ŀ�����������
    /// </summary>
    /// <returns></returns>
    public abstract void MaxInCome();//�������޸Ľ����Ĺ滮,��ʹ���������
}



///// <summary>
///// ������ҵ,��������Ai,����Ϊ:�ӵ�,����.(���������̶�,���������ܵĿ�)
///// </summary>
//public class TobAi : BuildingAI
//{
//    public int maxPipSum;//����������
//    public int historySum;//��ʷ����������
//    public TobAi(BuildingObj buildingObj) : base(buildingObj)
//    {
//    }
//    /// <summary>
//    /// ��ҵ�õ��µĶ���Ҫ���������滮ʱ����
//    /// </summary>
//    public void onAddOrder(Order theOrder)
//    {
//        var ins = building.getIn();//��ȡһϵ�е�����
//        foreach(var item in building.GenGoodsPipline.trans.from.source)
//        {
//            var order=new Order(theOrder.Day,item.Item1,,theOrder.sum*item.Item2);
//        }
//    }
//    /// <summary>
//    /// ��ҵ��Ҫ���
//    /// </summary>
//    public void requestOrder(Order order)
//    {
//        foreach(var building in GameArchitect.get.buildings)//һϵ�еĽ���
//        {
//            ((TobAi)(building.ai)).canGetOrder(theOrder);
//        }
//    }
//    /// <summary>
//    /// ��ȡһ��ʱ����ܲ�������
//    /// </summary>
//    /// <returns></returns>
//    public int GetAllGoodsSum(int time)
//    {
//        //ͨ��Ԥ�������.
//        //����ÿ��������
//        //ÿ���ܹ�
//        // ����ÿ���������������ã����ɹ����趨��
//        int maxDailyProduction = maxPipSum;

//        // ʵ��ÿ����ò��ܣ���������Դ���豸״̬�ȣ��ɸ��ݾ������������
//        int actualDailyProduction = GetAvailableDailyProduction(time);

//        // ���������ܲ���
//        int theoreticalTotalProduction = maxDailyProduction * time;

//        // ʵ���ܲ��ܣ�������ʵ��ÿ��Ŀ��ò���
//        int actualTotalProduction = actualDailyProduction * time;

//        // ������������ܲ��ܺ�ʵ���ܲ����еĽ�Сֵ
//        return Math.Min(theoreticalTotalProduction, actualTotalProduction);
//    }
//    /// <summary>
//    /// ��ȡһ��ʱ��Ĺ����Ѿ��ų��Ķ���
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
//            return (historySum+historySum/10)*time;//1.1������
//    }
//    /// <summary>
//    /// �����Ƿ���
//    /// </summary>
//    /// <param name="order"></param>
//    /// <returns></returns>
//    public Pair<bool,int> canGetOrder(Order order)//����,����,���������ע��
//    {
//        int time = order.Day;
//        int money = order.price;
//        int sum = order.sum;
//        var mincost = GetMinGoodsCost(sum);
//        return new Pair<bool,int>((GetAllGoodsSum(time)-GetOrderGoodsSum(time))>=sum&& mincost<= money,mincost);
//    }
//    /// <summary>
//    /// ��ȡ�ܹ������ͳɱ��۸�
//    /// </summary>
//    /// <returns></returns>
//    public int GetMinGoodsCost(int sum)
//    {
//        var trans = building.GenGoodsPipline.trans;//��Ʒ
//        int sumCost =0;
//        //��Ʒû�н��ڼ۸�
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
//        // ����һ��0��1֮��������
//        double randomProbability = random.NextDouble();

//        // �� x �ĸ��ʷ�����ʷ���ֵ
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