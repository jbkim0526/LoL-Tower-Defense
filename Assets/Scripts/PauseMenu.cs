using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public GameObject ui;
    public SceneFader sceneFader;

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();

        }
    }


    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;  // 0 : 스탑, 1: 정상 시간 , 2: 두배 

        }
        else
        {

            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();

        sceneFader.Fadeto(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {

        Toggle();
        sceneFader.Fadeto("MainMenu");
    }
}
