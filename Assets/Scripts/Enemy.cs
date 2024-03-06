
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float startspeed = 10f;
    
    [HideInInspector]
    public float speed;


    public float starthealth = 100;
    private float health;
    private GameObject Ult;

    public int NexusDamage = 1;
    public int worth = 10; // 죽으면 주는 돈 
    public GameObject deathEffect;
    public GameObject ultEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    [HideInInspector]
    public bool ultOn = false;



    private void Start()
    {
        speed = startspeed;
        health = starthealth;
    }

    private void Update()
    {
        if (ultOn)
        {
            if (transform != null && Ult != null)
            {
                Ult.transform.position = transform.position;
            }

        }
    }

    public void TakeDamage(float amount) {

 

        health -= amount;
        healthBar.fillAmount = health / starthealth;
        if (health <= 0)
        {
            Die();
        }
    }



    public void Slow(float percentage)
    {
        speed = startspeed * (1f - percentage);


    }

    void Die() {

        PlayerStats.Money += worth;


        GameObject effect =Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect,5f);
        WaveSpawner.EnemiesKilled++;
        Destroy(gameObject);
        
    }

    public void KarthusUlt()
    {
        ultOn = true;
        Ult = Instantiate(ultEffect, transform.position, Quaternion.identity);
        Destroy(Ult, 3.2f);
    }





}
