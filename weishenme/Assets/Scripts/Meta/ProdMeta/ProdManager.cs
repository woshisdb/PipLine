using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdData
{

}

public class ProdManager : Singleton<ProdManager>
{
    /// <summary>
    /// ���һ���ṩ�����������Ƿ�����������Ҫ��,0���´�����,���ϴ������
    /// </summary>
    /// <returns></returns>
    public float TestProd(INeedWork worker, ProdEnum prodEnum)
    {
        return 1;
    }
    private ProdManager()
    {

    }
}
