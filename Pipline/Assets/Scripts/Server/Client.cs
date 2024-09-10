using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;  // 确保已安装 Newtonsoft.Json 库
/// <summary>
/// 设计规划
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
            // 连接到 Python 服务器
            client = new TcpClient("127.0.0.1", 8080);
            stream = client.GetStream();

            // 创建要发送的 JSON 数据
            var jsonData = new
            {
                message = "Hello from Unity",
                time = DateTime.Now.ToString()
            };

            // 将数据序列化为 JSON 字符串
            string jsonString = JsonConvert.SerializeObject(jsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonString);

            // 发送数据
            stream.Write(data, 0, data.Length);
            Debug.Log("已发送 JSON 数据到 Python 服务器");

            // 接收来自服务器的响应
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log($"从服务器接收到的响应: {response}");

        }
        catch (Exception e)
        {
            Debug.LogError("Socket 错误: " + e.Message);
        }
        finally
        {
            // 关闭连接
            if (stream != null) stream.Close();
            if (client != null) client.Close();
        }
    }
}
