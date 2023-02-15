using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScore : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private TrackController trackController;

    public TrackController TrackController
    {
        get { return trackController; }
    }

    // Private variables
    private int score;

    public int Score { get { return score; } set { score = value; } }

}
