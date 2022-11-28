using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LifeState
{
    Alive,
    Dead
}

public class LifeSystem : MonoBehaviour
{
    [SerializeField]
    private LifeState lifeState = LifeState.Alive;

    [SerializeField]
    private float health_max = 100f;

    [SerializeField]
    private float health;

    [SerializeField]
    private AnimationCurve healthToDisplay;

    [Header("Components")]
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private SpriteRenderer healthSpriteRenderer;

    private void Start()
    {
        health = health_max;
        UpdateHealthShader(health);

    }

    public virtual void SwitchState(LifeState ls)
    {
        LifeState previousState = lifeState;
        lifeState = ls;
        print("Player: " + this + " " + previousState + " --> " + lifeState);
    }

    public bool TakeDamage(float f)
    {
        health -= f;
        if (health <= 0)
        {
            SwitchState(LifeState.Dead);
            health = 0;
        }

        UpdateHealthShader(health);

        return IsDead();
    }

    public bool IsDead()
    {
        return lifeState == LifeState.Dead;
    }

    
    [ContextMenu("UpdateHealth")]
    public void UpdateHealthShader(float h)
    {
        if (healthSpriteRenderer)
        {
            healthSpriteRenderer.material.SetFloat("_CircleStrength", healthToDisplay.Evaluate(h/health_max));
        }
    }
}