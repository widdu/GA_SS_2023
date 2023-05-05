using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonNewGame()
    {
        PlayerPrefs.SetInt("Load",0);
        SceneManager.LoadScene("Play");
    }
       public void ButtonMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadSave()
    {
        PlayerPrefs.SetInt("Load",1);
        SceneManager.LoadScene("Play");
    }

    // Update is called once per frame
    public void ButtonExit()
    {
        Application.Quit();
    }
}
