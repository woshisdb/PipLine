using System.Collections;
using System.Collections.Generic;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct UpdateSceneEvent:IEvent
{

}

public class SceneControler : MonoBehaviour,IRegisterEvent,IPutInPool<SceneObj>
{
    public int sum;
    public float dx;
    public float dy;
    public Transform buildings;
    public SceneObj sceneObj;
    public Transform center;
    public TextMeshPro tableNameUI;
    public List<BuildingControl> buildingControls=new List<BuildingControl>();
    public List<PathControler> pathControls=new List<PathControler>();
    int num = 0;
    public IArchitecture GetArchitecture()
    {
        return GameArchitect.get;
    }
    public void Init(SceneObj sceneObj)
    {
        this.sceneObj = sceneObj;
        tableNameUI.text = sceneObj.sceneName;
        this.Register<UpdateSceneEvent>(sceneObj, OnUpdateScene);
    }
    public void ClearPersonObj()
    {
    }
    public void ClearCardSlot()
    {
    }
    public Vector3 CenterPos()
    {
        return center.position;
    }

    public void Allocate()
    {
        gameObject.SetActive(true);
        buildingControls.Clear();
    }

    public void ClearBuildingsAndPath()
    {
        num = 0;
        foreach (BuildingControl c in buildingControls)//回收后重载所有建筑
        {
            GameArchitect.get.buildingPool.Recycle(c);
        }
        buildingControls.Clear();
        foreach (PathControler c in pathControls)
        {
            GameArchitect.get.pathPool.Recycle(c);
        }
        pathControls.Clear();
    }
    public void AddBuildings()
    {
        for(int i=0;i<sceneObj.buildings.Count;i++)
        {
            var x = sceneObj.buildings[i];
            var b = GameArchitect.get.buildingPool.Allocate(x);
            b.transform.SetParent(buildings);//创建子节点
            buildingControls.Add(b);
            b.transform.localPosition = new Vector3(dx*(num%sum),-dy*(num/sum),0);
            num++;
        }
    }
    public void AddPaths()
    {
        foreach (var path in sceneObj.paths)
        {
            var x = sceneObj.paths[ path.Key ];
            var b = GameArchitect.get.pathPool.Allocate(x);
            b.transform.SetParent(buildings);//创建子节点
            pathControls.Add(b);
            b.transform.localPosition = new Vector3(dx * (num % sum), -dy * (num / sum), 0);
            num++;
        }
    }
    public void Recycle()
    {
        gameObject.SetActive(false);
        this.Unregister<UpdateSceneEvent>(sceneObj, OnUpdateScene);
        ClearBuildingsAndPath();
    }
    public void OnUpdateScene(UpdateSceneEvent exampleEvent)
    {
        this.tableNameUI.text = sceneObj.sceneName;
        ClearBuildingsAndPath();
        AddBuildings();
        AddPaths();
    }
}
