using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    public SaveData saveData;
    public void Save()
    {
        saveData.Save();
    }
    public void Load()
    {
        saveData=(SaveData)Resources.Load("SaveData/newSaveData");
        saveData.Load();
    }
    private SaveSystem()
    {

    }
}
