using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStyle
{
    /// <summary>
    /// һϵ�е���Ʒ
    /// </summary>
    public List<GoodsEnum> goods;
}

///// <summary>
///// �Ͷ�����Դ������
///// </summary>
//public abstract class Job
//{
//    public NpcState npcState;
//    /// <summary>
//    /// �����Ͷ���ʱ���״̬��ȡ�������б�
//    /// </summary>
//    /// <returns></returns>
//    public abstract Tuple<ProdEnum, Int>[] GenProd(NpcState npcState,int t);
//}
///// <summary>
///// ū���׼�,���κ�����ǿ���Ͷ�
///// </summary>
//public class NothingStyle:LifeStyle
//{
//    public float PredicateEarn()
//    {
//        return 0;
//    }
//}
///// <summary>
///// ��ū���׼�,�����Ϊ�Ͷ�����������Ͷ�������
///// </summary>
//public class HasThingLifeStyle: LifeStyle
//{
//    /// <summary>
//    /// ���ݵ�ǰ״̬Ԥ���Լ�δ�������ڵ�����,(һ���˻���ݵ�ǰ���������Ԥ��δ��������)
//    /// </summary>
//    /// <returns></returns>
//    public float PredicateEarn()
//    {
//        ///��Ҫ��������ֻ����ǹ�������
//        if(maxWorkTime==0)
//        {

//        }
//        else//��Ҫ���㹤������
//        {

//        }
//        return 1;
//    }
//}