using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    // 设置更新的时间间隔
    private float updateInterval = 2.0f;
    private float timeSinceLastUpdate = 0f;

    // 初始化连接
    void Start()
    {
        //ConnectToServer();
    }

    //void Update()
    //{
    //    timeSinceLastUpdate += Time.deltaTime;

    //    if (timeSinceLastUpdate >= updateInterval)
    //    {
    //        SendRequest();
    //        timeSinceLastUpdate = 0f;
    //    }
    //}

    // 连接到 Python 服务器
    void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8080);
            stream = client.GetStream();
            Debug.Log("已连接到 Python 服务器");
        }
        catch (Exception e)
        {
            Debug.LogError("连接失败: " + e.Message);
        }
    }

    // 发送请求到 Python 服务器
    public void SendRequest()
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("未连接到服务器");
            return;
        }

        // 构建请求数据（例如，用 sinValue 和 cosValue 随机调整服务器的图表）
        var requestData = new
        {
            BuildingEc = GameArchitect.get.economicSystem.buildingGoodsPrices[GameArchitect.get.buildings[0]].RetHis()
        };
        // 将数据序列化为 JSON 字符串
        string jsonString = JsonConvert.SerializeObject(requestData);
        //Debug.Log(jsonString);
        byte[] data = Encoding.UTF8.GetBytes(jsonString);

        // 发送数据到服务器
        stream.Write(data, 0, data.Length);
        Debug.Log("已发送数据到服务器");

        // 接收来自服务器的响应
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        // 处理服务器响应
        Debug.Log($"从服务器接收到的响应: {response}");
    }

    // 关闭连接
    private void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}
