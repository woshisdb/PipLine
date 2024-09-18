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
/// <summary>
/// 新的时间步
/// </summary>
public struct NewStepEvent:IEvent
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
    /// <summary>
    /// 跨天GameLogic处理
    /// </summary>
    /// <param name="passDay"></param>
    public void PassDay(PassDayEvent passDay)
    {
        //foreach (var npc in GameArchitect.get.npcs)
        //{
        //    npc.lifeStyle.job.SetDayJob();//设置一个人今天的活动
        //}
        GameArchitect.get.npcManager.jobContainer.Update();
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
        var nowT=GameArchitect.get.timeSystem.GetTime();
        foreach(var job in GameArchitect.get.npcManager.jobContainer.npcContainers)
        {
            yield return job.Key.befAct.Run();
        }
        yield return null;
    }
    /// <summary>
    /// 更新个人一天结束的行为
    /// </summary>
    /// <returns></returns>
    public IEnumerator NPCEndCircle()
    {
        var nowT = GameArchitect.get.timeSystem.GetTime();
        foreach (var job in GameArchitect.get.npcManager.jobContainer.npcContainers)
        {
            if (job.Key.InEndTime(nowT))
                yield return job.Key.endAct.Run();
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
        foreach(var b in GameArchitect.get.saveData.buildings)
        {
            b.SendEvent(new UpdateBuildingEvent());
        }
        //当前时间结束
        this.SendEvent(new EndStepEvent());
        GameArchitect.get.saveData.timeSystem.time++;

        if(GameArchitect.get.saveData.timeSystem.GetTime()==0)//跨天了,重新计算今天的活动
        {
            foreach (var x in GameArchitect.get.npcManager.jobContainer.npcContainers)
            {
                var job = x.Key;
                var b = job.buildingObj;
                int allmoney =job.money*job.npcs.Count;//需要发的工资
                if(allmoney>job.buildingObj.money.money)//不够钱
                {
                    allmoney = job.buildingObj.money.money;
                }
                b.money.money-=allmoney;
                int perMoney = allmoney / job.npcs.Count;
                Task parallelTask1 = Task.Run(() =>
                {
                    Parallel.For(0, job.npcs.Count, i =>
                    {
                        // 假设处理逻辑是将每个元素乘以 2
                        job.npcs[i].money.Add(perMoney);
                    });
                });

            }
            //更新收入情况
            Task parallelTask2 = Task.Run(() =>
            {
                Parallel.For(0, GameArchitect.get.npcs.Count, i =>
                {
                    GameArchitect.get.npcs[i].money.Update();
                });
            });

            // Wait for the parallel task to complete
            while (!parallelTask2.IsCompleted)
            {
                yield return null; // Yield control back to Unity until the task is complete
            }
            this.SendEvent<PassDayEvent>(new PassDayEvent());//跨天事件
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
        ///新的时间步更新
        this.SendEvent<NewStepEvent>(new NewStepEvent());
        yield return PathCircle();
        //Debug.Log("PathCircle");
        yield return null;
        yield return NPCBefCircle();//NPC开始的行为
        //Debug.Log("NPCBefCircle");
        yield return null;
		yield return BuildingCircle();//建筑自己的循环
        //Debug.Log("BuildingCircle");
        yield return null;
        yield return NPCEndCircle();//结算收入
        //Debug.Log("NPCEndCircle");
        yield return null;
        yield return EnvirUpdate();//对环境的更新
        //Debug.Log("EnvirUpdate");
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
