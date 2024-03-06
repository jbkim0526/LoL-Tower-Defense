using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleEffect : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float circleRadius = 6f;
    public int damageOverTime = 100;
    public float slowAmount = .999f;
    private Transform now;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = 0.1f;
    private float volHighRange = 0.3f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));
            
            if (distanceToEnemy <= circleRadius)
            {
                
                now = enemy.transform;
                Enemy targetEnemy = now.GetComponent<Enemy>();
                targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
                targetEnemy.Slow(slowAmount);
            }
        }

    }
}
