using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Խ��չ���
/// </summary>
public class ReceiveWork
{
    /// <summary>
    /// ��ס��λ��
    /// </summary>
    public Func<SceneObj> scene;
    /// <summary>
    /// ����
    /// </summary>
    public IReceiveWork obj;
    /// <summary>
    /// ���ٸ��ҵ�Ǯ
    /// </summary>
    public Func<Float> minPrice;
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
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
    public float maxMoney;
}
/// <summary>
/// ������Ʒ
/// </summary>
public class ReceiveGoods
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
    /// ���ٸ��ҵ�Ǯ
    /// </summary>
    public Func<Float> minPrice;
    public float minMoney;
    /// <summary>
    /// ���ն���
    /// </summary>
    public IReceiveGoods obj;
}
/// <summary>
/// ���Է��Ͷ���
/// </summary>
public class SendGoods
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
    public ISendGoods obj;
    /// <summary>
    /// �����Ը���Ǯ
    /// </summary>
    public Func<Float> maxPrice;
    public float maxMoney;
}
