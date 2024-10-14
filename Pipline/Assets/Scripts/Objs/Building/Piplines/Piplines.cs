using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ�Ĺ���
/// </summary>
public class GenGoodsPipline : Pipline
{
    public int maxSum = 99999999;//ÿ�غ�������Դ����Ŀ
    public List<double> piplineList;
    public Trans trans;//��Ʒ���ת�ƹ�ϵ
    public Resource from;//ԭ���ϲֿ�
    public Resource to;//��Ʒ�Ĳֿ�
    public Productivity productivity;
    public int wasterTimes;
    public Func<BuildingObj, Job> jobcreate;
    public Type jobType;
    public Job job;
    public override void Update()
    {
        double prodSum = maxSum;//����������������Ŀ
        // ������С������������
        foreach (var edge in trans.edge.tras)
        {
            prodSum = Math.Min(prodSum, ((double)productivity[edge.Key]) / ((double)edge.Value));
        }
        int sourceMax = int.MaxValue;//��������Ʒ����Ŀ
        foreach (var source in trans.from.source)
        {
            sourceMax = Math.Min(sourceMax, from.Get(source.Item1) / source.Item2);
        }
        // �Ƴ����������е�ԭ����
        foreach (var source in trans.from.source)
        {
            int sumValue = source.Item2 * sourceMax;
            from.Remove(source.Item1, sumValue);
        }

        // ����������
        foreach (var edge in trans.edge.tras)
        {
            productivity[edge.Key] -= (int)(prodSum * edge.Value);
        }
        piplineList[0] = sourceMax;//ʣ���Ʒ������
                                   // ѭ������ÿ������׶�
        for (int i = piplineList.Count - 1; i >= 1; i--)
        {
            var val = piplineList[i];
            var temp = Math.Min(prodSum, val);
            prodSum -= temp;
            piplineList[i] -= temp;
            var createSum = ((int)Math.Ceiling(val) - (int)Math.Ceiling(piplineList[i]));
            if (i == piplineList.Count - 1)//���һ��
            {
                to.Add(trans.to.Item1, createSum * trans.to.Item2);
            }
            else
            {
                piplineList[i + 1] += createSum;
            }
        }
    }
    public override void Init(BuildingObj building)//��ʼ������
    {
        base.Init(building);
        wasterTimes = trans.wasterTimes;
        piplineList = new List<double>(wasterTimes);
        from = building.resource;
        to = building.goodsRes;
        productivity = new Productivity(building);
        job = (Job)Activator.CreateInstance(jobType, building);
        building.InitJob(job);
    }
    public GenGoodsPipline(Trans trans,Type job)
    {
        jobType = job;
    }
}

///// <summary>
///// ������Ʒ�Ĺ���
///// </summary>
//public class CarrayPipline : Pipline
//{
//    /// <summary>
//    /// ��ѡ�������·��
//    /// </summary>
//    public Dictionary<GoodsEnum, GoodPath> goods2Path;
//    /// <summary>
//    /// ������Դ����Ŀ
//    /// </summary>
//    public Dictionary<GoodsEnum, ALLOrder> allOrders;
//    /// <summary>
//    /// ӵ�ж��ٵ���Ʒ
//    /// </summary>
//    public Dictionary<GoodsEnum, int> inputsSum;
//    /// <summary>
//    /// ��Դ����Ŀ
//    /// </summary>
//    public int resourceSum = 1;
//    /// <summary>
//    /// �ȱ�������Դ
//    /// </summary>
//	public override void Update()
//    {
//        foreach (var goodsPath in goods2Path)
//        {
//            int varSource = Math.Max(resourceSum * inputsSum[goodsPath.Key] - allOrders[goodsPath.Key].get(), 0);//��Ҫ���������
//            if (varSource > 0)
//                RequestRes(goodsPath.Key, goodsPath.Value.from, varSource, goodsPath.Value.cost, goodsPath.Value.govcost, goodsPath.Value.wasterTime);//ȥ������Դ
//        }
//    }
//    /// <summary>
//    /// ��Ʒ���������Ŀ,��ÿһ���ļ۸�
//    /// </summary>
//    /// <param name="goods"></param>
//    /// <param name="sum"></param>
//    public void RequestRes(GoodsEnum goodsEnum, BuildingObj building, int sum, int cost, int govCost, int time)
//    {
//        var maxNum = Math.Min(sum, belong.money.money / cost);//�������Դ��Ŀ
//        foreach (var tran in trans.edge.tras)
//        {
//            var remain = productivity[tran.Key] / tran.Value;
//            maxNum = Mathf.Min(maxNum, remain);
//        }
//        var goods = GoodsGen.GetGoodsObj(goodsEnum);
//        goods.sum = maxNum;
//        this.Execute(new RequestGoodsCommand(building, this.belong, goods, time, cost, govCost));
//    }
//    public CarrayPipline(BuildingObj building, Resource from, Resource to, Trans trans, Productivity productivity)
//    {
//        this.belong = building;
//        this.from = from;
//        this.to = to;
//        this.trans = trans;
//        this.productivity = productivity;
//        goods2Path = new Dictionary<GoodsEnum, GoodPath>();
//        inputsSum = new Dictionary<GoodsEnum, int>();//һϵ�е���Ʒ
//        allOrders = new Dictionary<GoodsEnum, ALLOrder>();
//        foreach (var tran in trans.from.source)
//        {
//            inputsSum.Add(tran.Item1, tran.Item2);
//            allOrders.Add(tran.Item1, new ALLOrder(belong.resource, tran.Item1));
//        }
//    }
//    /// <summary>
//    /// ����һϵ�еĽ�����Դ,Ȼ����»����ɱ�
//    /// </summary>
//    /// <param name="goodsEnum"></param>
//    public void UpdateAllResource()
//    {
//        goods2Path.Clear();
//        foreach (var goods in belong.inputGoods)//ѡ������·��
//        {
//            UpdateResource(goods);
//        }
//    }
//    public void UpdateResource(GoodsEnum goodsEnum)
//    {
//        var obj = GoodsGen.GetGoodsObj(goodsEnum);
//        var res = GameArchitect.get.economicSystem.GetGoods(obj, belong.scene);//���ԭ���е���С�Ļ�ȡ�ɱ�
//        var result = res.FindMinElement(e => { return e.cost; });//������гɱ�����С���Ǹ�
//        if (result != null)
//        {
//            result.wasterTime = Math.Max(result.wasterTime, 1);
//            goods2Path.Add(goodsEnum, result);//�������Դ�ĺϼ�
//        }
//    }
//}


public enum OrderState
{
    hasFinish,//�����Ѿ�����
    broken,//������������
    inProcess,//���ڽ���
}

/// <summary>
/// ��������
/// </summary>
public class Order
{
    public int id;
    /// <summary>
    /// �����ʱ��
    /// </summary>
    public int Day;
    /// <summary>
    /// ��Ʒ����Ϣ
    /// </summary>
    public GoodsEnum goods;
    public int price;
    public int sum;
    public int remainsum;
    public int remainprice;
    public OrderState state;
    public Order parent;//������
    public Dictionary<GoodsEnum, Order> orders;//һϵ�е��Ӷ���
    public Order(int day, GoodsEnum goodsEnum, int cost, int sum)
    {
        orders = new Dictionary<GoodsEnum, Order>();
        Day = day;
        goods = goodsEnum;
        price = cost;
        remainprice = cost;
        this.sum = sum;
        remainsum = sum;
        state = OrderState.hasFinish;
    }
}


/// <summary>
/// ����������
/// </summary>
public class OrderPipline:Pipline
{
    /// <summary>
    /// �����б�
    /// </summary>
    public List<Order> orderTree;
    public BuildingObj building;
    /// <summary>
    /// ��Ҫ����ĳ����Ʒ,��Ҫ��˭
    /// </summary>
    /// 
    public class OrderInf
    {
        public class OrderDetail
        {
            public int price;//���ṩ�ļ۸�
            public BuildingObj building;//�����Ľ���
            public int Day;
            public OrderDetail(int price, BuildingObj building, int day)
            {
                this.price = price;
                this.building = building;
                Day = day;
            }
        }
        public Dictionary<GoodsEnum, OrderDetail> orderInf=new Dictionary<GoodsEnum, OrderDetail>();
    }
    //һ��������Ӧ�Ķ�����Ϣ
    public Dictionary<int,OrderInf> orderMemory;
    public OrderPipline(BuildingObj building) 
    {
        this.building = building;
        orderMemory = new Dictionary<int,OrderInf>();
    }
    /// <summary>
    /// ��������ӵ��Ķ���
    /// </summary>
    /// <param name="order"></param>
    public void BOrder(Order order)
    {
        //�Լ��ܹ�����
        orderTree.Add(order);
        var inf=orderMemory[order.id];
        if(building.getIn()!=null)//�Լ��Ϳ�������
        foreach(var item in inf.orderInf)
        {
            var ins = building.GetPipline<GenGoodsPipline>().trans.from.source.Find(e=> { return e.Item1 == item.Key; }).Item2;
            var b=item.Value.building;
            var tempOrder = new Order(order.Day,item.Key,item.Value.price,order.sum*ins);
            b.GetPipline<OrderPipline>().BOrder(tempOrder);  
        }
    }
    /// <summary>
    /// ��Ϊ�׷����Ͷ���
    /// </summary>
    /// <param name="order"></param>
    public void AOrder(Order order)
    {
        //��ӵ��������ͺ�
        orderTree.Add(order);
    }
    /// <summary>
    /// ��ȡ�ܹ������ͳɱ��۸�
    /// �۸�,ʱ��
    /// </summary>
    /// <returns></returns>
    public Pair<int,int> GetMinGoodsCost(Order order)
    {
        int sum = order.sum;
        var trans = building.GenGoodsPipline.trans;//��Ʒ
        int sumCost = 0;
        int sumDay=0;
        //��Ʒû�н��ڼ۸�
        if (building.getIn() != null)
            foreach (var ins in trans.from.source)
            {
                var ret = GameArchitect.get.economicSystem.GetMinGoodsCost(ins.Item1,ins.Item2*sum,order.Day);
                var retC = ret.Item3;
                var retB=ret.Item2;
                var retA=ret.Item1;
                orderMemory[order.id].orderInf[ins.Item1]=new OrderInf.OrderDetail(retA,retB,retC);
                sumDay = Math.Max(sumDay,retC);
                sumCost += retA;
            }
        sumCost += 10;
        return new Pair<int, int>(sumCost,sumDay);
    }
    /// <summary>
    /// ����,����,������������򷵻���
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Tuple<bool, int,int> PlanOrder(Order order)
    {
        int time = order.Day;
        int money = order.price;
        int sum = order.sum;
        orderMemory.Add(order.id, new OrderInf());
        var ret = GetMinGoodsCost(order);//��ȡ��������С�۸�
        return new Tuple<bool, int, int>((GetAllGoodsSum(time) - GetOrderGoodsSum(time)) >= sum && ret.Item1 <= money&&ret.Item2<=time, ret.Item1,ret.Item2);
    }
    /// <summary>
    /// ��ȡһ��ʱ����ܲ�������
    /// </summary>
    /// <returns></returns>
    public int GetAllGoodsSum(int time)
    {
        return 10;
        ////ͨ��Ԥ�������.
        ////����ÿ��������
        ////ÿ���ܹ�
        //// ����ÿ���������������ã����ɹ����趨��
        //int maxDailyProduction = maxPipSum;

        //// ʵ��ÿ����ò��ܣ���������Դ���豸״̬�ȣ��ɸ��ݾ������������
        //int actualDailyProduction = GetAvailableDailyProduction(time);

        //// ���������ܲ���
        //int theoreticalTotalProduction = maxDailyProduction * time;

        //// ʵ���ܲ��ܣ�������ʵ��ÿ��Ŀ��ò���
        //int actualTotalProduction = actualDailyProduction * time;

        //// ������������ܲ��ܺ�ʵ���ܲ����еĽ�Сֵ
        //return Math.Min(theoreticalTotalProduction, actualTotalProduction);
    }
    public int GetAvailableDailyProduction(int time)
    {
        return 10;
    }
    public int GetOrderGoodsSum(int time)
    {
        return 10;
    }

}

/// <summary>
/// ����,�������ý�����Update����,Job����,��Ϊ����
/// </summary>
public class Pipline
{
    public BuildingObj belong;
    /// <summary>
    /// ����
    /// </summary>
    public virtual void Update()
    {

    }
    public virtual void Init(BuildingObj building)
    {
        belong = building;
        building.pipLineManager.pairs.Add(this);
    }
}


