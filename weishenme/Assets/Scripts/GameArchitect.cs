using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    /// <summary>
    /// �ܹ���ȡ�����������Ķ�����
    /// </summary>
    public HashSet<ICanReceiveProdOrder> receiveProdOrder;
    /// <summary>
    /// �ܹ���ȡ��ͨ��Ʒ�����Ķ�����
    /// </summary>
    public HashSet<ICanReceiveNormalGoodsOrder> receiveNormalGoodsOrder;
    public GameArchitect()
    {

    }

    public void 

}
