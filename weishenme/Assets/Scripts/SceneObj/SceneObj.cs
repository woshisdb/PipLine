using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PathState : BaseState
{
    /// <summary>
    /// 总收入
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
    /// 总收入
    /// </summary>
    public double money;
}

public class SceneEc : EconomicInf
{

}

public class SceneObj : BaseObj<SceneState, SceneEc>
{
    public Dictionary<SceneObj, PathObj> paths;//一系列的前往目标地的路径
    public override SceneState Update(SceneState input, SceneState output)
    {
        return output;
    }
}
