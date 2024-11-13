using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour, ISendEvent
{
    public BaseObj obj;
    public void OnMouseDown()
    {
        Debug.Log(1);
        this.SendEvent<SelectViewEvent>(new SelectViewEvent(this));
    }
}
