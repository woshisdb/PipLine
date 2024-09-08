using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransEnum
{
    public static Dictionary<string, Func<Resource, Resource, Trans, Productivity, Source>> ts;
    public static void Init()
    {
        ts=new Dictionary<string, Func<Resource, Resource, Trans, Productivity, Source>>();
        ts.Add("��������ʯ", (from, to, trans, prod) =>
        {
            return new PipLineSource(from.building,from, to, trans, prod);
        });
        ts.Add("��������", (from, to, trans, prod) =>
        {
            return new PipLineSource(from.building,from, to, trans, prod);
        });
        ts.Add("������Ʒ", (from, to, trans, prod) =>
        {
            return new CarrySource(from.building,from, to, trans, prod);
        });
    }
    public static Source AddSource(string title,Resource from, Resource to,Trans trans, BuildingObj building)
    {
        if(ts==null)
        {
            Init();
        }
        return ts[title](from, to,trans, new Productivity(from, building));
    }
}
