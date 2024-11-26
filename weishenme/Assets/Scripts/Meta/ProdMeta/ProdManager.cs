using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdData
{

}

public class ProdManager : Singleton<ProdManager>
{
    /// <summary>
    /// 检测一个提供生产力的人是否满足生产力要求,0以下代表不行,以上代表可以
    /// </summary>
    /// <returns></returns>
    public float TestProd(INeedWork worker, ProdEnum prodEnum)
    {
        return 1;
    }
    private ProdManager()
    {

    }
}
