using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Goods
{
    public Int sum=0;
    public Float cost=0;
}
public class BuildingState:BaseState
{
    public Float money;//�ܵ��ʽ�
    public Dictionary<GoodsEnum, Goods> goodslist;
    public BuildingState():base()
    {
        goodslist = new Dictionary<GoodsEnum, Goods>();
        money = 0;
        Init();
    }
    public override void Init()
    {
        base.Init();
        money = 0;
        foreach(var item in goodslist)
        {
            goodslist[item.Key] = new Goods();
        }
    }
}

public class BuildingEc:EconomicInf
{

}

/// <summary>
/// ��������
/// </summary>
/// <typeparam name="T"></typeparam>
public class BuildingObj<T,F> :BaseObj<T, F>,ISendEvent,ISendCommand,IRegisterEvent 
where T:BuildingState,new()
where F:BuildingEc,new()
{
    /// <summary>
    /// ��������
    /// </summary>
    public BuildingObj():base()
    {

    }
    public override T Update(T input, T output)
    {
        output.Init();
        output.money=input.money;
        foreach(var x in input.goodslist)
        {
            output.goodslist[x.Key].sum = input.goodslist[x.Key].sum;
            output.goodslist[x.Key].cost = input.goodslist[x.Key].cost;
        }
        return output;
    }
    /// <summary>
    /// ��ȡ��������������С����
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public GoodsOrder GetProdMoney(Tuple<ProdEnum, Int>[] prods,Int num)
    {
        return null;
    }
    /// <summary>
    /// ��ȡ������Ʒ����С����
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public GoodsOrder GetGoodsMoney(Tuple<GoodsEnum, Int>[] prods, Int num)
    {
        return null;
    }
}

public class GoodsBuildingState:BuildingState
{
    public GoodsStateMeta goodsStateMeta;
    /// <summary>
    /// ѭ������
    /// </summary>
    public CircularQueue<Float> generateList;
    public Float prodSate;
    public GoodsBuildingState():base()
    {
        goodsStateMeta = (GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>();
        generateList = new CircularQueue<Float>(10);
        foreach (var item in goodsStateMeta.inputs)
        {
            goodslist[item.Item1] = new Goods();
        }
        goodslist[goodsStateMeta.output.Item1] = new Goods();
    }
    public override void Init()
    {
        base.Init();
        generateList.Clear();
        prodSate = 0;
    }
}

public class GoodsBuildingEc: BuildingEc
{

}

public class GoodsBuildingObj:BuildingObj<GoodsBuildingState, GoodsBuildingEc>,ICanReceiveNormalGoodsOrder, INormalGoodsOrderUser, IProdOrderUser
{

    /// <summary>
    /// ����������������Ʒ��ˮ��
    /// </summary>
    /// <param name="input"></param>
    /// <param name="outpu"></param>
    public void GeneratePipline(GoodsBuildingState state)
    {
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
                    node-=needRed;
                    int allCreate = Mathf.CeilToInt(tempNow)- Mathf.CeilToInt(node);
                    // ������Ʒ�Ƿ��������������������뵽 goodslist ��
                    foreach (var input in ((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs)
                    {
                        // ������Ʒ����
                        state.goodslist[input.Item1].sum += input.Item2 * allCreate;
                    }
                }
                else // ���������ڵ㣨�Ƕ�β��
                {
                    // ����Ʒ��ǰ�ƽ���ģ����ˮ�ߣ�
                    var nextNode = state.generateList.FindTail(i - 1); // ������һ���ڵ�
                    nextNode = node;
                    var needRed=Math.Min(state.prodSate, node);
                    state.prodSate -= needRed;
                    nextNode +=needRed;
                    node -= needRed;
                }
            }
        }
    }
    /// <summary>
    /// ����Ʒ�ӵ����ߵĶ�β��
    /// </summary>
    /// <param name="state"></param>
    /// <param name="val"></param>
    public void AddPipline(GoodsBuildingState state,int val)
    {
        state.generateList.FindFront(0);
    }

    public override GoodsBuildingState Update(GoodsBuildingState input, GoodsBuildingState output)
    {
        base.Update(input, output);//����״̬
        var prodOrderInf = GetProdMoney(((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).prods, 10);
        if (prodOrderInf.cost <= output.money)//���Թ��������
        {
            EconmicSystem.Instance.Buy(this,output,prodOrderInf);//����������
        }
        var goodsOrderInf = GetGoodsMoney(((GoodsStateMeta)Meta.GetMeta<GoodsBuildingState>()).inputs, 10);
        if (goodsOrderInf.cost<= output.money)//������Ʒ
        {
            EconmicSystem.Instance.Buy(this, output, goodsOrderInf);//����������
        }
        ///������ˮ��
        GeneratePipline(output);
        return output;
    }

    public void addMoney(BaseState state, Float money)
    {
        (state as GoodsBuildingState).money += money;
    }

    public void reduceMoney(BaseState state, Float money)
    {
        (state as GoodsBuildingState).money -= money;
    }
    /// <summary>
    /// �޸ĵ�ǰ��״��,���ݶ���
    /// </summary>
    /// <param name="money"></param>
    /// <param name="goodsInf"></param>
    public void receiveMoney(Float money, NormalGoodsInf goodsInf)
    {
        var now=getNow();//Ҫ�޸ĵĵ�ǰ���
        //var normalGoodsInf=goodsInf as NormalGoodsInf;
        now.goodslist[goodsInf.goodsEnum].sum -= goodsInf.sum;
        addMoney(now, goodsInf.goodsPrice);
    }
    //ע����յĶ���
    public void registerReveiveGoodsOrder()
    {

    }

    public Dictionary<GoodsEnum, Goods> GetGoodsList(BaseState state)
    {
        return ((GoodsBuildingState)state).goodslist;
    }

    public Float GetProdState(BaseState state)
    {
        return ((GoodsBuildingState)state).prodSate;
    }

    public CircularQueue<Float> GetPipline(BaseState state)
    {
        return ((GoodsBuildingState)state).generateList;
    }
}