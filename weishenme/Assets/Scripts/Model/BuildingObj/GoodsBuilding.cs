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
    public List<Float> generateList;
    /// <summary>
    /// ����������Ŀ
    /// </summary>
    public float prodSate { get { return sendWork.getRate; } }
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
    public GoodsBuildingState(GoodsBuildingObj obj, BuildingEnum buildingEnum) : base(obj, buildingEnum)
    {
        goodsManager = new GoodsManager(buildingMeta.GetGoods(), obj);
        generateList = new List<Float>();
        for (int i = 0; i <buildingMeta.spendTime; i++)
        {
            generateList.Add(new Float(0));
        }
        //generateList.Clear();
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
        Market.Instance.Register(now.sendWork);
    }

    public void addMoney(Float money)
    {
        now.money.value += money;
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
        now.generateList[0].Value+= canPutSum * now.buildingMeta.pipOutput.Item2;
    }
    /// <summary>
    /// ����������������Ʒ��ˮ��
    /// </summary>
    /// <param name="input"></param>
    /// <param name="outpu"></param>
    public void GeneratePipline()
    {
        var state = (GoodsBuildingState)now;
        float prodState = now.prodSate;
        // ������ˮ�ߣ��Ӷ�β��ʼ
        for (int i = state.generateList.Count-1; i >=0; i--)
        {
            if (prodState >= 0)
            {
                // ��ȡ��ǰ��ˮ�߽ڵ㣬FindTail(i) �Ӷ�β��ͷ�����ҵ� i ��Ԫ��
                var node = state.generateList[i];

                // �ж��Ƿ�Ϊ���һ���ڵ�
                if (i == state.generateList.Count - 1) // �������һ���ڵ㣨��β��
                {
                    var needRed = Mathf.CeilToInt(Math.Min(prodState, node));
                    prodState -= needRed;
                    var tempNow = node;
                    node.value -= needRed;
                    int allCreate = (int)needRed;
                    // ������Ʒ�Ƿ��������������������뵽 goodslist ��
                    // ������Ʒ����
                    state.goodsManager.goods[now.buildingMeta.pipOutput.Item1].sum.value += now.buildingMeta.pipOutput.Item2 * allCreate;
                }
                else // ���������ڵ㣨�Ƕ�β��
                {
                    // ����Ʒ��ǰ�ƽ���ģ����ˮ�ߣ�
                    var nextNode = state.generateList[i + 1]; // ������һ���ڵ�
                    //nextNode = node;
                    var needRed = Math.Min(prodState, node);
                    prodState -= needRed;
                    nextNode.Value += needRed;
                    node.Value -= needRed;
                }
            }
        }
    }

    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum)
    {
        now.goodsManager.goods[goodsEnum].sum.value+=sum;
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
        now.money.value-=money;
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

    public Float getMoney()
    {
        return now.money;
    }
    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        ret.Add(new KVItemBinder(() =>
        {
            return "xy";
        },
        () =>
        {
            return "(" + x + "," + y + ")";
        }));
        ret.Add(new KVItemBinder(() =>
        {
            return "scene";
        }, () =>
        {
            return "(" + scene.now.x + "," + scene.now.y + ")";
        }));
        var goods = new List<UIItemBinder>();
        foreach(var g in now.goodsManager.goods)
        {
            var gx = g;
            goods.Add(new KVItemBinder(() =>
            {
                return gx.Key.ToString();
            }, () =>
            {
                return gx.Value.sum.Value.ToString();
            }));
        }
        var nowUI = new List<UIItemBinder>()
        {
            new KVItemBinder(()=>{
                return "money";
            },()=>{
                return now.money.Value.ToString();
            }),
            new TableItemBinder(()=>{
                return "goods";
            },goods),

        };
        ret.Add(new TableItemBinder(() =>
        {
            return "now";
        }, nowUI));
        return ret;
    }
}



