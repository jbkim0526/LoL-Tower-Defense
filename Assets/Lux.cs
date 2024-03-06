using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class Lux : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update

    private string enemyTag = "Enemy";
    private Transform target;
    private Enemy targetEnemy;
    private GameObject[] Enemies;

    [Header("Lux Ult")]
    public GameObject Cylinder;
    public float Cylinradius = 10f;

    public ParticleSystem LuxUltEffect;
    public int LuxUltDamage = 70;

    [Header("Lux")]
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
        Enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in Enemies)
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
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null) { return; }
        
        else
        {


            if( isEnded == true)
            {
                isEnded = false;
                LockOnTarget();
                StartCoroutine(useUlt());

                
            }

            

            
        

        }

    



    }


    IEnumerator useUlt()
    {
        
        anim.Play("Take 001 0");
        LuxUltEffect.Play();
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);

        DamageTargets();
        while (!anim.IsInTransition(0))
        {
            yield return null;
        }
        isEnded = true;



    }
    

    
    void DamageTargets()
    {
       
        Vector3 direction = target.position - transform.position;
        Vector3 startingPoint = transform.position;
        Ray ray = new Ray(startingPoint, direction);


        foreach (GameObject enemy in Enemies)
        {   
            if(enemy == null)
            {
                continue;
            }
            float distanceToEnemy = DistanceToLine(ray, enemy.transform.position);

            if (distanceToEnemy < Cylinradius) // 원통과의 거리가 반지름보다 작으면 데미지 줌. 
            {

                Damage(enemy.transform);
             
            }
        }

    }



    public static float DistanceToLine(Ray ray, Vector3 point)
    {
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
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
            e.TakeDamage(LuxUltDamage);
        }

    }

}