using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������Ž�Ļ���
/// </summary>
public class SearchMachine
{
    public void Think()
    {
        isSearch=true;//��������
        for(int i=0;i<;i++)
        {

        }
        isSearch=false;
    }
    public bool isSearch { get { return GameArchitect.Instance.saveSystem.saveData.isSearch; } set {
            GameArchitect.Instance.saveSystem.saveData.isSearch = value;
        } }
}
