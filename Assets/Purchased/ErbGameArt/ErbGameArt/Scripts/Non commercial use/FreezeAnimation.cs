using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAnimation : MonoBehaviour {

    public AnimationCurve AnimationSpeenOnTime;
    public float MaxTime = 1;

    private Animator myAnimator;
    private Transform root;
    private float oldSpeed;
    private float time;
    void GetAnimatorOnParent(Transform t)
    {
        var anim = t.parent.GetComponent<Animator>();
        if (anim == null)
        {
            if (root == t.parent) return;
            GetAnimatorOnParent(t.parent);
        }
        else
            myAnimator = anim;
    }

    void Start()
    {
        root = transform.root;
        GetAnimatorOnParent(transform);
        if (myAnimator == null) return;
        oldSpeed = myAnimator.speed;
    }

    void Update()
    {
        if (myAnimator == null || AnimationSpeenOnTime.length == 0) return;
        time += Time.deltaTime;
        myAnimator.speed = AnimationSpeenOnTime.Evaluate(time / MaxTime) * oldSpeed;
    }
}
