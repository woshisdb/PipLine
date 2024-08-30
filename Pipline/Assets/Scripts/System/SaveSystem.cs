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
            game.saveData=SerializationUtility.DeserializeValue<SaveData>(tableBytes, DataFormat.JSON); // ����TableType��game.tableAsset������
            Debug.Log("Table data loaded.");
            ///��ʼ��
            foreach(var obj in GameArchitect.get.objs)
            {

            }
        }
        else
        {
            firstInit = true;
            ///saveData��ʼ��
            GameArchitect.get.saveData = new SaveData();
            Debug.Log("Table data loaded.");
            foreach (var obj in GameArchitect.get.objs)
            {
                //Debug.Log(obj.GetType().Name);
                //obj.activities = GameArchitect.activities[obj.GetType()];//һϵ�еĻ
                //obj.cardInf.effect = () => { obj.SendEvent<SelectObjEvent>(new SelectObjEvent(obj)); };
            }
            Debug.LogWarning("No table save file found.");
        }
    }
}


public class SaveData
{
    /// <summary>
    /// ���
    /// </summary>
    public ulong no;
    /// <summary>
    /// һƬ����
    /// </summary>
    public List<LandCellObj> landCells;
    public List<BaseObj> objs;
    public List<NpcObj> npcs;
    public List<BuildingObj> buildings;
    public TimeSystem timeSystem;
    public SaveData()
    {
        no = 0;
        landCells = new List<LandCellObj>();
        objs=new List<BaseObj>();
        timeSystem = new TimeSystem();
        buildings = new List<BuildingObj>();
    }
}