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
    public BuildingState():base()
    {
        goodslist = new Dictionary<GoodsEnum, GoodsObj>();
        Init();
    }
    public override void Init()
    {
        base.Init();
        money = 0;
        foreach(var item in goodslist)
        {
            goodslist[item.Key] = new GoodsObj();
        }
    }
}

public class BuildingEc:EconomicInf
{

}



/// <summary>
/// 建筑对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
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
    public override void Update(BaseState input)
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
}

