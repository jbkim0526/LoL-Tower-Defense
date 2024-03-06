using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Anivia : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject EffectPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;
    public Animator anim;

    public AudioClip pickSound;

    GameObject nearestEnemy = null;

    void Start()
    {
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

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

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
        Vector3 onBoard;
        onBoard = new Vector3(nearestEnemy.transform.position.x, 0.5f, nearestEnemy.transform.position.z);
        anim.Play("Take 001");

        GameObject EffectGo = (GameObject)Instantiate(EffectPrefab, onBoard, EffectPrefab.transform.rotation);
        Destroy(EffectGo, 3f);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
