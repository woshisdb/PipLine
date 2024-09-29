using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    private void Start()
    {
    }

    public IEnumerator SendSceneRequest(uint id, SceneItem ecItem)
    {
        var str=JsonConvert.SerializeObject(new {id=id,ec=ecItem });
        byte[] postData = Encoding.UTF8.GetBytes(str);
        // ���� UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/scene", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            // ��������ͷ
            www.SetRequestHeader("Content-Type", "application/json");

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            // ��������Ƿ����
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // ��ӡ���ص�����
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }

    public IEnumerator SendNpcRequest(uint id, NpcItem ecItem)
    {
        // ����Ҫ���͵�����
        var jsonData = JsonUtility.ToJson(new { id = id, data = ecItem });

        // ���� UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/npc", jsonData))
        {
            // ��������ͷ
            www.SetRequestHeader("Content-Type", "application/json");

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            // ��������Ƿ����
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // ��ӡ���ص�����
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }
    public IEnumerator SendBuildingRequest(uint id, BuildingItem ecItem)
    {
        // ����Ҫ���͵�����
        var jsonData = JsonUtility.ToJson(new { id = id, data = ecItem });

        // ���� UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/building", jsonData))
        {
            // ��������ͷ
            www.SetRequestHeader("Content-Type", "application/json");

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            // ��������Ƿ����
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // ��ӡ���ص�����
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }
}
