using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem:Singleton<MapSystem>
{
    /// <summary>
    /// һϵ�еĳ���
    /// </summary>
    public List<SceneObj> scenes;
    /// <summary>
    /// �ڸ������ѵ���Сʱ��
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public int WasterTime(SceneObj a,SceneObj b,int money)
    {
        return 1;
    }

    public int WasterMoney(SceneObj a, SceneObj b,int time)
    {
        return 1;
    }
}
