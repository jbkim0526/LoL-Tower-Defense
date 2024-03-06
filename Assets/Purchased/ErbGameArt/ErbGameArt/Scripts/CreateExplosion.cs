using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosion : MonoBehaviour
{
    public GameObject EffectPrefab;
    public float DestroyTime = 2f;
    void Update ()
    {
        DestroyTime -= Time.deltaTime;
        if (DestroyTime <= 0)
        {
            Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, DestroyTime);
        }
    }
}
