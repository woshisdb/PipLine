using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    // ���ø��µ�ʱ����
    private float updateInterval = 2.0f;
    private float timeSinceLastUpdate = 0f;

    // ��ʼ������
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

    // ���ӵ� Python ������
    void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8080);
            stream = client.GetStream();
            Debug.Log("�����ӵ� Python ������");
        }
        catch (Exception e)
        {
            Debug.LogError("����ʧ��: " + e.Message);
        }
    }

    // �������� Python ������
    public void SendRequest()
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("δ���ӵ�������");
            return;
        }

        // �����������ݣ����磬�� sinValue �� cosValue ���������������ͼ��
        var requestData = new
        {
            BuildingEc = GameArchitect.get.economicSystem.buildingGoodsPrices[GameArchitect.get.buildings[0]].RetHis()
        };
        // ���������л�Ϊ JSON �ַ���
        string jsonString = JsonConvert.SerializeObject(requestData);
        //Debug.Log(jsonString);
        byte[] data = Encoding.UTF8.GetBytes(jsonString);

        // �������ݵ�������
        stream.Write(data, 0, data.Length);
        Debug.Log("�ѷ������ݵ�������");

        // �������Է���������Ӧ
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        // �����������Ӧ
        Debug.Log($"�ӷ��������յ�����Ӧ: {response}");
    }

    // �ر�����
    private void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}
