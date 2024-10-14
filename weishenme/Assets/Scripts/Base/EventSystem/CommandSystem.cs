using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ӿڣ���������ʵ������ӿ�
public interface ICommand 
{
    public void Execute();
}
public interface ISendCommand
{
}
public static class CommandSenderExtensions
{
    // ����������ض�������
    public static void Execute<T>(this ISendCommand sender,T command) where T : struct, ICommand
    {
        command.Execute();
    }
}
