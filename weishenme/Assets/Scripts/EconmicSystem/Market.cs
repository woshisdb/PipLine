using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market
{
    public Dictionary<SceneObj, MarketScene> Markets = new Dictionary<SceneObj, MarketScene>();
}
/// <summary>
/// �г�����
/// </summary>
public class MarketScene
{
    /// <summary>
    /// һϵ��������Ʒ��Э��
    /// </summary>
    public List<SendGoods> requestGoods;
    /// <summary>
    /// һϵ����������Э��
    /// </summary>
    public List<SendWork> requestWork;
    /// <summary>
    /// ��������NPC
    /// </summary>
    public List<ReceiveWork> requestWorkNpc;
    /// <summary>
    /// ���Խ�����Ʒ��Э��
    /// </summary>
    public List<ReceiveGoods> canReceiveGoodsContracts;
}