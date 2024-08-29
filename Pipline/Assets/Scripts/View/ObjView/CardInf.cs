using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardInf
{
    public string title;
    public string description;
    public UnityAction effect;
    public BindCardInf cardControl;
    /// <summary>
    /// ≥ı º∂‘œÛ
    /// </summary>
    public BaseObj baseObj;
    public CardInf(string title="", string description="", UnityAction action=null)
    {
        this.title = title;
        this.description = description;
        this.effect = action;
        this.cardControl = null;
    }
}

public interface BindCardInf
{
    public void UpdateInf();
}