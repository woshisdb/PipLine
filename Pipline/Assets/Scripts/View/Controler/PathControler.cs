using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathControler : MonoBehaviour, IRegisterEvent,IPutInPool<PathObj>
{
    public TextMeshPro title;
    public TextMeshPro content;
    public PathObj pathObj;
    public void Allocate()
    {
        gameObject.SetActive(true);
    }

    public void Init(PathObj pathObj)
    {
        this.pathObj = pathObj;
        this.Register<UpdateBuildingEvent>(OnUpdateBuilding);
        OnUpdateBuilding(new UpdateBuildingEvent());
    }

    public void Recycle()
    {
        transform.parent = GameArchitect.get.buildingPoolT;
        gameObject.SetActive(false);
        this.Unregister<UpdateBuildingEvent>(OnUpdateBuilding);
    }
    private void OnUpdateBuilding(UpdateBuildingEvent exampleEvent)
    {
        this.title.text = pathObj.path.from.sceneName+"->"+ pathObj.path.to.sceneName;
        this.content.text = pathObj.path.wastTime+"";
    }
}