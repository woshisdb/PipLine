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
    public NpcObj npcObj;
    /// <summary>
    /// �Լ������ĵط�
    /// </summary>
    public HouseBuildingObj livePlace;
    public PhysicalState(NpcObj obj)
    {
        this.npcObj = obj;
    }
}

public class EcState
{
    /// <summary>
    /// ��Ҫ��һϵ����Ʒ
    /// </summary>
    public List<NeedGoods> needGoods;
    public NeedWork needWork;
    /// <summary>
    /// �Ƿ���Ҫ����
    /// </summary>
    public bool needWorkB;
    /// <summary>
    /// ��Ǯ
    /// </summary>
    public Float money;
    public NpcObj npcObj;
    public EcState(NpcObj npcObj)
    {
        this.npcObj = npcObj;
        money=new Float(0);
        needGoods=new List<NeedGoods>();
        var needGood = new NeedGoods(npcObj);
        needGoods.Add(needGood);
    }
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
    public NpcObj npcObj;
    public ProdState(NpcObj npc)
    {
        this.npcObj = npc;
    }
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

    public NpcState(BaseObj obj):base(obj)
    {
        ecState = new EcState((NpcObj)obj);
        prodState = new ProdState((NpcObj)obj);
        physicalState = new PhysicalState((NpcObj)obj);
    }
}
public class NpcEc : EconomicInf
{
    public NpcEc(BaseObj obj) : base(obj)
    {
    }
}

public class NpcObj : BaseObj,INpc
{
    public NeedWork needWork { get { return now.ecState.needWork; } }
    public NpcState now { get { return (NpcState)getNow(); } }
    public void Init(SceneObj sceneObj)
    {
        now.sceneObj = sceneObj;
        ///ע��һϵ�еĶ���
        var goods=RegisterNeedGoods();
        if(goods!=null)
        {
            foreach(var need in goods)
            {
                Market.Instance.Register(need);
            }
        }
        now.ecState.needWork = new NeedWork();
        now.ecState.needWork.obj = this;
        now.ecState.needWork.prodEnum = ProdEnum.prod1;
        Market.Instance.Register(RegisterNeedWork()[0] );
    }
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
        //var works = RegisterNeedWork();
        //foreach (var work in works)
        //{
        //    Market.Instance.Register(work);
        //}
    }
    public override void Predict(BaseState input, int day)
    {

    }

    public override void InitBaseState()
    {
        state=new NpcState(this);
    }

    public override void InitEconomicInf()
    {
        ecInf=new NpcEc(this);
    }

    public NeedWork[] RegisterNeedWork()
    {
        return new NeedWork[] { needWork };
    }

    public NeedWork[] UnRegisterNeedWork()
    {
        return new NeedWork[] { needWork};
    }

    public SceneObj nowPos()
    {
        return now.sceneObj;
    }

    public NpcObj GetNpc()
    {
        return this;
    }

    public void addMoney(Float money)
    {
        now.ecState.money.value += money;
    }

    public void reduceMoney(Float money)
    {
        now.ecState.money.value -= money;
    }

    public override string ShowString()
    {
        return "";
    }

    public float GetNeedWorkRate(SendWork sendWork)
    {
        throw new System.NotImplementedException();
    }


    public NeedGoods[] RegisterNeedGoods()
    {
        return now.ecState.needGoods.ToArray();
    }

    public NeedGoods[] UnRegisterNeedGoods()
    {
        return now.ecState.needGoods.ToArray();
    }

    public SceneObj aimPos()
    {
        return now.sceneObj;
    }

    public float NeedGoodsSatifyRate(SendGoods sendGoods)
    {
        return 0.5f;
    }

    public Float getMoney()
    {
        return now.ecState.money;
    }

    public void GetGoodsProcess(GoodsEnum goodsEnum, int sum)
    {

    }

    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }
    //λ��Ϊ��ס��λ��
    public Vector2Int GetWorldPos()
    {
        return now.physicalState.livePlace.GetWorldPos();
    }

    public SceneObj GetSceneObj()
    {
        return now.sceneObj;
    }
}
