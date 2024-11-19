using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class BuildingState:BaseState
{
    /// <summary>
    /// 建筑的各种信息
    /// </summary>
    public BuildingMeta buildingMeta { get { return Meta.Instance.getMeta(buildingEnum); } }
    /// <summary>
    /// 所属于的人
    /// </summary>
    public NpcObj belong;
    public BuildingEnum buildingEnum;
    public BuildingState(BuildingObj buildingObj,BuildingEnum buildingEnum):base(buildingObj)
    {
        this.buildingEnum = buildingEnum;
        //buildingMeta=Meta.Instance.getMeta(buildingEnum);
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
    public int x;
    public int y;
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
    }

    public override void InitEconomicInf()
    {
        ecInf = new BuildingEc(this);
    }

    public override string ShowString()
    {
        return null;
    }
    /// <summary>
    /// 创建对象后初始化
    /// </summary>
    public virtual void Init()
    {

    }
}

