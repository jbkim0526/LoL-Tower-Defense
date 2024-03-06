using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Speed : MonoBehaviour
{

    public Text timescale;

    public void Start()
    {
        timescale.text = "1X";
    }
    public void Faster()
    {

        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f;
            timescale.text = "2X";
            return;
            

        }
        if (Time.timeScale == 2f)
        {

            Time.timeScale = 4f;
            timescale.text = "4X";
            return;
        }
    }

    public void MakeNormal()
    {

        Time.timeScale = 1f;
        timescale.text = "1X";
    }

    public void Slower()
    {
        if (Time.timeScale == 4f)
        {
            Time.timeScale = 2f;
            timescale.text = "2X";
            return;
        }
        if (Time.timeScale == 2f)
        {

            Time.timeScale = 1f;
            timescale.text = "1X";
            return;
        }

    }
}
