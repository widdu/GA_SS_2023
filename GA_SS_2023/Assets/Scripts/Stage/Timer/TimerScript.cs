using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    private TMP_Text text;
    public float timer;
    // Start is called before the first frame update
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        text.text = "Time: " + timer.ToString("F2");
    }
}
