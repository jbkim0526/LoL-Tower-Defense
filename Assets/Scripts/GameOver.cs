using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Text roundsText;
    public SceneFader sceneFader;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        sceneFader.Fadeto(SceneManager.GetActiveScene().name);


    }


    public void Menu()
    {
        sceneFader.Fadeto("MainMenu");
    }
}
