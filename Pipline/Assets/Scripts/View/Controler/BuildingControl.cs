using System.Collections;
using System.Collections.Generic;
using QFramework;
using TMPro;
using UnityEngine;

public interface IPutInPool<T>
{
    public void Allocate();
    public void Recycle();
    public void Init(T t);
}
public struct UpdateBuildingEvent:IEvent
{

}
public class BuildingControl : MonoBehaviour,IRegisterEvent, IPutInPool<BuildingObj>
{
    public TextMeshPro title;
    public TextMeshPro content;
    public BuildingObj buildingObj;

    public void Allocate()
    {
        gameObject.SetActive(true);
    }

    public void Init(BuildingObj buildingObj)
    {
        this.buildingObj = buildingObj;
        this.Register<UpdateBuildingEvent>(buildingObj,OnUpdateBuilding);
        OnUpdateBuilding(new UpdateBuildingEvent());
    }

    public void Recycle()
    {
        transform.parent = GameArchitect.get.buildingPoolT;
        gameObject.SetActive(false);
        this.Unregister<UpdateBuildingEvent>(buildingObj,OnUpdateBuilding);
    }
    private void OnUpdateBuilding(UpdateBuildingEvent exampleEvent)
    {
        this.title.text = buildingObj.name;
        this.content.text = buildingObj.GetContent();
    }

}
