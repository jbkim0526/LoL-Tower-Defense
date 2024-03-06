using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static float Lives;
    public static float startLives = 300;
    public static int Rounds;

    public Image healthBar;


    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;

    }


    private void Update()
    {
        healthBar.fillAmount = (Lives / startLives);


    }


}
