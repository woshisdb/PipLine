using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class BuildingState:BaseState
{
    /// <summary>
    /// �����ĸ�����Ϣ
    /// </summary>
    public BuildingMeta buildingMeta { get { return Meta.Instance.getMeta(buildingEnum); } }
    /// <summary>
    /// �����ڵ���
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
/// ��������
/// </summary>
/// <typeparam name="T"></typeparam>
public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent
{
    [SerializeField]
    public BuildingState now { get { return (BuildingState)getNow(); } }
    public int x;
    public int y;
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
    public override void Update()
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
    /// ����������ʼ��
    /// </summary>
    public virtual void Init()
    {

    }
}

