using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QFramework;
using UnityEngine;


public class GameArchitect : Architecture<GameArchitect>
{
    public static GameArchitect get
    {
        get { return (GameArchitect)GameArchitect.Interface; }

    }
    public GameLogic gameLogic;
    public SaveData saveData;
    public TimeSystem timeSystem { get { return saveData.timeSystem; } }
    public List<BaseObj> objs { get { return GameArchitect.get.saveData.objs; } }
    public List<LandCellObj> landCells { get { return GameArchitect.get.saveData.landCells; } }
    public static List<NpcObj> npcs { get { return GameArchitect.get.saveData.npcs; } }
    //public static Dictionary<string, Activity> actDic;
    //public void SetPlayer(PersonObj PersonObj)
    //{
    //    Debug.Log(PersonObj);
    //    //Debug.Log("?????????");
    //    this.player = PersonObj;
    //    if (GameArchitect.get.player != null)
    //    {
    //        gameLogic.camera.transform.position = GameArchitect.get.player.belong.control.CenterPos();
    //    }
    //}
    protected override void Init()
    {
        //winCons = new List<WinCon>();
        //this.objAsset = Resources.Load<ObjAsset>("ObjAsset");
        //var tableRess = Resources.Load<TableAsset>("Table/TableAsset");
        //this.tableAsset = tableRess;
        //tableAsset.tableSaver = new TableSaver();
        ///****************初始化对象*********************/
        //MapInit();
        //GameArchitect.get.InitActivities();
        ///*********************************************/
        ////InitActivities();
        SaveSystem.Instance.Load();//加载数据
        ////初始化一系列
        ////初始化PDDL类

        //objAsset.map.Init();
        ////for(int i=0;i<PersonObjs.Count;i++)
        ////{
        ////    PersonObjs[i].contractManager = new ContractManager(PersonObjs[i]);
        ////}
        //GameArchitect.gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        //this.RegisterModel<TableModelSet>(new TableModelSet(tableAsset));
        //this.RegisterModel<PersonObjsOptionModel>(new PersonObjsOptionModel(PersonObjs));
        //this.RegisterModel<ThinkModelSet>(new ThinkModelSet());
        //this.RegisterModel<TimeModel>(new TimeModel());
        //this.RegisterModel<EcModel>(new EcModel());
        if (SaveSystem.Instance.firstInit)
        {
            FirstInit();
        }
        //SetPlayer( PersonObjs.Find(e => { return e.isPlayer; }));
        //var cM = tableAsset.tableSaver.contractModel;
        //if (cM == null)
        //{
        //    cM = new ContractModel();
        //    tableAsset.tableSaver.contractModel = cM;
        //}
        //else
        //{
        //    foreach (var PersonObj in GameArchitect.PersonObjs)
        //    {
        //        if (!cM.aContract.ContainsKey(PersonObj))
        //        {
        //            cM.aContract.Add(PersonObj, new List<Contract>());
        //        }
        //    }
        //    foreach (var PersonObj in GameArchitect.PersonObjs)
        //    {
        //        if (!cM.bContract.ContainsKey(PersonObj))
        //        {
        //            cM.bContract.Add(PersonObj, new List<Contract>());
        //        }
        //    }
        //}
        //this.RegisterModel<ContractModel>(cM);
    }
    public void FirstInit()
    {
        //Debug.Log(111111211111);
        //tableAsset.CreateTable("TestTable", 100000);
        //Debug.Log(11131111);
        //var PersonObj = new PersonObj(Map.Instance.GetSaver(ObjEnum.PersonObjE));
        //GameArchitect.gameLogic.CreatePersonObj(true, "PersonObj", true, "TestTable");
        //Debug.Log(111111111111);
    }
    public void MapInit()
    {
        //Map.Instance.Init();
    }
    //public IEnumerator AddDecision(WinCon winCon)
    //{
    //    //Debug.Log(winCons.Count);
    //    //winCons.Add(winCon);
    //    //while (winCons.Count > 0)
    //    //{
    //    //    yield return CallDecision();
    //    //}
    //    //yield return winCon.data;
    //}
    //public IEnumerator CallDecision()
    //{
    //    //Debug.Log(":"+winCons.Count);
    //    //decisionUI.gameObject.SetActive(false);
    //    //selectUI.gameObject.SetActive(false);
    //    //Debug.Log(">>>>" + winCons.Count);
    //    //if (winCons.Count>0)
    //    //{
    //    //    var data = winCons[0];
    //    //    winCons.RemoveAt(0);
    //    //    Debug.Log(">>>>" + winCons.Count);
    //    //    if (data is DecisionTex)
    //    //    {
    //    //        decisionUI.gameObject.SetActive(true);
    //    //        yield return decisionUI.AddDecision((DecisionTex)data);
    //    //    }
    //    //    else if(data is SelectTex)
    //    //    {
    //    //        selectUI.gameObject.SetActive(true);
    //    //        yield return selectUI.AddDecision((SelectTex)data);
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    decisionUI.gameObject.SetActive(false);
    //    //    selectUI.gameObject.SetActive(false);
    //    //}
    //}
    /// <summary>
    /// 创建一系列的活动
    /// </summary>
    public void InitActivities()
    {
        //activities = new Dictionary<Type, List<Activity>>();
        //foreach (Type key in ((Map)Map.Instance).kv.Keys)
        //{
        //    Obj instance = (Obj)Activator.CreateInstance(key, new object[] { null });
        //    activities.Add(key, instance.InitActivities());
        //}
    }
}