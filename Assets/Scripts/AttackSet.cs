using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackSet : MonoBehaviour
{
    [SerializeField]
    private float attackDuration;

    private float startTime;
    
    [SerializeField]
    private Transform parent = null;

    [SerializeField]
    private AttackSet playerAttackSet = null;

    private void Start()
    {
        startTime = Time.time;
    }

    public Transform GetParent()
    {
        if (parent)
        {
            return parent;
        }

        else
        {
            return transform;
        }
    }

    public void TransferToPlayer()
    {
        if (playerAttackSet)
        {
            AttackSet newAttackSet = Instantiate(playerAttackSet.gameObject, transform.position, transform.rotation)
                .GetComponent<AttackSet>();
            PlayerController.Static_AddAttack(newAttackSet);
            gameObject.SetActive(false);
        }
        else
        {
            PlayerController.Static_AddAttack(this);
        }
    }

    public bool ExceededDuration()
    {
        return (attackDuration == 0 || Time.time - startTime > attackDuration);
    }
    
}