using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ·������
/// </summary>
public class PathObj : BaseObj
{
    public int wasterTime=10;
    public SceneObj from;
    public SceneObj to;
    /// <summary>
    /// ��������
    /// </summary>
    public NpcObj belong;
    public BuildingObj fromB;
    public BuildingObj toB;
    public override void InitBaseState()
    {
    }

    public override void InitEconomicInf()
    {
    }

    public override void Predict(BaseState input, int day)
    {
        throw new System.NotImplementedException();
    }


    public override void Update(BaseState input)
    {
    }

}


public class SceneObj : BaseObj
{
    /// <summary>
    /// һϵ�е�ǰ��Ŀ��ص�·��
    /// </summary>
    public Dictionary<SceneObj, PathObj> paths;
    /// <summary>
    /// �������б�
    /// </summary>
    public List<BuildingObj> buildingObjs;
    public override void InitBaseState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEconomicInf()
    {
        throw new System.NotImplementedException();
    }

    public override void Predict(BaseState input, int day)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(BaseState input)
    {
        throw new System.NotImplementedException();
    }
}
