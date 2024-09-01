using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Sirenix.Serialization;
using System.IO;
using Sirenix.OdinInspector;
using System;

public class SaveSystem :Singleton<SaveSystem>
{
    public bool firstInit;
    string tablesavePath;
    private SaveSystem()
    {
        Debug.Log(Application.persistentDataPath);
        tablesavePath = "Assets/Resources/SaveData" + "/tablesaveData.dat";
    }
    [Button("Save Game")]
    public void Save()
    {
        GameArchitect game= (GameArchitect)GameArchitect.Interface;
        byte[] tablebytes = SerializationUtility.SerializeValue(game.saveData, DataFormat.JSON);
        File.WriteAllBytes(tablesavePath, tablebytes);
        Debug.Log("Game Saved.");
    }
    //protected void PDDLInit()
    //{
    //    foreach (var x in tableAsset.tableSaver.objs)
    //    {
    //        x.InitPDDLClass();
    //    }
    //}
    [Button("Load Game")]
    public void Load()
    {
        GameArchitect game = (GameArchitect)GameArchitect.Interface;
        if (File.Exists(tablesavePath))
        {
            firstInit = false;
            byte[] tableBytes = File.ReadAllBytes(tablesavePath);
            game.saveData=SerializationUtility.DeserializeValue<SaveData>(tableBytes, DataFormat.JSON); // 假设TableType是game.tableAsset的类型
            Debug.Log("Table data loaded.");
            ///初始化
        }
        else
        {
            firstInit = true;
            ///saveData初始化
            GameArchitect.get.saveData = new SaveData();
            Debug.Log("Table data loaded.");
            Debug.LogWarning("No table save file found.");
        }
    }
}


public class SaveData
{
    /// <summary>
    /// 标号
    /// </summary>
    public ulong no;
    public List<NpcObj> npcs;
    public List<BuildingObj> buildings;

    [SerializeField]
    public WorldMap map;

    public TimeSystem timeSystem;
    public SaveData()
    {
        no = 0;
        timeSystem = new TimeSystem();
        buildings = new List<BuildingObj>();
        map = new WorldMap();
        npcs = new List<NpcObj>();
    }
}