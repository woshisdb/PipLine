using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntInputItemUI : TextInputItemUI<TMP_InputField>
{

}
public class IntInputItem:TextInputItem
{
    public IntInputItem(string text, Func<object> get, Action<object> set) : base(text, get, set)
    {
    }

    public int Val
    {
        get { return (int)GetValue(); }
        set { SetValue(value); }
    }
}