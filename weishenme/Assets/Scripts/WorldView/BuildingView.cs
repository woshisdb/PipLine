using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : BaseView
{
    /// <summary>
    /// 绑定对象
    /// </summary>
    /// <param name="baseObj"></param>
    public override void Bind(BaseObj baseObj)
    {
        this.obj = baseObj;
    }
    /// <summary>
    /// 选择一个对象
    /// </summary>
    /// <returns></returns>
    public override (StringInputItem, TextInputItem[])? SelectIt()
    {
        return null;
    }
}
