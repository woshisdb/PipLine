using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour, ISendEvent
{
    public BaseObj obj;
    /// <summary>
    /// ��ѡ���������֮����Ҫ��ʲô
    /// </summary>
    public abstract (StringInputItem, TextInputItem[])? SelectIt();
    public abstract void Bind(BaseObj baseObj);
}
