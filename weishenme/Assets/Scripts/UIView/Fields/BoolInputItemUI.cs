using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolInputItemUI : TextInputItemUI<Toggle>
{

}
public class BoolInputItem : TextInputItem
{
    public BoolInputItem(string text, Func<object> get, Action<object> set) : base(text, get, set)
    {
    }

    public bool Val { 
        get { return (bool)GetValue(); }
        set { SetValue(value); } 
    }
}