using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkaliAttack : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update

    public string enemyTag = "Enemy";
    private Transform target;
    private Enemy targetEnemy;
    public float range = 15f;
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public int AkaliAttackDmg = 40;

    public AudioClip pickSound;


    private bool isEnded = true;

    void UpdateTarget()
    {   


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
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


    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null) { return; }

        else
        {
            if (isEnded == true)
            {
                isEnded = false;
                LockOnTarget();
                StartCoroutine(useAttack());

            }
        }





    }

    IEnumerator useAttack()
    {

        anim.Play("Take 001");
        Damage(targetEnemy);
        yield return new WaitForSeconds(0.3f);
        
        isEnded = true;
    }



    void Damage(Enemy enemy)  // 입력 받은 enemy를 죽임 
    {

        if (enemy != null)
        {
            enemy.TakeDamage(AkaliAttackDmg);
        }

    }
}
