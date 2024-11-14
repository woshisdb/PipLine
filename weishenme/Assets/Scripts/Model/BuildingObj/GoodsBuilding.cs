using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsBuildingState : BuildingState
{
    /// <summary>
    /// ��Ʒ��������Ϣ
    /// </summary>
    public BuildingMeta buildingMeta;
    /// <summary>
    /// ѭ������
    /// </summary>
    public CircularQueue<Float> generateList;
    /// <summary>
    /// ����������Ŀ
    /// </summary>
    public Float prodSate;
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public GoodsManager goodsManager;

    public Dictionary<GoodsEnum, NeedGoods> needs;
    public Dictionary<GoodsEnum, SendGoods> sends;
    public SendWork sendWork;//�����һϵ�й���
    public GoodsBuildingState(GoodsBuildingObj obj) : base(obj)
    {
        buildingMeta = Meta.Instance.getMeta(BuildingEnum.building1);
        goodsManager = new GoodsManager(buildingMeta.GetGoods());
        generateList = new CircularQueue<Float>(10);
        foreach (var item in buildingMeta.inputs)
        {
            goodslist[item.Item1] = 0;
        }
        goodslist[buildingMeta.output.Item1] = 0;
    }
    public override void Init()
    {
        base.Init();
        generateList.Clear();
        prodSate = 0;
    }
}
public class GoodsBuildingEc : BuildingEc
{
    public GoodsBuildingEc(BaseObj obj) : base(obj)
    {
    }
}
/// <summary>
/// ������Ʒ�Ľ���
/// </summary>
public class GoodsBuildingObj : BuildingObj, EmploymentFactory
{
    public new GoodsBuildingState now{get{return (GoodsBuildingState)getNow();}}
    public GoodsBuildingObj()
    {
        now.needs = new Dictionary<GoodsEnum, NeedGoods>();
        var state = (GoodsBuildingState)now;
        var inputs=state.buildingMeta.inputs;
        var output = state.buildingMeta.output;
        now.sends = new Dictionary<GoodsEnum, SendGoods>();
        foreach(var item in inputs)
        {
            var needGoods= new NeedGoods();
            needGoods.goods=item.Item1;
            needGoods.obj = this;
            now.needs[item.Item1] = needGoods;
        }
        var sendGoods=new SendGoods();
        sendGoods.goods = output.Item1;
        sendGoods.obj = this;
        now.sends[output.Item1] = sendGoods;
    }

    public void addMoney(Float money)
    {
        throw new NotImplementedException();
    }

    public SceneObj aimPos()
    {
        throw new NotImplementedException();
    }

    public override void BefThink()
    {
        base.BefThink();
        
    }
    /// <summary>
    /// ����������������Ʒ��ˮ��
    /// </summary>
    /// <param name="input"></param>
    /// <param name="outpu"></param>
    public void GeneratePipline()
    {
        var state = (GoodsBuildingState)now;
        // ������ˮ�ߣ��Ӷ�β��ʼ
        for (int i = 0; i < state.generateList.Size(); i++)
        {
            if (state.prodSate >= 0)
            {
                // ��ȡ��ǰ��ˮ�߽ڵ㣬FindTail(i) �Ӷ�β��ͷ�����ҵ� i ��Ԫ��
                var node = state.generateList.FindTail(i);

                // �ж��Ƿ�Ϊ���һ���ڵ�
                if (i == 0) // �������һ���ڵ㣨��β��
                {
                    var needRed = Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    var tempNow = node;
                    node -= needRed;
                    int allCreate = Mathf.CeilToInt(tempNow) - Mathf.CeilToInt(node);
                    // ������Ʒ�Ƿ��������������������뵽 goodslist ��
                    foreach (var input in now.buildingMeta.inputs)
                    {
                        // ������Ʒ����
                        state.goodslist[input.Item1] += input.Item2 * allCreate;
                    }
                }
                else // ���������ڵ㣨�Ƕ�β��
                {
                    // ����Ʒ��ǰ�ƽ���ģ����ˮ�ߣ�
                    var nextNode = state.generateList.FindTail(i - 1); // ������һ���ڵ�
                    nextNode = node;
                    var needRed = Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    nextNode += needRed;
                    node -= needRed;
                }
            }
        }
    }

    public void GetGoodsProcess(BaseState state, GoodsEnum goodsEnum, int sum)
    {
        throw new NotImplementedException();
    }

    public NpcObj GetNpc()
    {
        throw new NotImplementedException();
    }

    public void GetProdProcess(INeedWork worker)
    {
        throw new NotImplementedException();
    }

    public void MaxMoney()
    {
        throw new NotImplementedException();
    }

    public SceneObj nowPos()
    {
        throw new NotImplementedException();
    }

    public void reduceMoney(Float money)
    {
        throw new NotImplementedException();
    }

    public SendGoods[] RegisterReceiveGoods()
    {
        throw new NotImplementedException();
    }

    public NeedGoods[] RegisterSendGoods()
    {
        throw new NotImplementedException();
    }

    public SendWork[] RegisterSendWork()
    {
        throw new NotImplementedException();
    }

    public SendGoods[] UnRegisterReceiveGoods()
    {
        throw new NotImplementedException();
    }

    public NeedGoods[] UnRegisterSendGoods()
    {
        throw new NotImplementedException();
    }

    public SendWork[] UnRegisterSendWork()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// ����״̬����
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override void Update()
    {
        GeneratePipline();
    }
}

///// <summary>
///// ��Ʒ�ӹ���,�ܹ�������Ʒ,������Ʒ,����������
///// </summary>
//public class ProcessFactoryObj : BuildingObj, EmploymentFactory
//{
//    public BuildingState now { get { return (BuildingState)getNow(); } }
//    /// <summary>
//    /// ����������������Ʒ��ˮ��
//    /// </summary>
//    /// <param name="input"></param>
//    /// <param name="outpu"></param>
//    public void GeneratePipline(GoodsBuildingState state)
//    {
//        // ������ˮ�ߣ��Ӷ�β��ʼ
//        for (int i = 0; i < state.generateList.Size(); i++)
//        {
//            if (state.prodSate >= 0)
//            {
//                // ��ȡ��ǰ��ˮ�߽ڵ㣬FindTail(i) �Ӷ�β��ͷ�����ҵ� i ��Ԫ��
//                var node = state.generateList.FindTail(i);

//                // �ж��Ƿ�Ϊ���һ���ڵ�
//                if (i == 0) // �������һ���ڵ㣨��β��
//                {
//                    var needRed = Math.Min(state.prodSate, node);
//                    state.prodSate -= needRed;
//                    var tempNow = node;
//                    node -= needRed;
//                    int allCreate = Mathf.CeilToInt(tempNow) - Mathf.CeilToInt(node);
//                    // ������Ʒ�Ƿ��������������������뵽 goodslist ��
//                    foreach (var input in ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs)
//                    {
//                        // ������Ʒ����
//                        state.goodslist[input.Item1].sum += input.Item2 * allCreate;
//                    }
//                }
//                else // ���������ڵ㣨�Ƕ�β��
//                {
//                    // ����Ʒ��ǰ�ƽ���ģ����ˮ�ߣ�
//                    var nextNode = state.generateList.FindTail(i - 1); // ������һ���ڵ�
//                    nextNode = node;
//                    var needRed = Math.Min(state.prodSate, node);
//                    state.prodSate -= needRed;
//                    nextNode += needRed;
//                    node -= needRed;
//                }
//            }
//        }
//    }
//    /// <summary>
//    /// ���������
//    /// </summary>
//    /// <param name="prods"></param>
//    /// <param name="state"></param>
//    public void AddProds(Tuple<ProdEnum, Int>[] prods,BuildingState state)
//    {

//    }

//    /// <summary>
//    /// ����Ʒ�ӵ����ߵĶ�β��
//    /// </summary>
//    /// <param name="state"></param>
//    /// <param name="val"></param>
//    public void AddPipline(GoodsBuildingState state, int val)
//    {
//        state.generateList.FindFront(0);
//    }
//    /// <summary>
//    /// ����״̬����
//    /// </summary>
//    /// <param name="input"></param>
//    /// <returns></returns>
//    public override void Update(BaseState input)
//    {
//        var npc = input as GoodsBuildingState;
//        var prodOrderInf = EconmicSystem.get.GetProdMoney(input,((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10);
//        if (prodOrderInf.cost <= npc.ecState.money)//���Թ��������
//        {
//            EconmicSystem.Instance.Buy(this, input, prodOrderInf);//����������
//        }
//        var goodsOrderInf = EconmicSystem.get.GetGoodsMoney(input,((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10);
//        if (goodsOrderInf.cost <= npc.ecState.money)//������Ʒ
//        {
//            EconmicSystem.Instance.Buy(this, input, goodsOrderInf);//����������
//        }
//        ///������ˮ��
//        GeneratePipline(input);
//    }
//    /// <summary>
//    /// ������һ���״̬Ԥ����һ��֮��ı仯
//    /// </summary>
//    /// <param name="input"></param>
//    /// <param name="day"></param>
//    public override void Predict(BaseState input,int day)
//    {
//        var prodOrderInf = EconmicSystem.get.GetProdMoney(input, ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10,day,true);
//        if (prodOrderInf.cost <= input.money)//���Թ��������
//        {
//            EconmicSystem.Instance.Buy(this, input, prodOrderInf);//����������
//        }
//        var goodsOrderInf = EconmicSystem.get.GetGoodsMoney(input, ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10, day, true);
//        if (goodsOrderInf.cost <= input.money)//������Ʒ
//        {
//            EconmicSystem.Instance.Buy(this, input, goodsOrderInf);//����������
//        }
//        ///������ˮ��
//        GeneratePipline(input);
//    }
//    public void addMoney(BaseState state, Float money)
//    {
//        (state as GoodsBuildingState).money += money;
//    }

//    public void reduceMoney(BaseState state, Float money)
//    {
//        (state as GoodsBuildingState).money -= money;
//    }
//    /// <summary>
//    /// �޸ĵ�ǰ��״��,���ݶ���
//    /// </summary>
//    /// <param name="money"></param>
//    /// <param name="goodsInf"></param>
//    public void receiveMoney(Float money, NormalGoodsInf goodsInf)
//    {
//        var now = getNow();//Ҫ�޸ĵĵ�ǰ���
//        //var normalGoodsInf=goodsInf as NormalGoodsInf;
//        now.goodslist[goodsInf.goodsEnum].sum -= goodsInf.sum;
//        addMoney(now, goodsInf.goodsPrice);
//    }
//    /// <summary>
//    /// ע����յĶ���
//    /// </summary>
//    public void registerReveiveGoodsOrder()
//    {

//    }

//    public Dictionary<GoodsEnum, GoodsObj> GetGoodsList(BaseState state)
//    {
//        return ((GoodsBuildingState)state).goodslist;
//    }

//    public Float GetProdState(BaseState state)
//    {
//        return ((GoodsBuildingState)state).prodSate;
//    }

//    public CircularQueue<Float> GetPipline(BaseState state)
//    {
//        return ((GoodsBuildingState)state).generateList;
//    }

//    public void unregisterReveiveGoodsOrder()
//    {
//        throw new NotImplementedException();
//    }
//}

///// <summary>
///// ��Ʒԭ����������,ֻ���ڲ�����
///// </summary>
//public class SourceEmploymentFactoryObj: BuildingObj, SourceEmploymentFactory
//{

//}

///// <summary>
///// �ܹ�ת����Ʒ,�˵Ľ���
///// </summary>
//public class TransBuildingObj:BuildingObj, TransGoodsFactory
//{

//}

