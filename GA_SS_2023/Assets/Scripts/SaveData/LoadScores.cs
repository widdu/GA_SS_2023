using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadScores : MonoBehaviour
{
    List<Scoredata> scorelist = new List<Scoredata> ();
    string filename = "Scores.json";
    private TMP_Text text;
    // Start is called before the first frame update
    private void Start()
    {
        string txt;
        text = GetComponent<TMP_Text>();
        txt = LoadScoresS();


        text.text = txt;

    }
    public string LoadScoresS(){
        string Scores ="";
        int a = 1;
        scorelist = FileHandler.ReadListFromJSON<Scoredata>(filename);
        for (int i = 0; i < scorelist.Count; i++){
            Scores += "Top "+ a + " Time: "+scorelist[i].Time +" Score: "+ scorelist[i].Score +"<br>";
            a++;
        }
        return Scores;
    
    }
}
