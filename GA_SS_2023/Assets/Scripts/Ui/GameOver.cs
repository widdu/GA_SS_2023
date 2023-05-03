using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    int score;
    string time;
    private TMP_Text text;
    public void Start() {
        text = GetComponent<TMP_Text>();
        time = PlayerPrefs.GetString("Time","Not found");
        score = PlayerPrefs.GetInt("Score",0);
        setData();

    }
    public void setData()
    {
        text.text =  "Score: " + score + " Time: " + time;
    }

}
