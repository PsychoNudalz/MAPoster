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
    
    [Header("Physics Material")]
    [SerializeField]
    private PhysicsMaterial2D physiocsMat;

    [SerializeField]
    private float friction;

    [SerializeField]
    private float bounciness;

    [SerializeField]
    private Rigidbody2D rb;

    [Header("Material")]
    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private Material rendererMat;

    [SerializeField]
    private AnimationCurve matCurve;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
        }

        rendererMat = renderer.material;
    }

    void Start()
    {
        physiocsMat = new PhysicsMaterial2D();
        physiocsMat.friction = friction;
        physiocsMat.bounciness= bounciness;
        rb.sharedMaterial = physiocsMat;
        GetComponent<Collider2D>().sharedMaterial = physiocsMat;
    }

    // Update is called once per frame
    void Update()
    {
        timeNow += Time.deltaTime * speed;
        rendererMat.SetFloat("_GradientTime",timeNow);
        SetMat(frictionCurve.Evaluate(timeNow),bouncinessCurve.Evaluate(timeNow));
    }

    void SetMat(float f,float b)
    {
        friction = f;
        bounciness = b;
        physiocsMat.friction = friction;
        physiocsMat.bounciness = bounciness;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        timeNow = 0;
    }
}