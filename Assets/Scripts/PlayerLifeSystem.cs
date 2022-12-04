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
        CameraController.ShakeCamera_S(0.05f,0.2f);

        return base.TakeDamage(f);
    }
    
    
}
