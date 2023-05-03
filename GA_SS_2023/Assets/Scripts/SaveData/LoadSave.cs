using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadSave : MonoBehaviour{

public int Score;
public float Time;
public void Start(){ 

}
    private void SaveScore() {
        SaveToJson();
    }
    public void SaveToJson()
    {
        savedata data = new savedata();
        data.Score = Score;
        data.Time = Time;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/GameSaveData.json", json);
    }
 
    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/GameSaveData.json");
        savedata data = JsonUtility.FromJson<savedata>(json);
        Score = data.Score;
        Time = data.Time;
    }
    
}
