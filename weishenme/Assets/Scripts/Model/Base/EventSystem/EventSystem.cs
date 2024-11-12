using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISendEvent { }

public interface IRegisterEvent
{
}

public interface IEvent 
{

}



public class EventManager: Singleton<EventManager>
{
    // 全局事件监听器字典
    private Dictionary<Type, Action<IEvent>> globalEventDictionary;

    // 特定发送者的事件监听器字典
    private Dictionary<ISendEvent, Dictionary<Type, Action<IEvent>>> specificEventDictionary;

    protected EventManager():base()
    {
        globalEventDictionary = new Dictionary<Type, Action<IEvent>>();
        specificEventDictionary = new Dictionary<ISendEvent, Dictionary<Type, Action<IEvent>>>();
    }

    // 注册全局事件监听器
    public void Register<T>(Action<T> listener) where T : struct, IEvent
    {
        var eventType = typeof(T);
        if (globalEventDictionary.TryGetValue(eventType, out Action<IEvent> thisEvent))
        {
            thisEvent += (e) => listener((T)e);
            globalEventDictionary[eventType] = thisEvent;
        }
        else
        {
            thisEvent += (e) => listener((T)e);
            globalEventDictionary.Add(eventType, thisEvent);
        }
    }

    // 注册特定发送者的事件监听器
    public void Register<T>(ISendEvent sender, Action<T> listener) where T : struct, IEvent
    {
        var eventType = typeof(T);
        if (!specificEventDictionary.ContainsKey(sender))
        {
            specificEventDictionary[sender] = new Dictionary<Type, Action<IEvent>>();
        }

        var senderDictionary = specificEventDictionary[sender];
        if (senderDictionary.TryGetValue(eventType, out Action<IEvent> thisEvent))
        {
            thisEvent += (e) => listener((T)e);
            senderDictionary[eventType] = thisEvent;
        }
        else
        {
            thisEvent += (e) => listener((T)e);
            senderDictionary.Add(eventType, thisEvent);
        }
    }

    // 取消全局事件监听器
    public void Unregister<T>(Action<T> listener) where T : struct, IEvent
    {
        var eventType = typeof(T);
        if (globalEventDictionary.TryGetValue(eventType, out Action<IEvent> thisEvent))
        {
            thisEvent -= (e) => listener((T)e);
            globalEventDictionary[eventType] = thisEvent;
        }
    }

    // 取消特定发送者的事件监听器
    public void Unregister<T>(ISendEvent sender, Action<T> listener) where T : struct, IEvent
    {
        if (specificEventDictionary.ContainsKey(sender))
        {
            var senderDictionary = specificEventDictionary[sender];
            var eventType = typeof(T);

            if (senderDictionary.TryGetValue(eventType, out Action<IEvent> thisEvent))
            {
                thisEvent -= (e) => listener((T)e);
                senderDictionary[eventType] = thisEvent;
            }
        }
    }

    // 触发事件
    public void TriggerEvent<T>(ISendEvent sender, T eventToSend) where T : struct, IEvent
    {
        var eventType = typeof(T);

        // 触发全局事件
        if (globalEventDictionary.TryGetValue(eventType, out Action<IEvent> thisEvent))
        {
            thisEvent.Invoke(eventToSend);
        }

        // 触发特定发送者的事件
        if (specificEventDictionary.ContainsKey(sender))
        {
            var senderDictionary = specificEventDictionary[sender];
            if (senderDictionary.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.Invoke(eventToSend);
            }
        }
    }
}
public static class EventSenderExtensions
{
    public static void SendEvent<T>(this ISendEvent sender, T eventToSend) where T : struct, IEvent
    {
        EventManager.Instance.TriggerEvent(sender, eventToSend);
    }
    public static void Register<T>(this IRegisterEvent registerer, Action<T> listener) where T : struct, IEvent
    {
        EventManager.Instance.Register(listener);
    }

    public static void Register<T>(this IRegisterEvent registerer, ISendEvent sender, Action<T> listener) where T : struct, IEvent
    {
        EventManager.Instance.Register(sender, listener);
    }

    public static void Unregister<T>(this IRegisterEvent registerer, Action<T> listener) where T : struct, IEvent
    {
        EventManager.Instance.Unregister(listener);
    }

    public static void Unregister<T>(this IRegisterEvent registerer, ISendEvent sender, Action<T> listener) where T : struct, IEvent
    {
        EventManager.Instance.Unregister(sender, listener);
    }
}