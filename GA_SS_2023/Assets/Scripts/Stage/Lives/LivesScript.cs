using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LivesScript : MonoBehaviour
{
    private TMP_Text text;
    int lives;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Player");
        PlayerController playerController = Player.GetComponent<PlayerController>();
        lives = playerController.Lives;
        text.text = "Lives: " + lives;
    }
}
