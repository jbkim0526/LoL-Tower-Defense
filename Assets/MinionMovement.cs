using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class MinionMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    Vector3 dir;


    void Start()
    {
        target = waypoints.points[0];
        enemy = GetComponent<Enemy>();
        dir = target.position - transform.position;

    }

    void Update()
    {
        //Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);


        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;


        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Vector3.Distance(transform.position, target.position) <= 1.5f)
        {

            GetNextWaypoint();
        }
        enemy.speed = enemy.startspeed;


    }

    void GetNextWaypoint()
    {

        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
        dir = target.position - transform.position;

    }

    void EndPath()
    {   
        if(PlayerStats.Lives > 0)
        {
            PlayerStats.Lives -= enemy.NexusDamage;
        }
        if(PlayerStats.Lives <= 0)
        {
            PlayerStats.Lives = 0;

        }
        WaveSpawner.EnemiesKilled++;
        Destroy(gameObject);

    }
}