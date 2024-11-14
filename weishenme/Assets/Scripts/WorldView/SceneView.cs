using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SceneView : MonoBehaviour
{
    /// <summary>
    /// 建筑对象
    /// </summary>
    public GameObject buildingInst;
    /// <summary>
    /// 建筑对象
    /// </summary>
    [ShowInInspector]
    public BuildingView[,] buildingViews;
    public Transform root;
    public int sx;
    public int sy;
    public void Start()
    {
        buildingViews = new BuildingView[sx,sy];
    }
    /// <summary>
    /// 向建筑添加一个对象
    /// </summary>
    [Button]
    public void AddBuilding(int x,int y,BuildingEnum buildingEnum)
    {
        var obj=GameObject.Instantiate(buildingInst);///建筑对象
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(x,0,y);//添加个对象
        buildingViews[x,y]=obj.GetComponent<BuildingView>();
    }
    /// <summary>
    /// 删除一个对象
    /// </summary>
    [Button]
    public void RemoveBuilding(int x,int y, BuildingEnum buildingEnum)
    {
        var obj=buildingViews[x,y];
        buildingViews[x,y]=null;
        Destroy(obj);
    }
}
