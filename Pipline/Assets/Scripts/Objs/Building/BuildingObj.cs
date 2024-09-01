using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObj :BaseObj,ISendEvent
{
    public string name="����";
    /// <summary>
    /// ���߹�������������Ʒ
    /// </summary>
    public PipLineManager pipLineManager;
    /// <summary>
    /// ��ˮ���м����
    /// </summary>
    public Resource resource;
    /// <summary>
    /// ��Ʒ��Դ
    /// </summary>
    public Resource goodsRes;
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public GoodsManager goodsManager;
    /// <summary>
    /// �ṩ��������
    /// </summary>
    public Productivity productivity;

    public BuildingObj()
    {
        pipLineManager = new PipLineManager(this);
        resource = new Resource();
        goodsRes = new Resource();
        goodsManager = new GoodsManager(goodsRes);
        productivity = new Productivity(resource,this);
    }
    public IEnumerator Update()
    {
        return null;
    }
    public void UpdateEvent()
    {
        this.SendEvent(new UpdateBuildingEvent());
    }
}


