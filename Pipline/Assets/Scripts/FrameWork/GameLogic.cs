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
public struct EndUpdateEvent:IEvent
{

}
/// <summary>
/// ¿ªÊ¼Ñ¡Ôñ»î¶¯
/// </summary>
public class BeginSelectEvent
{

}
/// <summary>
/// 不为空
/// </summary>
public struct PassDayEvent:IEvent
{

}

public class GameLogic : MonoBehaviour,ISendEvent,IRegisterEvent
{
    //public static OptionUIEnum optionUIEnum;
    public static bool isCoding=false;
    //Ïà»ú
    public Camera mainCamera;
    //protected TaskCompletionSource<bool> tcs;
    public static bool hasTime;
    public GameObject scenes;

    public void Start()
    {
        TransEnum.Init();
        var t=GameArchitect.Interface;
        this.Register<EndUpdateEvent>(this,
            (e)=> {
                GameCircle();//一个时间步
            }
        );
        this.Register<PassDayEvent>(//跨天活动
            e => {
                PassDay(e);
            }
        );
        this.SendEvent(new EndUpdateEvent());
    }
    public void PassDay(PassDayEvent passDay)
    {

    }
    public IArchitecture GetArchitecture()
	{
        return GameArchitect.get;
	}
    /// <summary>
    /// 获得个人一天结束的行为
    /// </summary>
    /// <returns></returns>
    public IEnumerator NPCBefCircle()
    {
        int n=0;
        var nowT=GameArchitect.get.timeSystem.GetTime();
        foreach (var npc in GameArchitect.get.npcs)
        {
            Debug.Log(n);
            n++;
            if (npc.lifeStyle.job.job.InStartTime(nowT))
            {
                yield return npc.befAct.Run();//执行
            }
		}
    }
    /// <summary>
    /// 更新个人一天结束的行为
    /// </summary>
    /// <returns></returns>
    public IEnumerator NPCEndCircle()
    {
        var nowT = GameArchitect.get.timeSystem.GetTime();
        foreach (var npc in GameArchitect.get.npcs)
		{
            if (npc.lifeStyle.job.job.InEndTime(nowT))
                yield return npc.endAct.Run();
		}
    }
    /// <summary>
    /// 更新建筑的活动
    /// </summary>
    /// <returns></returns>
    public IEnumerator BuildingCircle()
    {
        foreach (var building in GameArchitect.get.buildings)
        {
            yield return building.Update();//更新
        }
    }
    /// <summary>
    /// 对环境的更新
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnvirUpdate()
    {
        //时间加1
        foreach(var b in GameArchitect.get.saveData.buildings)
        {
            b.SendEvent(new UpdateBuildingEvent());
        }
        GameArchitect.get.saveData.timeSystem.time++;
        this.SendEvent(new TimeUpdateEvent());
        if(GameArchitect.get.saveData.timeSystem.GetTime()==0)//跨天了,重新计算今天的活动
        {
            GameArchitect.get.economicSystem.Update();//更新价格列表
            foreach (var npc in GameArchitect.get.npcs)
            {
                npc.lifeStyle.job.SetDayJob();//设置一个人今天的活动
            }
        }
        yield return null;
    }
    /// <summary>
    /// 迭代npc空闲工作
    /// </summary>
    public IEnumerator IterNPCSpareTime()
    {
        foreach(var npc in GameArchitect.get.npcs)
        {
            yield return npc.lifeStyle.timeWork.spareTimeAct.Run();
        }    
        yield return null;
    }
    public IEnumerator PathCircle()
    {
        foreach(var x in GameArchitect.get.scenes)
        {
            x.paths.Update();
        }
        yield return null;
    }
    public IEnumerator IterCircle()
    {
        yield return PathCircle();
        Debug.Log("PathCircle");
        yield return null;
        yield return NPCBefCircle();//NPC开始的行为
        Debug.Log("NPCBefCircle");
        yield return null;
		yield return BuildingCircle();//建筑自己的循环
        Debug.Log("BuildingCircle");
        yield return null;
        yield return NPCEndCircle();//结算收入
        Debug.Log("NPCEndCircle");
        yield return null;
        yield return EnvirUpdate();//对环境的更新
        Debug.Log("EnvirUpdate");
        yield return null;
        this.SendEvent<EndUpdateEvent>(new EndUpdateEvent());//跨时间步
        yield return null;
    }
    public void GameCircle()
    {
        //Debug.Log("A1");
        StartCoroutine(IterCircle());
        //Debug.Log("A2");
    }
    public void Save()
    {
        SaveSystem.Instance.Save();
    }
    public void Load()
    {
        SaveSystem.Instance.Load();
    }
}
