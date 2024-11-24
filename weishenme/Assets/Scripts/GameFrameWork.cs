using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameFrameWork : MonoBehaviour,IRegisterEvent
{
    [ShowInInspector,ReadOnly]
    GameArchitect gameArchitect;
    public void Awake()
    {
        gameArchitect = GameArchitect.Instance;
    }
    public void Start()
    {
        //UpdateFrameWork();
        this.Register<SelectViewEvent>(e =>
        {
            Debug.Log(1);
            GameArchitect.Instance.uiManager.inspector.ShowUI(e.view.obj);
        });
    }
    [Button]
    public void UpdateFrameWork()
    {
        Debug.Log(">>>");
        gameArchitect.Update();
    }
}
