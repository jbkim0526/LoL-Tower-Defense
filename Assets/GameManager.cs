using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameisOver;

    public GameObject gameOverUI;

    public AudioClip shootSound;
    private AudioSource source;
    public float vol = 0.2f;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = shootSound;
        source.volume = vol;
        source.Play();
        GameisOver = false;
    }

    void Update()
    {
        if (GameisOver) {
            return;
        }

        if (PlayerStats.Lives <= 0) {

            EndGame();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {

            Toggle();
        }
    }

    void EndGame()
    {
        GameisOver = true;

        gameOverUI.SetActive( true);


    }

    public void Toggle()
    {

        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f;  

        }
        else
        {

            Time.timeScale = 1f;
        }
    }
}
