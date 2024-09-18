using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager
{
    /// <summary>
    /// ����������
    /// </summary>
    public jobContainer jobContainer;
    /// <summary>
    /// ʳ�������
    /// </summary>
    public EatFoodContainer foodContainer;
    public NPCManager()
    {
        jobContainer = new jobContainer();
        foodContainer = new EatFoodContainer();
    }
}
/// <summary>
/// ������һ������
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
        return npcObjs.Count;//npc����Ŀ
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
/// ���Ը���һ��NPCһ�����������
/// </summary>
public abstract class NPCContainer<T,F>
where T : NPCKey
where F:NPCItem,new()
{
    public Dictionary<T,F> npcContainers;
    /// <summary>
    /// ע��NPC
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
            // ��ȡ������ָ��������ֵ
            return npcContainers[index];
        }
    }
}
