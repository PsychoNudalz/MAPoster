using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Vector2 startDir;

    private float speedCurrent;

    [SerializeField]
    private float speedStart;


    Rigidbody2D rb;

    [Header("Components")]
    [SerializeField]
    private LifeSystem lifeSystem;

    [SerializeField]
    private PlayerEffects playerEffects;

    [SerializeField]
    private MovementPredictionScript movementPredictionScript;

    [SerializeField]
    private PlayerAttackController playerAttackController;
    
    public static PlayerController current;


    private void Awake()
    {
        current = this;
        if (startDir.magnitude < .05f)
        {
            startDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            startDir = startDir.normalized;
        }
        if (!lifeSystem)
        {
            lifeSystem = GetComponent<LifeSystem>();
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (startDir.normalized * speedStart);

        if (!playerEffects)
        {
            playerEffects = GetComponent<PlayerEffects>();
        }

        if (!movementPredictionScript)
        {
            movementPredictionScript = GetComponentInChildren<MovementPredictionScript>();
        }

        if (!playerAttackController)
        {
            playerAttackController = GetComponentInChildren<PlayerAttackController>();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedCurrent = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        // movementPredictionScript.SetPointsRaycast(transform.position,rb.velocity.normalized);

    }

    public void TakeDamage(float f)
    {
        lifeSystem?.TakeDamage(f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        movementPredictionScript.SetPointsRaycast(transform.position,rb.velocity.normalized);

        // StartCoroutine(DelaySetLine());
    }

    IEnumerator DelaySetLine()
    {
        yield return new WaitForSeconds(.1f);

    }

    public void AddAttack(AttackSet attackSet)
    {
        playerAttackController.AddAttack(attackSet);
    }

    public static void Static_AddAttack(AttackSet attackSet)
    {
        current.AddAttack(attackSet);
    }
}