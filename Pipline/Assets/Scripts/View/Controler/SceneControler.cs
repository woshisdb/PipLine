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

    public void ClearBuildings()
    {
        foreach (BuildingControl c in buildingControls)//���պ��������н���
        {
            GameArchitect.get.buildingPool.Recycle(c);
        }
        buildingControls.Clear();
    }
    public void AddBuildings()
    {
        for(int i=0;i<sceneObj.buildings.Count;i++)
        {
            var x = sceneObj.buildings[i];
            var b = GameArchitect.get.buildingPool.Allocate(x);
            b.transform.SetParent(buildings);//�����ӽڵ�
            buildingControls.Add(b);
            b.transform.localPosition = new Vector3(dx*(i%sum),-dy*(i/sum),0);
        }
    }
    public void Recycle()
    {
        gameObject.SetActive(false);
        this.Unregister<UpdateSceneEvent>(sceneObj, OnUpdateScene);
        ClearBuildings();
    }
    public void OnUpdateScene(UpdateSceneEvent exampleEvent)
    {
        this.tableNameUI.text = sceneObj.sceneName;
        ClearBuildings();
        AddBuildings();
    }
}
