using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger2DDamage : DamageScript
{
    [Header("Trigger Damage")]


    [SerializeField]
    protected float timeBetweenTick = 0;

    [SerializeField]
    private List<LifeSystem> lifeSystems;




    [SerializeField]
    private float timeNow = 0;

    private void Update()
    {
        if (lifeSystems.Count > 0)
        {
            timeNow += Time.deltaTime;

            if (timeNow >= timeBetweenTick)
            {
                DealDamage(null,damagePerSecond*timeNow);
                timeNow = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (damageTags.Contains(col.tag))
        {
            if (col.TryGetComponent(out LifeSystem ls))
            {
                lifeSystems.Add(ls);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (damageTags.Contains(col.tag))
        {
            if (col.TryGetComponent(out LifeSystem ls))
            {
                if (lifeSystems.Contains(ls))
                {
                    lifeSystems.Remove(ls);
                    if (lifeSystems.Count == 0)
                    {
                        timeNow = 0;
                    }
                }
            }
        }
    }

    public override void DealDamage(LifeSystem ls, float damage)
    {
        foreach (LifeSystem lifeSystem in lifeSystems)
        {
            base.DealDamage(lifeSystem, damage);
        }
    }
}