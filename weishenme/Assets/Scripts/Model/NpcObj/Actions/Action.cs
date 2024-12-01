using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对npc进行操纵
/// </summary>
public abstract class NpcAction
{
    /// <summary>
    /// 这个行为要满足的条件
    /// </summary>
    /// <param name="state"></param>
    public abstract bool Condition(NpcObj state);
    /// <summary>
    /// 这个行为的效果
    /// </summary>
    /// <param name="state"></param>
    public abstract void Effect(NpcObj state);
}