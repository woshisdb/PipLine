using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInputItemUI:MonoBehaviour
{

}

public class TextInputItemUI<T> : TextInputItemUI
{
    public TMPro.TextMeshProUGUI text;
    public T inputField;//TMP_InputField
    public Button button;
}


public class TextInputItem
{
    public string text;
    public Func<object> GetValue;
    public Action<object> SetValue;
    public TextInputItem(string text,Func<object> get, Action<object> set)
    {
        this.text = text;
        GetValue= get;
        SetValue = set;
    }
}

public class StringInputItem
{
    public string Text;
    public StringInputItem(string text)
    {
        Text = text;
    }
}