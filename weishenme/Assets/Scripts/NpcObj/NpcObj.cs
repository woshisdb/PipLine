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
    /// <summary>
    /// �Լ��ļ�
    /// </summary>
    public HouseBuildingState homeState;
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
            ret+=building.Rate();//��ȡ��ǰÿ���������ϵ�������
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

public class NpcObj : BaseObj
{
    public NpcState now { get { return (NpcState)getNow(); } }
    /// <summary>
    /// ����˼�����յ�Э����Щ����ִ��
    /// </summary>
    public void ReThinkContract(NpcState npcState)
    {

    }

    /// <summary>
    /// ���ݵ�ǰ����ȥ����һ���������
    /// </summary>
    public void Plan(NpcState npcState)
    {

    }

    /// <summary>
    /// ���ڹ̶�����������и���,������������һ������
    /// </summary>
    public void BefFixedSalaryUpdate()
    {
        if(now.ecState.contracts.hasWorkContract)//���ӵ�й���
        {

        }
        else//�����ӵ�й�����Ѱ�ҹ���
        {

        }
    }
    /// <summary>
    /// ��Ӫ���õĸ���,����ӵ�еĸ�����������һ���˾�������
    /// </summary>
    public void BefSelfEmployment()
    {
        var works = now.ecState;
        if(works.needWork)//�����Ҫ���������������ص�����
        {
            if()//û�й���,�Ǿ�ȥ��
            {

            }
            else
            {

            }
        }
        else
        {
            //����м�ӹ��̵�����
            foreach (var factory in works.EmploymentFactories)
            {
                ///�������
                factory.MaxMoney();//ȷ������
            }
            ///�����Դ���ڵ�����
            foreach (var factory in works.SourceEmploymentFactories)
            {
                ///�������
                factory.MaxMoney();//ȷ������
            }
            ///���ת����Ʒ������
            foreach (var factory in works.transGoodsFactories)
            {
                ///�������
                factory.MaxMoney();
            }
        }
    }
    /// <summary>
    /// ����ӵ�е���������,���˵Ĺ����������Ӱ��Ƚϴ�
    /// </summary>
    public void BefSelfAndOtherEmployment()
    {
        var works = now.ecState;
        //����м�ӹ��̵�����
        foreach (var factory in works.EmploymentFactories)
        {
            ///�������
            factory.MaxMoney();
        }
        ///�����Դ���ڵ�����
        foreach (var factory in works.SourceEmploymentFactories)
        {
            ///�������
            factory.MaxMoney();
        }
        ///���ת����Ʒ������
        foreach (var factory in works.transGoodsFactories)
        {
            ///�������
            factory.MaxMoney();
        }
    }
    /// <summary>
    /// �Լ������ʱ�����ĸ���,�Լ��Ĺ����������ing�벻��
    /// </summary>
    public void BefCapitalGains()
    {
        var works = now.ecState;
        //����м�ӹ��̵�����
        foreach (var factory in works.EmploymentFactories)
        {
            ///�������
            factory.MaxMoney();
        }
        ///�����Դ���ڵ�����
        foreach (var factory in works.SourceEmploymentFactories)
        {
            ///�������
            factory.MaxMoney();
        }
        ///���ת����Ʒ������
        foreach (var factory in works.transGoodsFactories)
        {
            ///�������
            factory.MaxMoney();
        }
    }

    /// <summary>
    /// ����ÿһ���������ϵ�����,��������npc.����contract
    /// </summary>
    /// <param name="input"></param>
    public override void Update(BaseState input)
    {
        //����ÿһ��Э��
        
    }
    public override void Predict(BaseState input, int day)
    {

    }
    public void addMoney(BaseState state, Float money)
    {
        ((NpcState)state).ecState.money += money;
    }
    public void receiveMoney(Float money, ProdGoodsInf goodsInf)
    {
        this.now.ecState.money += money;
    }
    public void reduceMoney(BaseState state, Float money)
    {
        ((NpcState)state).ecState.money -= money;
    }

    public void registerReveiveProdsOrder()
    {

    }

    public void unregisterReveiveProdsOrder()
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
}
