using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public enum PlayerState
{
    Alive,
    Dead
}

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState = PlayerState.Alive;

    [SerializeField]
    Vector2 startDir;

    private float speedCurrent;

    [SerializeField]
    private Vector2 speedRange = new Vector2(10, 15);

    [SerializeField]
    private AnimationCurve speedCurve;


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
        rb.velocity = (startDir.normalized * speedRange.x);

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
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Alive)
        {
            rb.velocity = rb.velocity.normalized* speedCurrent;
        }
    }

    private void FixedUpdate()
    {
        // movementPredictionScript.SetPointsRaycast(transform.position,rb.velocity.normalized);
    }

    void UpdateSpeed()
    {
        speedCurrent = (speedCurve.Evaluate(EnemyManager.GetNumberOfKills()) * (speedRange.y - speedRange.x)) +
                       speedRange.x;
        
    }

    public void TakeDamage(float f)
    {
        lifeSystem?.TakeDamage(f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        movementPredictionScript.SetPointsRaycast(transform.position, rb.velocity.normalized);
        CameraController.ShakeCamera_S(0.15f,.15f);
        // StartCoroutine(DelaySetLine());
    }

    IEnumerator DelaySetLine()
    {
        yield return new WaitForSeconds(.1f);
    }

    public void OnDeath()
    {
        GameManager.GameOver_S();
        playerEffects.OnDeathEvent();
        playerState = PlayerState.Dead;
        rb.bodyType = RigidbodyType2D.Static;
        foreach (Collider2D componentsInChild in GetComponentsInChildren<Collider2D>())
        {
            componentsInChild.enabled = false;
        }
    }


    public void AddAttack(AttackSet attackSet)
    {
        playerAttackController.AddAttack(attackSet);
    }

    public static void Static_AddAttack(AttackSet attackSet)
    {
        current.AddAttack(attackSet);
    }

    public static void Static_UpdateSpeed()
    {
        current.UpdateSpeed();
    }
}