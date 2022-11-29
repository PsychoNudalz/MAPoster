using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{

    public virtual void DealDamage(LifeSystem ls, float damage)
    {
        ls.TakeDamage(damage);
    }
}