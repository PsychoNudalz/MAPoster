using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosiveDamage : DamageScript
{
    [Header("Explosive")]
    [SerializeField]
    private AnimationCurve damageFallOff;

    [SerializeField]
    private float damageRadius;

    private void Start()
    {
        if (needLineOfSight && LOSRange == 0)
        {
            LOSRange = damageRadius;
        }
    }


    [ContextMenu("Explode")]
    public void Explode()
    {
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, damageRadius, transform.up, damageRadius, damageLayers);

        foreach (RaycastHit2D hit2D in hits)
        {
            if (hit2D.collider.TryGetComponent(out LifeSystem ls))
            {
                if (damageTags.Contains(ls.tag))
                {
                    if (!needLineOfSight || (needLineOfSight && isLOS(ls)))
                    {
                        DealDamage(ls,damagePerSecond);
                    }
                    
                }
            }
        }
    }

    public override void DealDamage(LifeSystem ls, float damage)
    {
        float dis = Vector2.Distance(transform.position, ls.transform.position);
        damage *= damageFallOff.Evaluate(dis / damageRadius);
        base.DealDamage(ls, damage);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(.2f, .2f, .2f, .2f);
        Gizmos.DrawSphere(transform.position,damageRadius);
    }
}
