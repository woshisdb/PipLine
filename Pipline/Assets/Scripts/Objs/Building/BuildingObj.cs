using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager
{

}

public class BuildingObj :BaseObj
{
    /// <summary>
    /// 管线管理器，管理商品
    /// </summary>
    public PipLineManager pipLineManager;
    public Resource resource;
    public Resource goodsResource;
    public GoodsManager goodsManager;
    public BuildingObj()
    {

    }
}
