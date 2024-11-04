using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Goods
{
    public GoodsEnum GoodsEnum;
    public Int sum=0;
    public Float cost=0;
    public BuildingObj inPlace;
    public NpcObj belong;
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
public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
    /// <summary>
    /// �������ڵ�λ��
    /// </summary>
    public SceneObj scene;
    /// <summary>
    /// ��������
    /// </summary>
    public BuildingObj():base()
    {

    }
    public override void Update(BaseState input)
    {
    }
    public override void Predict(BaseState input,int day)
    {

    }
    /// <summary>
    /// Ԥ��һ��δ��������
    /// </summary>
    /// <returns></returns>
    public float Rate()
    {

    }
}

