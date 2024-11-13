using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseView : MonoBehaviour, ISendEvent
{
    [ShowInInspector]
    public BaseObj obj;
    public void Start()
    {
        obj=new BuildingObj();
        ((BuildingObj)obj).now.goodslist.Add(GoodsEnum.goods1, 1);
    }
    public void OnMouseDown()
    {
        this.SendEvent<SelectViewEvent>(new SelectViewEvent(this));
    }
}
