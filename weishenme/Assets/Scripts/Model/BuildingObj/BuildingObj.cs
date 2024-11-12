using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class BuildingState:BaseState
{
    /// <summary>
    /// 所属于的人
    /// </summary>
    public NpcObj belong;
    /// <summary>
    /// 商品列表
    /// </summary>
    public Dictionary<GoodsEnum, GoodsObj> goodslist;

    public BuildingState(BuildingObj buildingObj):base(buildingObj)
    {
        goodslist = new Dictionary<GoodsEnum, GoodsObj>();
        Init();
    }
    public override void Init()
    {
        base.Init();
        foreach(var item in goodslist)
        {
            goodslist[item.Key] = new GoodsObj();
        }
    }

}

public class BuildingEc : EconomicInf
{
    public BuildingEc(BaseObj obj) : base(obj)
    {
    }
}



/// <summary>
/// 建筑对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
    public BuildingState now { get { return (BuildingState)getNow(); } }
    /// <summary>
    /// 场景所在的位置
    /// </summary>
    public SceneObj scene;
    /// <summary>
    /// 建筑对象
    /// </summary>
    public BuildingObj():base()
    {

    }
    public override void Update()
    {

    }
    /// <summary>
    /// 预测状态
    /// </summary>
    /// <param name="input"></param>
    /// <param name="day"></param>
    public override void Predict(BaseState input,int day)
    {

    }
    public virtual void BefThink()
    {

    }

    public override void InitBaseState()
    {
        state= new BuildingState(this);
        ecInf = new BuildingEc(this);
    }

    public override void InitEconomicInf()
    {
        throw new NotImplementedException();
    }

    public override string ShowString()
    {
        throw new NotImplementedException();
    }
    public virtual (StringInputItem, List<TextInputItem>) showInf()
    {
        var item1 = new StringInputItem("Building");
        var item2=new List<TextInputItem>();
        foreach (var g in now.goodslist)
        {
            item2.Add(new IntInputItem(g.Key.ToString(), () => { return 1; }, e => { }));
        }
        return (item1, item2);
    }
}

