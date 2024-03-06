using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    public float turnSpeed = 10f;


    void Start()
    {
        target = waypoints.points[0];
        enemy = GetComponent<Enemy>();

    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
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

    }

    void EndPath()
    {
        PlayerStats.Lives -= enemy.NexusDamage;
        WaveSpawner.EnemiesKilled++;
        Destroy(gameObject);

    }
}
