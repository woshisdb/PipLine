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
/// ¸üÐÂ»·¾³
/// </summary>
public class EnvirUpdateEvent
{

}
/// <summary>
/// ¿ªÊ¼Ñ¡Ôñ»î¶¯
/// </summary>
public class BeginSelectEvent
{

}
public class EndSelectEvent
{
}
/// 描述今天的工作
    /// 每一天的工作
    //public List<List<CodeData>> work;
    /// <summary>
    /// 获取当前的工作列表
    /// </summary>
    /// <returns></returns>
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
    //        sels.Add(new CardInf("选择:" + data.activityName, data.detail,

    //    "工作的内容", "选择工作的内容进行工作", sels));
    //    PersonObj tempPersonObj = new PersonObj();//自建PersonObj
    //    ///选择一系列活动
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
//            sels.Add(new CardInf("Ñ¡Ôñ:"+data.activityName,data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj,new DecisionTex(
//        "¹¤×÷µÄÄÚÈÝ","Ñ¡Ôñ¹¤×÷µÄÄÚÈÝ½øÐÐ¹¤×÷",sels));
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
//            sels.Add(new CardInf("Ñ¡Ôñ:" + data.activityName, data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj, new DecisionTex(
//        "¹¤×÷µÄÄÚÈÝ", "Ñ¡Ôñ¹¤×÷µÄÄÚÈÝ½øÐÐ¹¤×÷", sels));
//        foreach (var x in work)
//        {
//            x.obj = obj;
//        }
//        var w = work.Find((x) => { return x.codeName == "°áÔË"; });
//        PersonObj tempPersonObj = new PersonObj();//×Ô½¨PersonObj
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
//        ///Ñ¡ÔñÒ»ÏµÁÐ»î¶¯
//        w.wins = winDatas;
//    }
//}



/// <summary>
/// Ñ¡ÔñÐÐÎª
/// </summary>
public class WinData
{
    
}

public class SelData:WinData
{
    public List<System.Tuple<string, int>> selects;//Ñ¡ÔñµÄÐÐÎª
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
    //Ïà»ú
    public Camera mainCamera;
    protected TaskCompletionSource<bool> tcs;
    public static bool hasTime;
    //public CodeControler codeControler;

	public IArchitecture GetArchitecture()
	{
        return GameArchitect.get;
	}

}
