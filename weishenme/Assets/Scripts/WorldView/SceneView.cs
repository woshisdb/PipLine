using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SceneView : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public GameObject buildingInst;
    /// <summary>
    /// ��������
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
    /// �������һ������
    /// </summary>
    [Button]
    public void AddBuilding(int x,int y,BuildingEnum buildingEnum)
    {
        var obj=GameObject.Instantiate(buildingInst);///��������
        obj.transform.parent = root;
        obj.transform.transform.localPosition = new Vector3(x,0,y);//��Ӹ�����
        buildingViews[x,y]=obj.GetComponent<BuildingView>();
    }
    /// <summary>
    /// ɾ��һ������
    /// </summary>
    [Button]
    public void RemoveBuilding(int x,int y, BuildingEnum buildingEnum)
    {
        var obj=buildingViews[x,y];
        buildingViews[x,y]=null;
        Destroy(obj);
    }
}
