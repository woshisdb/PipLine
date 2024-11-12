using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatInputItemUI : TextInputItemUI<TMP_InputField>
{

}
public class FloatInputItem:TextInputItem
{
    public FloatInputItem(string text, Func<object> get, Action<object> set) : base(text, get, set)
    {
    }

    public float Val
    {
        get { return (float)GetValue(); }
        set { SetValue(value); }
    }
}