using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ÿ���ṩ��Ʒ��Э��,��ˮ�߹���
/// </summary>
public class EvenyDayGooodsContract : EveryDayContract
{
    /// <summary>
    /// ��Ʒ��Enum
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// ʣ�������
    /// </summary>
    public int remainTime;
    /// <summary>
    /// ����Ǯ
    /// </summary>
    public Float givenMoney;
}

/// <summary>
/// ����һ���Խ�����Э��,�����û�����,��
/// </summary>
public class OnceDayGooodsContract : OnceDayContract
{
    /// <summary>
    /// ��Ʒ��Enum
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// ����Ǯ
    /// </summary>
    public Float givenMoney;
}

/// <summary>
/// ������������Э��,ÿ��BȥA��ʲô����
/// </summary>
public class WorkContract : EveryDayContract,ICanRaiseMoney
{
    /// <summary>
    /// ÿ�������
    /// </summary>
    public Float money;
    public int wastTime = 8;
    public Tuple<ProdEnum, Int>[] prods;
    public ISendWorkContract aim;
    public ICanReceiveWorkContract p;
    public int wasterEnerge;
    /// <summary>
    /// ת��Ǯ��Э��
    /// </summary>
    public int ContractMode;
    public override void ABreak()
    {
    }
    public override void BBreak()
    {
    }
    /// <summary>
    /// �Ƿ���ִ�������Ϊ
    /// </summary>
    /// <returns></returns>
    public override bool Condition()
    {
        ///����ͨ��ʱ���жϵ���λ�õ���С����
        int money1 = GameArchitect.Instance.mapSystem.WasterMoney(p.nowPos(), aim.aimPos(), npcState.prodState.goWorkTime);//����ʱ���������Ƿ��ܵ�
        int money2 = GameArchitect.Instance.mapSystem.WasterMoney(aim.aimPos(), p.nowPos(), npcState.prodState.goWorkTime);//����ʱ���������Ƿ��ܵ�
        if (money1 + money2 >= money)//����������Ļ�����׬����Ǯ
        {
            return false;
        }
        else
            return true;
    }
    /// <summary>
    /// ÿ��ĸ���,Ǯת��ȥ,Ȼ��
    /// </summary>
    public override void DayUpdate()
    {
        EconmicSystem.get.GiveMoney(buildingObj, npcState, money);//ת�ƽ�Ǯ
        npc.prodState.remainTime -= wastTime;
        buildingObj.AddProds(prods, buildingObj.now);
        ///��ȥʣ�������
        npc.physicalState.remainEnerge -= wasterEnerge;
    }
    public override void Init()
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int MoneyCircle()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Ԥ�⵱ǰ��ǩ��������
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public float Predicate(NpcObj npc)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ӵ���������ϵ�Э��
/// </summary>
public class IOwnerEmploymentFactoryContract : OnceDayContract,ICanRaiseMoney
{
    /// <summary>
    /// �����Ķ���
    /// </summary>
    public IBuilding employmentFactory;
    public override bool Condition()
    {
        throw new NotImplementedException();
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ת����Ʒ��Э��
/// </summary>
public class ITransGoodsContract : EveryDayContract
{
    public override void ABreak()
    {
        throw new NotImplementedException();
    }

    public override void BBreak()
    {
        throw new NotImplementedException();
    }

    public override bool Condition()
    {
        throw new NotImplementedException();
    }

    public override void DayUpdate()
    {
        throw new NotImplementedException();
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }
}
