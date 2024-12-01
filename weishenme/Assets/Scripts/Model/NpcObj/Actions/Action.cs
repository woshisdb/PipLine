using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��npc���в���
/// </summary>
public abstract class NpcAction
{
    /// <summary>
    /// �����ΪҪ���������
    /// </summary>
    /// <param name="state"></param>
    public abstract bool Condition(NpcObj state);
    /// <summary>
    /// �����Ϊ��Ч��
    /// </summary>
    /// <param name="state"></param>
    public abstract void Effect(NpcObj state);
}