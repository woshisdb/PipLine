using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class BuildingState:BaseState
{
    /// <summary>
    /// �����ڵ���
    /// </summary>
    public NpcObj belong;
    /// <summary>
    /// ��Ʒ�б�
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
    /// <summary>
    /// Ԥ��״̬
    /// </summary>
    /// <param name="input"></param>
    /// <param name="day"></param>
    public override void Predict(BaseState input,int day)
    {

    }
}

