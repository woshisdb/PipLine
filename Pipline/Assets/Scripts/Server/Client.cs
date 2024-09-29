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
        // 创建 UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/scene", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            // 设置请求头
            www.SetRequestHeader("Content-Type", "application/json");

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            // 检查请求是否出错
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // 打印返回的数据
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }

    public IEnumerator SendNpcRequest(uint id, NpcItem ecItem)
    {
        // 创建要发送的数据
        var jsonData = JsonUtility.ToJson(new { id = id, data = ecItem });

        // 创建 UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/npc", jsonData))
        {
            // 设置请求头
            www.SetRequestHeader("Content-Type", "application/json");

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            // 检查请求是否出错
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // 打印返回的数据
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }
    public IEnumerator SendBuildingRequest(uint id, BuildingItem ecItem)
    {
        // 创建要发送的数据
        var jsonData = JsonUtility.ToJson(new { id = id, data = ecItem });

        // 创建 UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/building", jsonData))
        {
            // 设置请求头
            www.SetRequestHeader("Content-Type", "application/json");

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            // 检查请求是否出错
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // 打印返回的数据
                Debug.Log($"Response: {www.downloadHandler.text}");
            }
        }
    }
}
