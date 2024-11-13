using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
[Serializable]
public class BuildingState:BaseState
{
    public int data;
    /// <summary>
    /// 所属于的人
    /// </summary>
    public NpcObj belong;
    [SerializeField]
    /// <summary>
    /// 商品列表
    /// </summary>
    public Dictionary<GoodsEnum, int> goodslist;
    
    public BuildingState(BuildingObj buildingObj):base(buildingObj)
    {
        goodslist = new Dictionary<GoodsEnum, int>();
        goist.Add(1, 1);
        goist.Add(2, 2);
        Init();
    }
    public override void Init()
    {
        base.Init();
        foreach(var item in goodslist)
        {
            goodslist[item.Key] = 0;
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
    [SerializeField]
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
    }

    public override void InitEconomicInf()
    {
        ecInf = new BuildingEc(this);
    }

    public override string ShowString()
    {
        throw new NotImplementedException();
    }
}

