using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// һ����Ʒ����,����NPC�ļ�,����˯��,���üҾ�,�洢��Ʒ
/// </summary>
public class HouseBuildingState : BuildingState
{
    /// <summary>
    /// �洢��һϵ����Ʒ
    /// </summary>
    public Dictionary<GoodsEnum, int> storeGoods;
    public HouseBuildingState() : base()
    {

    }
    public override void Init()
    {

    }
}
/// <summary>
///���Ӿ�����Ϣ
/// </summary>
public class HouseBuildingEc : BuildingEc
{

}
/// <summary>
/// ���ӵĽ���
/// </summary>
public class HouseBuildingObj : BuildingObj
{
}