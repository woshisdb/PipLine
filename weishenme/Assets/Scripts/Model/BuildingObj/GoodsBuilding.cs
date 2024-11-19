using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsBuildingState : BuildingState
{
    /// <summary>
    /// ������Ʒ����Ŀ
    /// </summary>
    public int allResourceSum;
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
    public BuildingEnum GetEnum()
    {
        return buildingMeta.ReturnEnum();
    }
    public GoodsBuildingState(GoodsBuildingObj obj,BuildingEnum buildingEnum) : base(obj,buildingEnum)
    {
        goodsManager = new GoodsManager(buildingMeta.GetGoods(),obj);
        generateList = new CircularQueue<Float>(buildingMeta.spendTime);
        generateList.Clear();
        prodSate = 0;
        allResourceSum = 10;
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
    public GoodsBuildingObj(BuildingEnum buildingEnum):base()
    {
        this.state=new GoodsBuildingState(this,buildingEnum);
        now.needs = new Dictionary<GoodsEnum, NeedGoods>();
        var state = now;
        var inputs=state.buildingMeta.inputs;
        var output = state.buildingMeta.output;
        now.sends = new Dictionary<GoodsEnum, SendGoods>();
        ///��ʼ������������
        ///������Ʒ
        foreach(var item in inputs)
        {
            var needGoods= new NeedGoods(this);
            needGoods.goods=item.Item1;
            needGoods.obj = this;
            now.needs[item.Item1] = needGoods;
        }
        ///������Ʒ
        var sendGoods=new SendGoods(this);
        sendGoods.goods = output.Item1;
        sendGoods.obj = this;
        sendGoods.remainSum = now.goodsManager.goods[output.Item1].sum;
        now.sends[output.Item1] = sendGoods;
        ///���͹���
        now.sendWork = new SendWork(this);
    }

    public void addMoney(Float money)
    {
        throw new NotImplementedException();
    }

    public SceneObj aimPos()
    {
        return this.scene;
    }

    public override void BefThink()
    {
        base.BefThink();
        var allsum = now.allResourceSum;
        var inputs=now.buildingMeta.inputs;
        var data = new List<Tuple<GoodsEnum,int>>();
        for (int i=0;i<inputs.Length;i++)
        {
            data.Add(new Tuple<GoodsEnum, int>(inputs[i].Item1, inputs[i].Item2 * allsum - now.goodsManager.goods[inputs[i].Item1].sum));
        }
        ///������Щ�����������Ŀ
        foreach(var item in data)
        {
            now.needs[item.Item1].needSum = Math.Max(item.Item2,0);
        }
    }
    /// <summary>
    /// ����Դ������ˮ��
    /// </summary>
    public void GenerateSource()
    {
        int canPutSum=10000000;
        foreach(var item in now.buildingMeta.pipInputs)
        {
            var sum=now.goodsManager.goods[item.Item1].sum;
            canPutSum=Math.Min(canPutSum,sum/item.Item2);
        }
        foreach (var item in now.buildingMeta.pipInputs)
        {
            var sum = now.goodsManager.goods[item.Item1].sum;
            sum.Value -= canPutSum* item.Item2;
        }
        //now.goodsManager.goods[now.buildingMeta.pipOutput.Item1].sum.Value
        now.generateList.FindFront(0).Value+= canPutSum * now.buildingMeta.pipOutput.Item2;
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
                    // ������Ʒ����
                    state.goodsManager.goods[now.buildingMeta.pipOutput.Item1].sum += now.buildingMeta.pipOutput.Item2 * allCreate;
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
        return now.belong;
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
        return this.scene;
    }

    public void reduceMoney(Float money)
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
        GenerateSource();
        GeneratePipline();
    }
    public override void Init()
    {
        var market = GameArchitect.Instance.market;
        var sends = RegisterSendGoods();
        if (sends != null)
        {
            foreach (var send in sends)
            {
                market.Register(send);
            }
        }
        var needs=RegisterNeedGoods();
        if(needs!=null)
        {
            foreach(var need in needs)
            {
                market.Register(need);
            }
        }
        var works = RegisterSendWork();
        foreach(var work in works)
        {
            market.Register(work);
        }
    }

    public SendWork[] RegisterSendWork()
    {
        SendWork[] sends = new SendWork[1];
        sends[0] = now.sendWork;
        return sends;
    }

    public SendWork[] UnRegisterSendWork()
    {
        SendWork[] sends = new SendWork[1];
        sends[0] = now.sendWork;
        return sends;
    }

    public SendGoods[] RegisterSendGoods()
    {
        var ret=new List<SendGoods>();
        foreach(var send in now.sends)
        {
            ret.Add(send.Value);
        }
        return ret.ToArray();
    }

    public SendGoods[] UnRegisterSendGoods()
    {
        var ret = new List<SendGoods>();
        foreach (var send in now.sends)
        {
            ret.Add(send.Value);
        }
        return ret.ToArray();
    }

    public NeedGoods[] RegisterNeedGoods()
    {
        var needGoods=new List<NeedGoods>();
        foreach (var need in now.needs)
        {
            needGoods.Add(need.Value);
        }
        return needGoods.ToArray();
    }

    public NeedGoods[] UnRegisterNeedGoods()
    {
        var needGoods = new List<NeedGoods>();
        foreach (var need in now.needs)
        {
            needGoods.Add(need.Value);
        }
        return needGoods.ToArray();
    }

    public float GetSendWorkRate(NeedWork needWork)
    {
        throw new NotImplementedException();
    }

    public float SendGoodsSatifyRate(NeedGoods goods)
    {
        throw new NotImplementedException();
    }

    public float NeedGoodsSatifyRate(SendGoods sendGoods)
    {
        throw new NotImplementedException();
    }
}



