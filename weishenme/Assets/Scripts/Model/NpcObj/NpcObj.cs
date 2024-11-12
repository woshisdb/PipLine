using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����״̬
public class PhysicalState
{
    /// <summary>
    /// ����״̬,ʣ�������,��ǰ������״̬
    /// </summary>
    public int remainEnerge;//������ǰ������
    /// <summary>
    /// ����״̬,�������,���������½�,ÿһ���½�1��,��0��
    /// </summary>
    public int strength;
}

public class EcState
{
    /// <summary>
    /// �Ƿ���Ҫ����
    /// </summary>
    public bool needWork;
    /// <summary>
    /// �Ƿ�����Ѱ�ҹ���
    /// </summary>
    public bool allowFindJob;
    /// <summary>
    /// ����ִ���ܶ�����Ϊ
    /// </summary>
    public bool allowGo;
    /// <summary>
    /// ��������Ʒ
    /// </summary>
    public bool allowBuyThing;
    /// <summary>
    /// �Ƿ���������
    /// </summary>
    public bool isFreedom { get { return belong == null; } }
    /// <summary>
    /// �����ڵ���
    /// </summary>
    public NpcState belong;
    /// <summary>
    /// ��Ǯ
    /// </summary>
    public Float money;
    /// <summary>
    /// ǩ��ĺ�Լ
    /// </summary>
    public ContractList contracts;
    /// <summary>
    /// һϵ�е���������
    /// </summary>
    public List<EmploymentFactory> EmploymentFactories;
    public List<SourceEmploymentFactory> SourceEmploymentFactories;
    public List<TransGoodsFactory> transGoodsFactories;
    ///// <summary>
    ///// �Լ��ļ�
    ///// </summary>
    //public HouseBuildingState homeState;
    /// <summary>
    /// ���������
    /// </summary>
    public IncomeType incomeEnum;
}

public class ProdState
{
    /// <summary>
    /// һ�����ʱ��
    /// </summary>
    public int allTime { get { return Meta.dayTime; } }
    /// <summary>
    /// ʣ�����ɻʱ��
    /// </summary>
    public int remainTime;
    /// <summary>
    /// ��Ҫ������ʱ��
    /// </summary>
    public int workTime;
    /// <summary>
    /// ǰ�������ص��ʱ��
    /// </summary>
    public int goWorkTime;
    /// <summary>
    /// ���ʽ
    /// </summary>
    public LifeStyle lifeStyle;
}
/// <summary>
/// ÿ���˵ĸ�������
/// </summary>
public class NPcPower
{

}

public class NpcState:BaseState
{
    public SceneObj sceneObj;
    /// <summary>
    /// ����
    /// </summary>
    public EcState ecState;
    /// <summary>
    /// ������
    /// </summary>
    public ProdState prodState;
    /// <summary>
    /// ����
    /// </summary>
    public PhysicalState physicalState;
    //****************************************
    /// <summary>
    /// ��õ�ǰ��NPCState�����
    /// </summary>
    /// <returns></returns>
    public float Rate()
    {
        float ret = 0;
        foreach (var building in ecState.EmploymentFactories)
        {
            //ret+=building.Rate();//��ȡ��ǰÿ���������ϵ�������
        }
        return ret;
    }
    public NpcState(BaseObj obj):base(obj)
    {
        ecState.money = 0;
    }
    public override void Init()
    {
        base.Init();
        ecState.money = 0;
    }
}

public class NpcObj : BaseObj,INpc
{
    public NeedWork needWork;
    public NpcState now { get { return (NpcState)getNow(); } }
    /// <summary>
    /// ����ÿһ���������ϵ�����,��������npc.����contract
    /// </summary>
    /// <param name="input"></param>
    public override void Update()
    {
        
    }
    public void BefThink()
    {
        //����ÿһ��Э��
        var works = RegisterReceiveWork();
        foreach (var work in works)
        {
            Market.Instance.Register(work);
        }
    }
    public override void Predict(BaseState input, int day)
    {

    }

    public override void InitBaseState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEconomicInf()
    {
        throw new System.NotImplementedException();
    }

    public NeedWork[] RegisterReceiveWork()
    {
        return new NeedWork[] { needWork };
    }

    public NeedWork[] UnRegisterReceiveWork()
    {
        return new NeedWork[] { needWork};
    }

    public SceneObj nowPos()
    {
        return now.sceneObj;
    }

    public NpcObj GetNpc()
    {
        return (NpcObj)now.ecState.belong.GetObj();
    }

    public void addMoney(Float money)
    {
        throw new System.NotImplementedException();
    }

    public void reduceMoney(Float money)
    {
        throw new System.NotImplementedException();
    }

    public override string ShowString()
    {
        throw new System.NotImplementedException();
    }
}
