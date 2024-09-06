using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransEnum
{
    public static Dictionary<string,Func<Resource, Resource, Trans, Productivity, Source>> ts;
    public static Source AddSource(string title,Resource from, Resource to,Trans trans, BuildingObj building)
    {
        return ts[title](from, to,trans, new Productivity(from, building));
    }
}
