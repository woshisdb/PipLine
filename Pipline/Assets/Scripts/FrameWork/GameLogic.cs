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
    public void NPCBefCircle()
    {
        foreach (var npc in GameArchitect.get.npcs)
        {
            StartCoroutine( npc.befAct.Run() );
        }
    }
    public void NPCEndCircle()
    {
        foreach (var npc in GameArchitect.get.npcs)
		{
			StartCoroutine(npc.endAct.Run());
		}
	}
    public void BuildingCircle()
    {
        foreach (var building in GameArchitect.get.buildings)
        {
            StartCoroutine(building.Update());//更新
        }
    }
    public void GameCircle()
    {
        NPCBefCircle();
        BuildingCircle();
        NPCEndCircle();
    }
}
