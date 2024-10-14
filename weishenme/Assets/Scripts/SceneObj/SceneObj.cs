using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PathState : BaseState
{
    /// <summary>
    /// ������
    /// </summary>
    public double money;
}

public class PathEc : EconomicInf
{

}


public class PathObj : BaseObj<PathState, PathEc>
{
    public int wasterTime=10;
    public SceneObj from;
    public SceneObj to;
    public override PathState Update(PathState input, PathState output)
    {
        return null;
    }
}


public class SceneState : BaseState
{
    /// <summary>
    /// ������
    /// </summary>
    public double money;
}

public class SceneEc : EconomicInf
{

}

public class SceneObj : BaseObj<SceneState, SceneEc>
{
    public Dictionary<SceneObj, PathObj> paths;//һϵ�е�ǰ��Ŀ��ص�·��
    public override SceneState Update(SceneState input, SceneState output)
    {
        return output;
    }
}
