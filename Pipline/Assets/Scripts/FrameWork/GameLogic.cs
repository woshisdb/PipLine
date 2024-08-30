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
    //public CodeControler codeControler;

	public IArchitecture GetArchitecture()
	{
        return GameArchitect.get;
	}
    public IEnumerator NPCBefCircle()
    {
        foreach (var npc in GameArchitect.get.npcs)
        {
            yield return npc.befAct.Run();
        }
    }
    public IEnumerator NPCEndCircle()
    {
        foreach (var npc in GameArchitect.get.npcs)
		{
            yield return npc.endAct.Run();
		}
    }
    public IEnumerator BuildingCircle()
    {
        foreach (var building in GameArchitect.get.buildings)
        {
            yield return building.Update();//更新
        }
    }
    public IEnumerator IterCircle()
    {
        NPCBefCircle();
        yield return null;
        BuildingCircle();
        yield return null;
        NPCEndCircle();
        yield return null;
    }
    public void GameCircle()
    {
        StartCoroutine(IterCircle());
    }
}
