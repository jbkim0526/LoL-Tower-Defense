using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class DebuffOnEnemyFromCollision : MonoBehaviour
{
    public float EffectRadius = 1f;
    public GameObject Effect;
    public event EventHandler<CollisionInfo> CollisionEnter;
    public LayerMask LayerMask = -1;

    void Start()
    {
        CollisionEnter += EffectSettings_CollisionEnter;
    }

    void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
    {
        if (Effect == null)
            return;
        var colliders = Physics.OverlapSphere(transform.position, EffectRadius, LayerMask);
        foreach (var coll in colliders)
        {
            var hitGO = coll.transform;
            var renderer = hitGO.GetComponentInChildren<Renderer>();
            var effectInstance = Instantiate(Effect) as GameObject;
            effectInstance.transform.parent = renderer.transform;
            effectInstance.transform.localPosition = Vector3.zero;
        }
    }

    public enum DeactivationEnum
    {
        Deactivate,
        DestroyAfterCollision,
        DestroyAfterTime,
        Nothing
    };
    public DeactivationEnum InstanceBehaviour = DeactivationEnum.Nothing;
    private bool deactivatedIsWait;
    public float DeactivateTimeDelay = 4;
    public float DestroyTimeDelay = 10;
    public void OnCollisionHandler(CollisionInfo e)
    {
        var handler = CollisionEnter;
        if (handler != null)
            handler(this, e);
        if (InstanceBehaviour == DeactivationEnum.Deactivate && !deactivatedIsWait)
        {
            deactivatedIsWait = true;
            Invoke("Deactivate", DeactivateTimeDelay);
        }
        if (InstanceBehaviour == DeactivationEnum.DestroyAfterCollision) Destroy(gameObject, DestroyTimeDelay);
    }
}

public class CollisionInfo : EventArgs
{
    public RaycastHit Hit;
}