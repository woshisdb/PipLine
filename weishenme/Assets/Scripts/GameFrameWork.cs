using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameFrameWork : MonoBehaviour
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
    }
    [Button]
    public void UpdateFrameWork()
    {
        Debug.Log(">>>");
        gameArchitect.Update();
    }
}
