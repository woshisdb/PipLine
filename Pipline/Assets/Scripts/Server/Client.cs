using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;  // ȷ���Ѱ�װ Newtonsoft.Json ��
/// <summary>
/// ��ƹ滮
/// </summary>
public struct PlanEvent:IEvent
{

}

public class Client : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            // ���ӵ� Python ������
            client = new TcpClient("127.0.0.1", 8080);
            stream = client.GetStream();

            // ����Ҫ���͵� JSON ����
            var jsonData = new
            {
                message = "Hello from Unity",
                time = DateTime.Now.ToString()
            };

            // ���������л�Ϊ JSON �ַ���
            string jsonString = JsonConvert.SerializeObject(jsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonString);

            // ��������
            stream.Write(data, 0, data.Length);
            Debug.Log("�ѷ��� JSON ���ݵ� Python ������");

            // �������Է���������Ӧ
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log($"�ӷ��������յ�����Ӧ: {response}");

        }
        catch (Exception e)
        {
            Debug.LogError("Socket ����: " + e.Message);
        }
        finally
        {
            // �ر�����
            if (stream != null) stream.Close();
            if (client != null) client.Close();
        }
    }
}
