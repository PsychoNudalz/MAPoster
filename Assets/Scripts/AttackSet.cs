using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSet : MonoBehaviour
{
    [SerializeField]
    private Transform parent = null;

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
        PlayerController.Static_AddAttack(this);
    }
    
}
