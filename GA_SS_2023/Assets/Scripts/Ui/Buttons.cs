using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonNewGame()
    {
        SceneManager.LoadScene("Play");
    }
       public void ButtonMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadSave()
    {
        
    }

    // Update is called once per frame
    public void ButtonSettings()
    {
        
    }
    public void ButtonExit()
    {
        
    }
    /*public void ButtonSettings()
    {
        
    }*/
}
