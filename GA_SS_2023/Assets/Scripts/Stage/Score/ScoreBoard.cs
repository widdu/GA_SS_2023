using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private GoalScore[] endGoals = new GoalScore[3];

    // Private variables
    private TMP_Text text;
    private int totalScore;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void UpdateScore()
    {
        totalScore = 0;
        for(int i = 0; i < endGoals.Length; i++)
        {
            totalScore += endGoals[i].Score;
        }
        UpdateText();
    }

    public void ResetScore()
    {
        for(int i = 0; i < endGoals.Length; i++)
        {
            endGoals[i].Score = 0;
        }
        totalScore = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = "Score: " + totalScore;
    }
}
