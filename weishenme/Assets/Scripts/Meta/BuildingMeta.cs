using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BuildingMeta : MetaI<BuildingEnum>
{
    ///// <summary>
    ///// ��Ʒ��Enum
    ///// </summary>
    //public GoodsStateEnum state;
    /// <summary>
    /// ��Ҫ���������б�
    /// </summary>
    public Tuple<ProdEnum, Int>[] prods;
    /// <summary>
    /// �����б�
    /// </summary>
    public Tuple<GoodsEnum, Int>[] inputs;
    /// <summary>
    /// ����б�
    /// </summary>
    public Tuple<GoodsEnum, Int> output;
    /// <summary>
    /// ��Ҫ���������б�
    /// </summary>
    public Tuple<ProdEnum, Int>[] pipProds;
    /// <summary>
    /// �����б�
    /// </summary>
    public Tuple<GoodsEnum, Int>[] pipInputs;
    /// <summary>
    /// ����б�
    /// </summary>
    public Tuple<GoodsEnum, Int> pipOutput;
    public int spendTime;
    public GameObject view;
    /// <summary>
    /// ��ȡ��Ʒ������Ϣ
    /// </summary>
    /// <returns></returns>
    public abstract GoodsEnum[] GetGoods();
    public BuildingMeta()
    {
        
    }
    public abstract BuildingObj createBuildingObj();
    public abstract BuildingEnum ReturnEnum();

    public string RetText()
    {
        return ReturnEnum().ToString();
    }
}


