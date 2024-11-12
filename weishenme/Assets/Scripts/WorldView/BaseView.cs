using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour, ISendEvent
{
    public BaseObj obj;
    /// <summary>
    /// 当选择这个对象之后需要做什么
    /// </summary>
    public abstract (StringInputItem, TextInputItem[])? SelectIt();
    public abstract void Bind(BaseObj baseObj);
}
