using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad = "Main";
    public SceneFader sceneFader;
    public void Play()
    {
        sceneFader.Fadeto(levelToLoad);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
