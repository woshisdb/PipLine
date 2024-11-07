using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ṩ����
/// </summary>
public class NeedWork
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ����
    /// </summary>
    public INeedWork obj;
    /// <summary>
    /// ��������͹���
    /// </summary>
    public float minMoney;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class SendWork
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ���ͷ�
    /// </summary>
    public ISendWork obj;
    /// <summary>
    /// ���ĸ��Ĺ���
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// �Ƿ������㹤��
    /// </summary>
    public Func<NeedWork, bool> isSatify;
    /// <summary>
    /// �����
    /// </summary>
    public Func<NeedWork, float> satifyRate;
}
/// <summary>
/// ������Ʒ
/// </summary>
public class SendGoods
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public GoodsObj goods;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ��Ҫ���ֵ�Ǯ
    /// </summary>
    public float minMoney;
    /// <summary>
    /// ���ն���
    /// </summary>
    public ISendGoods obj;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class NeedGoods
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public GoodsObj[] goods;
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ������Ʒ
    /// </summary>
    public INeedGoods obj;
    /// <summary>
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
    /// <summary>
    /// ����Ǯ
    /// </summary>
    public float maxMoney;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public Func<SendGoods, bool> isSatify;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public Func<SendGoods, bool> satifyRate;
}
