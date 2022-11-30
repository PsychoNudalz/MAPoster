using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum AttackSetState
{
    On,
    Off
}
public class AttackSet : MonoBehaviour
{
    private AttackSetState attackSetState = AttackSetState.On;
    [SerializeField]
    private float attackDuration;

    private float startTime;
    
    [SerializeField]
    private Transform parent = null;

    [SerializeField]
    private AttackSet playerAttackSet = null;

    [Header("On Active")]
    [SerializeField]
    private UnityEvent onOnEvent;
    
    [SerializeField]
    private UnityEvent onOffEvent;
    
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
        if (attackDuration == 0)
        {
            return false;
        }
        return (Time.time - startTime > attackDuration);
    }

    public void SetActive(bool b)
    {
        if (b)
        {
            attackSetState = AttackSetState.On;
            onOnEvent.Invoke();

        }
        else
        {
            attackSetState = AttackSetState.Off;
            onOffEvent.Invoke();

        }
    }
    
}