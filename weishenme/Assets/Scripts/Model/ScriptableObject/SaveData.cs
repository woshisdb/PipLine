using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class SaveFile
{
    //************************场景与NPC
    public List<List<SceneObj>> SceneObjects;
    public HashSet<NpcObj> npcs;
    //************************市场
    public GoodsMatcher goodsMatcher;
    public WorkMatcher workMatcher;
    public CircularQueue<List<TransGoodsItem>> cirQueue;
    public GovernmentObj government;
}

[CreateAssetMenu(fileName = "newSaveData", menuName = "SaveData/newSaveData")]
public class SaveData : SerializedScriptableObject
{
    /// <summary>
    /// 根据是否是搜索状态来选择访问的是什么
    /// </summary>
    public bool isSearch;
    public SaveFile saveFile;
    public SaveFile searchFile;
    public List<List<SceneObj>> SceneObjects { get {
        if(!isSearch)
            return saveFile.SceneObjects;
        return searchFile.SceneObjects;
    } }
    public HashSet<NpcObj> npcs { get {
        if (!isSearch)
        {
            return saveFile.npcs;
        }
        else
        {
            return searchFile.npcs;
        }
    } }

    public GoodsMatcher goodsMatcher { get {
            if (!isSearch)
            {
                return saveFile.goodsMatcher;
            }
            else
            {
                return searchFile.goodsMatcher ;
            }
    } }
    public WorkMatcher workMatcher { get {
            if (!isSearch)
            {
                return saveFile.workMatcher;
            }else
            {
                return searchFile.workMatcher;
            }
    } }
    public CircularQueue<List<TransGoodsItem>> cirQueue { get {
            if (!isSearch)
            {
                return saveFile.cirQueue;
            }
            else
            {
                return searchFile.cirQueue;
            }
    } }
    public GovernmentObj Government { get {
            if (!isSearch)
            {
                return saveFile.government;
            }
            return searchFile.government;
    } }
    /// <summary>
    /// 设置长度
    /// </summary>
    [Button]
    public void setCirQueue(int n)
    {
        saveFile.cirQueue = new CircularQueue<List<TransGoodsItem>>(n);
        for(int i=0;i<n;i++)
        {
            saveFile.cirQueue.Enqueue( new List<TransGoodsItem>() );
        }
    }

    /// <summary>
    /// 每次修改场景后,修改位置
    /// </summary>
    [Button]
    public void InitScenes()
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            var t=SceneObjects[i];
            for(int j = 0; j < t.Count; j++)
            {
                t[j].now.x = i;
                t[j].now.y = j;
            }
        }
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitAllData(int sizex,int sizey)
    {

    }
    [Button]
    public void Save()
    {
        var filePath = "Assets/Resources/Saves" + "/tablesaveData.dat";
        Debug.Log(filePath);
        var json = SerializationUtility.SerializeValue(saveFile, DataFormat.JSON);
        File.WriteAllBytes(filePath, json);
        Debug.Log("Data Saved: " + json);
    }
    [Button]
    public void Load()
    {
        var filePath = "Assets/Resources/Saves" + "/tablesaveData.dat";
        var json = File.ReadAllBytes(filePath);
        var data = SerializationUtility.DeserializeValue<SaveFile>(json, DataFormat.JSON);
        saveFile = data;
        /////////////////////////////////////
        searchFile= SerializationUtility.DeserializeValue<SaveFile>(json, DataFormat.JSON);
    }
}
