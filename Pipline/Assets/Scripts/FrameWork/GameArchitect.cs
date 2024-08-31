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
    public List<BuildingObj> buildings { get { return GameArchitect.get.saveData.buildings; } }
    public List<SceneObj> scenes { get { return GameArchitect.get.saveData.scenes; } }
    public List<NpcObj> npcs { get { return GameArchitect.get.saveData.npcs; } }

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
    }
    public void MapInit()
    {
    }
    /// <summary>
    /// 创建一系列的活动
    /// </summary>
    public void InitActivities()
    {
    }
}