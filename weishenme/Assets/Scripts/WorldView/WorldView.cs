using System.Collections;
using System.Collections.Generic;
using RuntimeInspectorNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

public class WorldView : MonoBehaviour
{
    public List<SceneView> sceneViews;
    public RuntimeInspector inspector;
    public void Start()
    {
        var t=new BuildingObj();
        inspector.Inspect(t);
    }
}
