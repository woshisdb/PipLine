using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuiWuJu : BaseObj
{
    public Money money;
    public int exchangeCost;
    public ShuiWuJu():base()
    {
        money = new Money();
        money.money = 10;
        exchangeCost = 10;
    }
}
