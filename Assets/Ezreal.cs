using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class Ezreal : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update

    public string enemyTag = "Enemy";
    private Transform target;
    private Enemy targetEnemy;
    private GameObject[] Enemies;

    [Header("Ez Ult")]
    public Transform firepoint;
    public ParticleSystem EzUltEffect;
    public int EzUltDamage = 70;

    [Header("Ez")]
    public float range = 15f;
    public Transform PartToRotate;
    public float turnSpeed = 10f;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = 0.2f;
    private float volHighRange = 0.4f;

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
        source = GetComponent<AudioSource>();
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
                StartCoroutine(UseUlt());


            }
        }
  

    }


    IEnumerator UseUlt()
    {

        anim.Play("Take 001");
        EzUltEffect.Play();
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);


        while (!anim.IsInTransition(0))
        {
            yield return null;
        }
        Damage(target);

        isEnded = true;

        

    }



    void Damage(Transform enemy)  // 입력 받은 enemy를 죽임 
    {

        if (enemy == null)
        {
            return;
        }

        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(EzUltDamage);
        }

    }




}