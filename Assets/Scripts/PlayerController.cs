using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : EntityObject
{
    [SerializeField]
    Vector2 startDir;

    private float speedCurrent;

    [SerializeField]
    private float speedStart;
    
    
    Rigidbody2D rb;

    [Header("Components")]
    private LifeSystem lifeSystem;


    private void Awake()
    {
        lifeSystem = GetComponent<LifeSystem>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (startDir.normalized*speedStart);
    }

    // Update is called once per frame
    void Update()
    {
        speedCurrent = rb.velocity.magnitude;

    }

    public void TakeDamage(float f)
    {
        lifeSystem?.TakeDamage(f);
    }
}