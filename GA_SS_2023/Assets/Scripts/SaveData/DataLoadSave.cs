using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadSave : MonoBehaviour
{
List<Scoredata> scorelist = new List<Scoredata> ();
    int maxCount = 5;
    string filename = "Scores.json";
    private void Start(){
    //LoadScores();
    }
    public void LoadScores(){
        scorelist = FileHandler.ReadListFromJSON<Scoredata>(filename);

        while(scorelist.Count > maxCount){
            scorelist.RemoveAt(maxCount);
        }
    }
    private void SaveScores(){
        FileHandler.SaveToJSON<Scoredata> (scorelist, filename);
    }

    public void AddScore(Scoredata data){
        for (int i = 0; i < maxCount; i++){
            if (i >= scorelist.Count || data.Score > scorelist[i].Score){
                scorelist.Insert(i, data);

                while(scorelist.Count > maxCount){
                scorelist.RemoveAt(maxCount);
                }
            
            SaveScores();
            break;      
            }
            
        }
    }
}
