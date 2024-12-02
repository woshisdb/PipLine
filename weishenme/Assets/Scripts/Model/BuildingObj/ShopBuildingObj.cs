using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuildingState : BuildingState
{
    /// <summary>
    /// ����������Ŀ
    /// </summary>
    public Float prodSate;
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public GoodsManager goodsManager;
    public int allSum;
    /// <summary>
    /// һϵ������
    /// </summary>
    public Dictionary<GoodsEnum, NeedGoods> needs;
    /// <summary>
    /// һϵ�з���
    /// </summary>
    public Dictionary<GoodsEnum, SendGoods> sends;
    ///�����һϵ�й���
    public SendWork sendWork;
    public BuildingEnum GetEnum()
    {
        return buildingMeta.ReturnEnum();
    }
    public ShopBuildingState(ShopBuildingObj obj, BuildingEnum buildingEnum) : base(obj, buildingEnum)
    {
        goodsManager = new GoodsManager(buildingMeta.GetGoods(), obj);
        prodSate = 0;
    }
}
public class ShopBuildingEc : BuildingEc
{
    public ShopBuildingEc(BaseObj obj) : base(obj)
    {
    }
}


/// <summary>
/// ����������Ʒ�Ľ���
/// </summary>
public class ShopBuildingObj : BuildingObj, MarketFactory
{
    public new ShopBuildingState now { get { return (ShopBuildingState)getNow(); } }

    public ShopBuildingObj(BuildingEnum buildingEnum) : base()
    {
        this.state = new ShopBuildingState(this, buildingEnum);
        now.needs = new Dictionary<GoodsEnum, NeedGoods>();
        var state = now;
        var inputs = state.buildingMeta.inputs;
        var output = state.buildingMeta.output;
        now.sends = new Dictionary<GoodsEnum, SendGoods>();
        ///��ʼ������������
        ///������Ʒ
        foreach (var item in inputs)
        {
            var needGoods = new NeedGoods(this);
            needGoods.goods = item.Item1;
            needGoods.obj = this;
            now.needs[item.Item1] = needGoods;
        }
        ///������Ʒ
        var sendGoods = new SendGoods(this);
        sendGoods.goods = output.Item1;
        sendGoods.obj = this;
        now.sends[output.Item1] = sendGoods;
        ///���͹���
        now.sendWork = new SendWork(this);
    }
    public void addMoney(Float money)
    {
        now.money.value += money;
    }

    public SceneObj aimPos()
    {
        throw new System.NotImplementedException();
    }

    public NpcObj GetNpc()
    {
        throw new System.NotImplementedException();
    }

    public void GetProdProcess(INeedWork worker)
    {
        throw new System.NotImplementedException();
    }

    public float GetSendWorkRate(NeedWork needWork)
    {
        throw new System.NotImplementedException();
    }

    public void MaxMoney()
    {
        throw new System.NotImplementedException();
    }

    public float NeedGoodsSatifyRate(SendGoods sendGoods)
    {
        throw new System.NotImplementedException();
    }

    public SceneObj nowPos()
    {
        throw new System.NotImplementedException();
    }

    public void reduceMoney(Float money)
    {
        now.money.value -= money;
    }

    public NeedGoods[] RegisterNeedGoods()
    {
        throw new System.NotImplementedException();
    }

    public SendGoods[] RegisterSendGoods()
    {
        throw new System.NotImplementedException();
    }

    public SendWork[] RegisterSendWork()
    {
        throw new System.NotImplementedException();
    }

    public float SendGoodsSatifyRate(NeedGoods goods)
    {
        throw new System.NotImplementedException();
    }

    public NeedGoods[] UnRegisterNeedGoods()
    {
        throw new System.NotImplementedException();
    }

    public SendGoods[] UnRegisterSendGoods()
    {
        throw new System.NotImplementedException();
    }

    public SendWork[] UnRegisterSendWork()
    {
        throw new System.NotImplementedException();
    }

    public Float getMoney()
    {
        return now.money;
    }

    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum)
    {
        throw new System.NotImplementedException();
    }
    
    public override float effect()
    {
        throw new System.NotImplementedException();
    }
}
