using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField]
    private float speed = .2f;

    [SerializeField]
    private float timeNow;

    [SerializeField]
    private AnimationCurve frictionCurve;
    [SerializeField]
    private AnimationCurve bouncinessCurve;
    
    [Header("Material")]
    [SerializeField]
    private PhysicsMaterial2D mat;

    [SerializeField]
    private float friction;

    [SerializeField]
    private float bounciness;

    [SerializeField]
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = new PhysicsMaterial2D();
        mat = rb.sharedMaterial;
        mat.friction = friction;
        mat.bounciness= bounciness;
    }

    // Update is called once per frame
    void Update()
    {
        timeNow += Time.deltaTime * speed;
        SetMat(frictionCurve.Evaluate(timeNow),bouncinessCurve.Evaluate(timeNow));
    }

    void SetMat(float f,float b)
    {
        friction = f;
        bounciness = b;
        mat.friction = friction;
        mat.bounciness = bounciness;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        timeNow = 0;
    }
}