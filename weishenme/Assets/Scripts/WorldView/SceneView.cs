using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public struct OnAddBuilding:IEvent
{
    public BuildingObj buildingObj;
    public OnAddBuilding(BuildingObj buildingObj)
    {
        this.buildingObj = buildingObj;
    }
}

public struct OnRemoveBuilding:IEvent
{
    public BuildingObj buildingObj;
    public OnRemoveBuilding(BuildingObj buildingObj)
    {
        this.buildingObj = buildingObj;
    }
}

public class SceneView : MonoBehaviour,ISendEvent
{
    public SceneObj sceneObj;
    /// <summary>
    /// 建筑对象
    /// </summary>
    [ShowInInspector]
    public BuildingView[,] buildingViews;
    public Transform root;
    public int sx;
    public int sy;
    public void Awake()
    {
        buildingViews = new BuildingView[sx,sy];
    }
    public void Bind(SceneObj sceneObj)
    {
        this.sceneObj = sceneObj;
        for(int i=0;i<sceneObj.buildings.Count;i++)
        {
            BindBuildingObj(sceneObj.buildings[i]);
        }
        sceneObj.Register<OnAddBuilding>(this,e =>
        {
            Debug.Log("add");
            sceneObj.AddBuilding(e.buildingObj);
        });
        sceneObj.Register<OnRemoveBuilding>(this,e =>
        {
            sceneObj.RemoveBuilding(e.buildingObj);
        });
    }
    [Button]
    public void AddNpc()
    {
        sceneObj.AddNpc();
    }

    /// <summary>
    /// 向建筑添加一个对象
    /// </summary>
    [Button]
    public void AddBuilding(int x,int y,BuildingEnum buildingEnum)
    {
        var buildingInst=Meta.Instance.getMeta(buildingEnum).view;
        var obj=GameObject.Instantiate(buildingInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(x,0.3f,y);//添加个对象
        buildingViews[x,y]=obj.GetComponent<BuildingView>();
        var buildingObj = Meta.Instance.getMeta(buildingEnum).createBuildingObj();
        buildingObj.scene=sceneObj;
        buildingObj.x = x;
        buildingObj.y = y;
        buildingViews[x,y].obj = buildingObj;//对象
        this.SendEvent(new OnAddBuilding(buildingObj));//发送数据
    }
    public void BindBuildingObj(BuildingObj buildingObj)
    {
        var buildingInst = buildingObj.now.buildingMeta.view;
        var obj = GameObject.Instantiate(buildingInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(buildingObj.x, 0.3f, buildingObj.y);//添加个对象
        buildingViews[buildingObj.x, buildingObj.y] = obj.GetComponent<BuildingView>();
        buildingViews[buildingObj.x, buildingObj.y].obj = buildingObj;//对象
    }
    /// <summary>
    /// 删除一个对象
    /// </summary>
    [Button]
    public void RemoveBuilding(int x,int y, BuildingEnum buildingEnum)
    {
        var obj=buildingViews[x,y];
        var t = obj.obj;
        buildingViews[x,y]=null;
        Destroy(obj);
        this.SendEvent(new OnRemoveBuilding((BuildingObj)t));
    }
}
