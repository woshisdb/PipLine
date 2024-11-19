using System.Collections;
using System.Collections.Generic;
using RuntimeInspectorNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

public class WorldView : MonoBehaviour
{
    public List<List<SceneView>> sceneViews;
    public RuntimeInspector inspector;
    public GameObject scenePrefab;
    public Transform root;
    public void Start()
    {
    }
    /// <summary>
    /// 绑定一个场景对象
    /// </summary>
    /// <param name="mapSystem"></param>
    public void Bind(MapSystem mapSystem)
    {
        sceneViews = new List<List<SceneView>>();
        ///遍历每一个场景对象
        for(int i = 0; i < mapSystem.scenes.Count; i++)
        {
            var sceneObjs = mapSystem.scenes[i];
            sceneViews.Add(new List<SceneView>());
            for(int j = 0; j < sceneObjs.Count; j++)
            {
                AddSceneView(sceneObjs[j], i,j);
            }
        }
    }
    public void AddSceneView(SceneObj scene,int x,int y)
    {
        var sv=GameObject.Instantiate(scenePrefab);
        sv.transform.parent = root;
        sv.transform.localPosition = new Vector3(x*10,0,y*10);
        sceneViews[x].Add( sv.GetComponent<SceneView>() );
        sv.GetComponent<SceneView>().Bind(scene);
    }
    
}
