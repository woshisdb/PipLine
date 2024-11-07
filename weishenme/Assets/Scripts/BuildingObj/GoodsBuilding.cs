using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsBuildingState : BuildingState
{
    /// <summary>
    /// ��Ʒ��������Ϣ
    /// </summary>
    public GoodsStateMeta goodsStateMeta;
    /// <summary>
    /// ѭ������
    /// </summary>
    public CircularQueue<Float> generateList;
    /// <summary>
    /// ����������Ŀ
    /// </summary>
    public Float prodSate;
    public GoodsBuildingState() : base()
    {
        goodsStateMeta = (GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>();
        generateList = new CircularQueue<Float>(10);
        foreach (var item in goodsStateMeta.inputs)
        {
            goodslist[item.Item1] = new GoodsObj();
        }
        goodslist[goodsStateMeta.output.Item1] = new GoodsObj();
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

}
/// <summary>
/// ������Ʒ�Ľ���
/// </summary>
public class GoodsBuildingObj : BuildingObj
{

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

