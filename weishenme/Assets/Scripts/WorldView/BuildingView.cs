using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : BaseView
{
    /// <summary>
    /// �󶨶���
    /// </summary>
    /// <param name="baseObj"></param>
    public override void Bind(BaseObj baseObj)
    {
        this.obj = baseObj;
    }
    /// <summary>
    /// ѡ��һ������
    /// </summary>
    /// <returns></returns>
    public override (StringInputItem, TextInputItem[])? SelectIt()
    {
        return null;
    }
}
