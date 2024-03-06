using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Karthus : MonoBehaviour
{
    private Transform target;
    private Transform now;
    private Enemy targetEnemy;
    private Enemy nowEnemy;

    [Header("Attributes")]

    public float range = 150f;

    [Header("Use Bullets (default)")]
    public GameObject karthusUlt;
    public int damage;
    public float fireRate = 0.1f;
    public float ultCountdown = 0f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 50f;

    public Transform firePoint;
    public Animator anim;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = 0.05f;
    private float volHighRange = 0.1f;

    public AudioClip pickSound;

    GameObject nearestEnemy = null;

    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //Instantiate(EffectPrefab, firePoint.position, firePoint.rotation);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);



            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (fireCountdown <= 0f)
        { 
            foreach (GameObject enemy in enemies)
            {
                now = enemy.transform;
                nowEnemy = now.GetComponent<Enemy>();
                nowEnemy.KarthusUlt();
                Shoot();


                ultCountdown = 3f;
                fireCountdown = 1f / fireRate;
            }
        }
        if (ultCountdown <= 0f)
        {
            foreach (GameObject enemy in enemies)
            {
                now = enemy.transform;
                nowEnemy = now.GetComponent<Enemy>();
                if(nowEnemy.ultOn)
                {
                    nowEnemy.TakeDamage(damage);
                    nowEnemy.ultOn = false;
                }
                
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
        {

            return;
        }

        LockOnTarget();

        


        ultCountdown -= Time.deltaTime;
        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        anim.Play("Take 001");
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);
        //karthusUlt.SetActive(true);

        //karthusUlt.SetActive(false);

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
