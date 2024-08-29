using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using PimDeWitte.UnityMainThreadDispatcher;
using QFramework;
using Sirenix.OdinInspector;
using System.Text.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;
/// <summary>
/// 更新环境
/// </summary>
public class EnvirUpdateEvent
{

}
/// <summary>
/// 开始选择活动
/// </summary>
public class BeginSelectEvent
{

}
public class EndSelectEvent
{
}
/// <summary>
/// 描述今天的工作
/// </summary>
public class CodeSystemData
{
    /// <summary>
    /// 每一天的工作
    /// </summary>
    public int nowTime;
    public string name;
    /// <summary>
    /// 
    /// </summary>
    //public List<List<CodeData>> work;
    ///// <summary>
    ///// 获取当前的工作列表
    ///// </summary>
    ///// <returns></returns>
    //public List<CodeData> GetNowWorks()
    //{
    //    return work[nowTime];
    //}
    //public void NextDay()
    //{
    //    nowTime=(nowTime+1)%work.Count;
    //}
    //public CodeSystemData()
    //{
    //    work= new List<List<CodeData>>();
    //    nowTime=0;
    //}
    //public virtual IEnumerator EditCodeSystem(PersonObj PersonObj, Obj obj,CodeData codeData)
    //{
    //    List<Activity> activities = obj.InitActivities();
    //    List<CardInf> sels = new List<CardInf>();
    //    Activity activity = null;
    //    foreach (var x in activities)
    //    {
    //        var data = x;
    //        sels.Add(new CardInf("选择:" + data.activityName, data.detail,
    //            () =>
    //            {
    //                activity = data;
    //            }
    //        ));
    //    }

    //    yield return GameArchitect.gameLogic.AddDecision(PersonObj, new DecisionTex(
    //    "工作的内容", "选择工作的内容进行工作", sels));
    //    foreach (var x in work)
    //    {
    //        x.obj = obj;
    //    }
    //    /////////////////////////////////////////////////////////////////
    //    yield return GetActDec(codeData, obj, activity);
    //}
    //public IEnumerator GetActDec(CodeData codeData, Obj obj,Activity activity)
    //{
    //    var w= codeData;
    //    PersonObj tempPersonObj = new PersonObj();//自建PersonObj
    //    BuildingObj buildingObj = new BuildingObj();
    //    w.activity = activity;
    //    List<WinData> winDatas = new List<WinData>();
    //    var eff = activity.Effect(obj, tempPersonObj, winDatas);
    //    tempPersonObj.SetAct(eff);
    //    tempPersonObj.isPlayer = true;
    //    Debug.Log(eff.GetType().Name);
    //    bool hasTime = GameLogic.hasTime;
    //    while (tempPersonObj.hasSelect == true)
    //    {
    //        GameLogic.hasTime = true;
    //        yield return tempPersonObj.act.Run(
    //            (result) => {
    //                if (result is EndAct)
    //                    tempPersonObj.RemoveAct();
    //                else if (result is Act)
    //                    tempPersonObj.SetAct((Act)result);
    //            }
    //        );
    //    }
    //    GameLogic.hasTime = hasTime;
    //    ///选择一系列活动
    //    w.wins = winDatas;
    //}
}
//public class CodeSystemDataWeek: CodeSystemData
//{
//    public CodeSystemDataWeek():base()
//    {
//        code = CodeSystemEnum.week;
//        week = new List<int>(7);
//        for(int i = 0; i < 7; i++)
//        { week.Add(0); }
//    }
//    public override IEnumerator EditCodeSystem(PersonObj PersonObj,Obj obj, CodeData codeData)
//    {
//        List<Activity> activities = obj.InitActivities();
//        List<CardInf> sels = new List<CardInf>();
//        Activity activity = null;
//        foreach(var x in activities)
//        {
//            var data = x;
//            sels.Add(new CardInf("选择:"+data.activityName,data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj,new DecisionTex(
//        "工作的内容","选择工作的内容进行工作",sels));
//        foreach(var x in work)
//        {
//            x.obj = obj;
//        }
//        /////////////////////////////////////////////////////////////////
//        yield return GetActDec(codeData, obj,activity);
//    }
//}

//public class CodeSystemDataMove : CodeSystemData
//{
//    public CodeSystemDataMove() : base()
//    {
//        code = CodeSystemEnum.week;
//        week = new List<int>(7);
//        for (int i = 0; i < 7; i++)
//        { week.Add(0); }
//    }
//    public override IEnumerator EditCodeSystem(PersonObj PersonObj, Obj obj, CodeData codeData)
//    {
//        List<Activity> activities = obj.InitActivities();
//        List<CardInf> sels = new List<CardInf>();
//        Activity activity = null;
//        foreach (var x in activities)
//        {
//            var data = x;
//            sels.Add(new CardInf("选择:" + data.activityName, data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj, new DecisionTex(
//        "工作的内容", "选择工作的内容进行工作", sels));
//        foreach (var x in work)
//        {
//            x.obj = obj;
//        }
//        var w = work.Find((x) => { return x.codeName == "搬运"; });
//        PersonObj tempPersonObj = new PersonObj();//自建PersonObj
//        BuildingObj buildingObj = new BuildingObj();
//        w.activity = activity;
//        List<WinData> winDatas = new List<WinData>();
//        var eff = activity.Effect(obj, tempPersonObj, winDatas);
//        tempPersonObj.SetAct(eff);
//        tempPersonObj.isPlayer = true;
//        Debug.Log(eff.GetType().Name);
//        bool hasTime = GameLogic.hasTime;
//        while (tempPersonObj.hasSelect == true)
//        {
//            GameLogic.hasTime = true;
//            yield return tempPersonObj.act.Run(
//                (result) => {
//                    if (result is EndAct)
//                        tempPersonObj.RemoveAct();
//                    else if (result is Act)
//                        tempPersonObj.SetAct((Act)result);
//                }
//            );
//        }
//        GameLogic.hasTime = hasTime;
//        ///选择一系列活动
//        w.wins = winDatas;
//    }
//}



/// <summary>
/// 选择行为
/// </summary>
public class WinData
{
    
}

public class SelData:WinData
{
    public List<System.Tuple<string, int>> selects;//选择的行为
    public SelData()
    {
        selects = new List<System.Tuple<string, int>>();
    }
}

public class DecData : WinData
{
    public string selc;
}

//public struct BeginActEvent
//{
//    public PersonObj PersonObj;
//    public BeginActEvent(PersonObj PersonObj)
//    {
//        this.PersonObj = PersonObj;
//    }
//}
//public struct EndActEvent
//{
//    public PersonObj PersonObj;
//    public EndActEvent(PersonObj PersonObj)
//    {
//        this.PersonObj = PersonObj;
//    }
//}
//public class SelectCardEvent
//{
//    public CardInf card;
//    public PersonObj PersonObj;
//    public SelectCardEvent(CardInf card, PersonObj PersonObj)
//    {
//        this.card = card;
//        this.PersonObj = PersonObj;
//    }
//}
public class GameLogic : MonoBehaviour,ICanRegisterEvent
{
    //public static OptionUIEnum optionUIEnum;
    public static bool isCoding=false;
    //相机
    public Camera camera;
    //public static WindowManager windowsmanager;
    public static List<UnityAction> unityActions;
    protected TaskCompletionSource<bool> tcs;
    public static bool hasTime;
    //public CodeControler codeControler;
    // Start is called before the first frame update
    public IEnumerator WindowSwitch()
    {
        while (true)
        {
            if (unityActions.Count > 0)
            {
                unityActions[0].Invoke();
                unityActions.RemoveAt(0);
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitect.get;
    }

}
