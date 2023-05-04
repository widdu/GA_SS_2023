using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] DataLoadSave dataLoadSave;
    // Start is called before the first frame update
    public int score;
    public string time;
    private TMP_Text text;
    private void Start() {
        text = GetComponent<TMP_Text>();
        time = PlayerPrefs.GetString("Time","Not found");
        score = PlayerPrefs.GetInt("Score",0);
        text.text =  "Score: " + score + " Time: " + time;
        dataLoadSave.LoadScores();
        dataLoadSave.AddScore(new Scoredata(score,time));
        Debug.Log(score);
        Debug.Log(time);
    }

}
