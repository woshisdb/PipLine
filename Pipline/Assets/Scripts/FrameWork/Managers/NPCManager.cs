using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager
{
    /// <summary>
    /// 工作的容器
    /// </summary>
    public jobContainer jobContainer;
    /// <summary>
    /// 食物的容器
    /// </summary>
    public EatFoodContainer foodContainer;
    public NPCManager()
    {
        jobContainer = new jobContainer();
        foodContainer = new EatFoodContainer();
    }
}
/// <summary>
/// 容器的一个类型
/// </summary>
public class NPCItem
{
    public HashSet<NpcObj> npcObjs;
    public void Add(NpcObj npc)
    {
        npcObjs.Add(npc);
    }
    public int Count()
    {
        return npcObjs.Count;//npc的数目
    }
    public NPCItem()
    {
        npcObjs = new HashSet<NpcObj>();
    }
}
public interface NPCKey
{

}


/// <summary>
/// 用以概括一个NPC一个方面的特征
/// </summary>
public abstract class NPCContainer<T,F>
where T : NPCKey
where F:NPCItem,new()
{
    public Dictionary<T,F> npcContainers;
    /// <summary>
    /// 注册NPC
    /// </summary>
    public void RegContainer(T job,NpcObj npc)
    {
        if (!npcContainers.ContainsKey(job))
        {
            npcContainers.Add(job, new F());
        }
        npcContainers[job].Add(npc);
    }
    public void Add(T job)
    {
        if (!npcContainers.ContainsKey(job))
        {
            npcContainers.Add(job, new F());
        }
    }


    public NPCContainer()
    {
        npcContainers = new Dictionary<T, F>();
    }
    public F this[T index]
    {
        get
        {
            // 获取数组中指定索引的值
            return npcContainers[index];
        }
    }
}
