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

public class GameLogic : MonoBehaviour,ISendEvent,IRegisterEvent
{
    //public static OptionUIEnum optionUIEnum;
    public static bool isCoding=false;
    //Ïà»ú
    public Camera mainCamera;
    protected TaskCompletionSource<bool> tcs;
    public static bool hasTime;
    public GameObject scenes;

    public void Start()
    {
        var t=GameArchitect.Interface;
        this.Register<EndUpdateEvent>(this,
            (e)=> {
                GameCircle();
            }
        );
        this.SendEvent(new EndUpdateEvent());
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
        foreach (var npc in GameArchitect.get.npcs)
        {
            Debug.Log(npc.name);
            npc.lifeStyle.job.SetDayJob();//设置一个人活动
			yield return npc.befAct.Run();//执行
		}
    }
    /// <summary>
    /// 更新个人一天结束的行为
    /// </summary>
    /// <returns></returns>
    public IEnumerator NPCEndCircle()
    {
        foreach (var npc in GameArchitect.get.npcs)
		{
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
        foreach(var building in GameArchitect.get.buildings)
        {
			yield return building.LaterUpdate();
		}
    }
    /// <summary>
    /// 对环境的更新
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnvirUpdate()
    {
        //时间加1
        GameArchitect.get.saveData.timeSystem.time++;
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
    public IEnumerator IterCircle()
    {
        yield return NPCBefCircle();//NPC开始的行为
        yield return null;
		yield return BuildingCircle();//建筑自己的循环
		yield return null;
		yield return NPCEndCircle();//更新NPC的结束循环
		yield return null;
        yield return EnvirUpdate();//对环境的更新
        yield return null;
        this.SendEvent<EndUpdateEvent>(new EndUpdateEvent());
        yield return null;
    }
    public void GameCircle()
    {
        Debug.Log("A1");
        StartCoroutine(IterCircle());
        Debug.Log("A2");
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
