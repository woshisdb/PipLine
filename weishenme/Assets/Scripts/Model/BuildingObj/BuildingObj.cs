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
    public Float money;
    public BuildingEnum buildingEnum;
    public BuildingState(BuildingObj buildingObj,BuildingEnum buildingEnum):base(buildingObj)
    {
        this.buildingEnum = buildingEnum;
        //buildingMeta=Meta.Instance.getMeta(buildingEnum);
        money=new Float(100);
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
public class BuildingObj :BaseObj,ISendEvent,ISendCommand,IRegisterEvent, IWorldPosition,IEffectShort
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

    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        ret.Add(new KVItemBinder(() =>
        {
            return "xy";
        },
        () =>
        {
            return "("+x+","+y+")";
        }));
        ret.Add(new KVItemBinder(() =>
        {
            return "scene";
        },() =>
        {
            return "("+scene.now.x + "," + scene.now.y+")";
        }));
        var nowUI=new List<UIItemBinder>()
        {
            new KVItemBinder(()=>{
                return "money";
            },()=>{
                return now.money.Value.ToString();
            }),
        };
        ret.Add(new TableItemBinder(() =>
        {
            return "now";
        }, nowUI));
        return ret;
    }

    public Vector2Int GetWorldPos()
    {
        return new Vector2Int(x, y);
    }

    public SceneObj GetSceneObj()
    {
        return scene;
    }
    public void AddBelong(NpcObj npc)
    {
        now.belong = npc;
        npc.now.needState.shortNeed.ecTimeLine.baseMoneyList.Add(this);
    }
    public void RemoveBelong()
    {
        now.belong.now.needState.shortNeed.ecTimeLine.baseMoneyList.Remove(this);
        now.belong = null;
    }

    public virtual float effect()
    {
        return 1;
    }
}

