using System.Collections;
using System.Collections.Generic;
using RuntimeInspectorNamespace;
using UnityEngine;
using UnityEngine.UI;

public struct SelectViewEvent:IEvent
{
    public BaseView view;
    public SelectViewEvent(BaseView baseView)
    {
        view = baseView;
    }
}

public class DetailUI : MonoBehaviour,IRegisterEvent
{
    public RuntimeInspector inspector;
    public void Start()
    {
        this.Register<SelectViewEvent>(e =>
        {
            inspector.Inspect(e.view);
        });
    }
}
