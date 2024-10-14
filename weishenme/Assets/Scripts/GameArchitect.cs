using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    /// <summary>
    /// 能够接取生产力订单的对象们
    /// </summary>
    public HashSet<ICanReceiveProdOrder> receiveProdOrder;
    /// <summary>
    /// 能够接取普通商品订单的对象们
    /// </summary>
    public HashSet<ICanReceiveNormalGoodsOrder> receiveNormalGoodsOrder;
    public GameArchitect()
    {

    }

    public void 

}
