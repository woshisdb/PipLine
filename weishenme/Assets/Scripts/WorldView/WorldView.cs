using System.Collections;
using System.Collections.Generic;
using RuntimeInspectorNamespace;
using UnityEngine;

public class TestIns
{
    public List<SceneView> sceneViews;
    public Vector2 scenePosition;
}

public class WorldView : MonoBehaviour
{
    public List<SceneView> sceneViews;
    public RuntimeInspector inspector;
    public TestIns testIns;
    public void Start()
    {
        inspector.Inspect(this);
    }
}
