using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关于每天提供商品的协议,流水线工厂
/// </summary>
public class EvenyDayGooodsContract : EveryDayContract
{
    /// <summary>
    /// 商品的Enum
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// 剩余的天数
    /// </summary>
    public int remainTime;
    /// <summary>
    /// 给的钱
    /// </summary>
    public Float givenMoney;
}

/// <summary>
/// 关于一次性交付的协议,例如用户买房子,车
/// </summary>
public class OnceDayGooodsContract : OnceDayContract
{
    /// <summary>
    /// 商品的Enum
    /// </summary>
    public GoodsEnum goods;
    /// <summary>
    /// 给的钱
    /// </summary>
    public Float givenMoney;
}

/// <summary>
/// 关于生产力的协议,每天B去A做什么事情
/// </summary>
public class WorkContract : EveryDayContract,ICanRaiseMoney
{
    /// <summary>
    /// 每天的收入
    /// </summary>
    public Float money;
    public int wastTime = 8;
    public Tuple<ProdEnum, Int>[] prods;
    public ISendWorkContract aim;
    public ICanReceiveWorkContract p;
    public int wasterEnerge;
    /// <summary>
    /// 转移钱的协议
    /// </summary>
    public int ContractMode;
    public override void ABreak()
    {
    }
    public override void BBreak()
    {
    }
    /// <summary>
    /// 是否能执行这个行为
    /// </summary>
    /// <returns></returns>
    public override bool Condition()
    {
        ///根据通勤时间判断到达位置的最小花费
        int money1 = GameArchitect.Instance.mapSystem.WasterMoney(p.nowPos(), aim.aimPos(), npcState.prodState.goWorkTime);//花费时间的情况下是否能到
        int money2 = GameArchitect.Instance.mapSystem.WasterMoney(aim.aimPos(), p.nowPos(), npcState.prodState.goWorkTime);//花费时间的情况下是否能到
        if (money1 + money2 >= money)//如果是这样的话代表赚不到钱
        {
            return false;
        }
        else
            return true;
    }
    /// <summary>
    /// 每天的更新,钱转过去,然后
    /// </summary>
    public override void DayUpdate()
    {
        EconmicSystem.get.GiveMoney(buildingObj, npcState, money);//转移金钱
        npc.prodState.remainTime -= wastTime;
        buildingObj.AddProds(prods, buildingObj.now);
        ///减去剩余的能量
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
    /// 预测当前的签订的收入
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public float Predicate(NpcObj npc)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 拥有生产资料的协议
/// </summary>
public class IOwnerEmploymentFactoryContract : OnceDayContract,ICanRaiseMoney
{
    /// <summary>
    /// 工厂的对象
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
/// 转移商品的协议
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
