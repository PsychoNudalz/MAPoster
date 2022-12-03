using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EnemyEntity : EntityObject
{
    [Header("Enemy")]
    [SerializeField]
    private float score = 100f;

    [SerializeField]
    private int cost = 1;

    [SerializeField]
    private AttackSet[] attackSets;

    public int Cost => cost;

    // Start is called before the first frame update
    protected override void StartBehaviour()
    {
        base.StartBehaviour();
        SwitchState(EntityState.Placing);
    }

    protected override void SwitchState_Grabbed()
    {
        base.SwitchState_Grabbed();
        foreach (AttackSet attackSet in attackSets)
        {
            attackSet.SetActive(false);
        }
    }

    protected override void SwitchState_Placing()
    {
        base.SwitchState_Placing();

    }

    protected override void SwitchState_Placed()
    {
        base.SwitchState_Placed();
        foreach (AttackSet attackSet in attackSets)
        {
            attackSet.SetActive(true);
        }
    }


    public override void OnDeath()
    {
        if (entityState == EntityState.Death)
        {
            return;
        }
        SetAllMainBodyCollider(false);
        foreach (AttackSet attackSet in attackSets)
        {
            attackSet.SetActive(false);
        }

        base.OnDeath();
        GameManager.AddKill_S(score,cost);

    }

    public void OnDeath_Collision()
    {
        OnDeath();
        foreach (AttackSet set in attackSets)
        {
            set.TransferToPlayer();
        }
    }


}
