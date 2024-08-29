using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObj :BaseObj
{
    /// <summary>
    /// 管线管理器，管理商品
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// 流水线中间产物
    /// </summary>
    public Resource resource;
    /// <summary>
    /// 商品资源
    /// </summary>
    public Resource goodsRes;
    /// <summary>
    /// 商品管理器
    /// </summary>
    public GoodsManager goodsManager;
    public BuildingObj()
    {
        pipLineManager = new PipLineManager();
        resource = new Resource();
        goodsRes = new Resource();
        goodsManager = new GoodsManager(goodsManager);
    }
}


