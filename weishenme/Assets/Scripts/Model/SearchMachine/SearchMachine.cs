using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 搜索最优解的机器
/// </summary>
public class SearchMachine
{
    public void Think()
    {
        isSearch=true;//进行搜索
        for(int i=0;i<;i++)
        {

        }
        isSearch=false;
    }
    public bool isSearch { get { return GameArchitect.Instance.saveSystem.saveData.isSearch; } set {
            GameArchitect.Instance.saveSystem.saveData.isSearch = value;
        } }
}
