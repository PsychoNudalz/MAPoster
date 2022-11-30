using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public enum ProjectileState
{
    moving,
    impacted
}

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileState projectileState = ProjectileState.moving;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private float lifetime = 5;

    // [SerializeField]
    // private float damage;


    [SerializeField]
    private string[] impactTags;

    [SerializeField]
    private bool impactOnTrigger = true;

    [SerializeField]
    private UnityEvent impactEvent;

    private void Start()
    {
        Destroy(gameObject,lifetime);
    }

    // [Header("Sprite")]
    private void Update()
    {
        switch (projectileState)
        {
            case ProjectileState.moving:
                UpdateMovement();
                UpdateRotation();
                break;
            case ProjectileState.impacted:

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateMovement()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
    }

    void UpdateRotation()
    {
        transform.up = dir.normalized;
    }

    public void LaunchProjectile(float s, Vector3 d)
    {
        speed = s;
        dir = d;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (impactTags.Contains(col.collider.tag))
        {
            OnImpact();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (impactOnTrigger)
        {
            if (impactTags.Contains(col.tag))
            {
                OnImpact();
            }
        }
    }

    public void OnImpact()
    {
        if (projectileState == ProjectileState.impacted)
        {
            return;
        }
        // print("Impacted");
        impactEvent.Invoke();
        projectileState = ProjectileState.impacted;
    }

    public void ChangeState(ProjectileState ps)
    {
        projectileState = ps;
    }
}