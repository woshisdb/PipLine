using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

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

public struct OnAddPath : IEvent
{
    public PathObj pathObj;
    public OnAddPath(PathObj pathObj)
    {
        this.pathObj = pathObj;
    }
}

public struct OnRemovePath : IEvent
{
    public PathObj pathObj;
    public OnRemovePath(PathObj pathObj)
    {
        this.pathObj = pathObj;
    }
}

public class SceneView : MonoBehaviour,ISendEvent
{
    public SceneObj sceneObj;
    /// <summary>
    /// 建筑对象
    /// </summary>
    [ShowInInspector]
    public List<List<BaseView>> views;
    public Transform root;
    public int sx;
    public int sy;
    public int x { get { return sceneObj.now.x; } }
    public int y { get { return sceneObj.now.y; } }
    public void Awake()
    {
        InitView();
    }
    [Button]
    public void InitView()
    {
        views = new List<List<BaseView>>();
        for (int i = 0; i < sx; i++)
        {
            views.Add(new List<BaseView>());
            for (int j = 0; j < sy; j++)
            {
                views[i].Add(null);
            }
        }
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
        for (int i = 0; i < sceneObj.paths.Count; i++)
        {
            BindPathObj(sceneObj.paths[i]);
        }
        sceneObj.Register<OnAddPath>(this, e =>
        {
            Debug.Log("add");
            sceneObj.AddPath(e.pathObj);
        });
        sceneObj.Register<OnRemoveBuilding>(this, e =>
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
        if (views[x][y]!=null)
        {
            return;
        }
        var buildingInst=Meta.Instance.getMeta(buildingEnum).view;
        var obj=GameObject.Instantiate(buildingInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(x,0.3f,y);//添加个对象
        views[x][y] = obj.GetComponent<BuildingView>();
        var buildingObj = Meta.Instance.getMeta(buildingEnum).createBuildingObj();
        buildingObj.scene=sceneObj;
        buildingObj.x = x;
        buildingObj.y = y;
        views[x][y].obj = buildingObj;//对象
        this.SendEvent(new OnAddBuilding(buildingObj));//发送数据
    }

    /// <summary>
    /// 向建筑添加一个对象
    /// </summary>
    [Button]
    public void AddPath(int x, int y, PathEnum buildingEnum)
    {
        if (views[x][y] != null)
        {
            return;
        }
        var pathInst = Meta.Instance.getMeta(buildingEnum).view;
        var obj = GameObject.Instantiate(pathInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(x, 0f, y);//添加个对象
        views[x][y] = (BaseView) obj.GetComponent<PathView>();
        var pathObj = Meta.Instance.getMeta(buildingEnum).createPathObj();
        pathObj.scene = sceneObj;
        pathObj.x = x;
        pathObj.y = y;
        views[x][y].obj = pathObj;//对象
        sceneObj.now.mapLabels.PathUpdate(pathObj);
        this.SendEvent(new OnAddPath(pathObj));//发送数据
    }

    public void BindBuildingObj(BuildingObj buildingObj)
    {
        var buildingInst = buildingObj.now.buildingMeta.view;
        var obj = GameObject.Instantiate(buildingInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(buildingObj.x, 0.3f, buildingObj.y);//添加个对象
        views[buildingObj.x][buildingObj.y] = obj.GetComponent<BuildingView>();
        views[buildingObj.x][buildingObj.y].obj = buildingObj;//对象
    }

    public void BindPathObj(PathObj pathObj)
    {
        var pathIns = pathObj.now.pathMeta.view;
        var obj = GameObject.Instantiate(pathIns);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(pathObj.x, 0.3f, pathObj.y);//添加个对象
        views[pathObj.x][pathObj.y] = obj.GetComponent<PathView>();
        views[pathObj.x][pathObj.y].obj = pathObj;//对象
    }

    /// <summary>
    /// 删除一个对象
    /// </summary>
    [Button]
    public void RemoveBuilding(int x,int y, BuildingEnum buildingEnum)
    {
        var obj=views[x][y];
        var t = obj.obj;
        views[x][y]=null;
        Destroy(obj);
        this.SendEvent(new OnRemoveBuilding((BuildingObj)t));
    }
    [Button]
    public void TestPath(Vector2Int a,Vector2Int b)
    {
        Debug.Log( sceneObj.GetTime(a,b) );
    }
}
