using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzShark : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float circleRadius = 3f;
    private Transform now;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = 0.15f;
    private float volHighRange = 0.2f;

    private float ulttime = 2f;

    private void Start()
    {
        ulttime = 1f;
        source = GetComponent<AudioSource>();
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);
    }
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (ulttime > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));

                if (distanceToEnemy <= circleRadius)
                {

                    now = enemy.transform;
                    Enemy targetEnemy = now.GetComponent<Enemy>();
                    targetEnemy.TakeDamage(targetEnemy.starthealth);

                }
            }
        }
        ulttime -= Time.deltaTime;
        ulttime = Mathf.Clamp(ulttime, 0f, Mathf.Infinity);
    }
}
