using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsObj
{
    /// <summary>
    /// ��������
    /// </summary>
    public BuildingObj building;
    /// <summary>
    /// �ܵ���Ŀ
    /// </summary>
    public Int sum;
    /// <summary>
    /// ��Ʒ����Ϣ
    /// </summary>
    public GoodsEnum goods;
    public GoodsObj(BuildingObj building, Int sum, GoodsEnum goods)
    {
        this.building = building;
        this.sum = sum;
        this.goods = goods;
    }
}
