using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcState:BaseState
{
    /// <summary>
    /// 金钱
    /// </summary>
    public Float money;
    /// <summary>
    /// 拥有的商品
    /// </summary>
    public Dictionary<GoodsEnum, Goods> goodslist;
    public NpcState():base()
    {
        goodslist = new Dictionary<GoodsEnum, Goods>();
        money = 0;
    }
    public override void Init()
    {
        base.Init();
        money = 0;
    }
}

public class NpcEc:EconomicInf
{

}

public class NpcObj : BaseObj<NpcState, NpcEc>,ICanReceiveProdOrder
{
    public override NpcState Update(NpcState input, NpcState output)
    {
        output.Init();
        return output;
    }
    public void addMoney(BaseState state, Float money)
    {
        ((NpcState)state).money += money;
    }
    public void receiveMoney(Float money, ProdGoodsInf goodsInf)
    {
        this.getNow().money += money;
    }
    public void reduceMoney(BaseState state, Float money)
    {
        ((NpcState)state).money -= money;
    }

    public void registerReveiveProdsOrder()
    {

    }
}
