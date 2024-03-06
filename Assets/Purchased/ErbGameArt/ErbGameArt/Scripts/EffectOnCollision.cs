using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectOnCollision : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public int damage = 40;
    public float slowAmount = .5f;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        if (other.tag == "Enemy")
        {
            
            Damage(other.transform);
        }

        while (i < numCollisionEvents)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                Damage(instance.transform);

                if (!UseWorldSpacePosition)instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);
            }
            i++;
        }

        
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
            e.Slow(slowAmount);
        }
    }
}
