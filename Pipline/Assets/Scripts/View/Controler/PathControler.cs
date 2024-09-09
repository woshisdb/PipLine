using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PathControler : MonoBehaviour, IRegisterEvent,IPutInPool<PathObj>
{
    public TextMeshPro title;
    public TextMeshPro content;
    public PathObj pathObj;
    public StringBuilder sb;
    public void Allocate()
    {
        gameObject.SetActive(true);
    }

    public void Init(PathObj pathObj)
    {
        sb = new StringBuilder();
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
        sb.Clear();
        foreach (var path in pathObj.path)
        {
            sb.AppendLine(path.from.sceneName+"->"+path.to.sceneName);
        }
        this.title.text = pathObj.scene.sceneName;
        this.content.text = sb.ToString();
    }
}
