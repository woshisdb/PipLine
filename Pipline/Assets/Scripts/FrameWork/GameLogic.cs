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

public class GameLogic : MonoBehaviour,ICanRegisterEvent
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
        NPCBefCircle();//NPC开始的行为
        yield return null;
        BuildingCircle();//建筑自己的循环
        yield return null;
        NPCEndCircle();//更新NPC的结束循环
        yield return null;
        EnvirUpdate();//对环境的更新
        yield return null;
    }
    public void GameCircle()
    {
        StartCoroutine(IterCircle());
    }
}
