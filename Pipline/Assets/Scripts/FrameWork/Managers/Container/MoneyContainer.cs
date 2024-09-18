using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对食物的追求
/// </summary>
public class EatFood:NPCKey
{
    public float price;
    public float quality;
}

public class NPCEatFoodSet : NPCItem
{
    public NPCEatFoodSet():base()
    {

    }
}

/// <summary>
/// 基本食物的需求
/// </summary>
public class EatFoodContainer : NPCContainer<EatFood, NPCEatFoodSet>
{
    public List<List<EatFood>> keyValues;
    public EatFoodContainer():base()
    {
        keyValues = new List<List<EatFood>>();
        for(int i= 0;i<10;i++)
        {
            keyValues.Add(new List<EatFood>());
            for(int j=0;j<10;j++)
            {
                var key = new EatFood();
                keyValues[i].Add(key);
                npcContainers.Add(key,new NPCEatFoodSet());
            }
        }
    }
    public void Add(float price,float quality,NpcObj npcObj)
    {
        int x = (int)((price) * 10);
        int y= (int)((quality) * 10);
        var key = keyValues[x][y];
        npcContainers[key].Add(npcObj);
    }
}
