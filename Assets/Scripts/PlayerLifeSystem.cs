using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : LifeSystem
{
    [Header("Player")]
    [SerializeField]
    private PlayerEffects playerEffects;
    [SerializeField]
    private PlayerController playerController;

    public override bool TakeDamage(float f)
    {
        if (playerEffects)
        {
            playerEffects.OnTakeDamage();
        }
        return base.TakeDamage(f);
    }
    
    
}