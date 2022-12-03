using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum LaserState
// {
//     Charaging
// }
public class LaserController : MonoBehaviour
{
    [SerializeField]
    private EffectPlayer effectPlayer;

    [SerializeField]
    private GameObject[] laserGO;

    [Header("Stats")]
    [SerializeField]
    private float chargeTime;

    private float chargeTime_Now;
    
    
    [SerializeField]
    private float fireTime;

    float fireTime_Now;

    private bool isCharging = true;


    private void Start()
    {
        OnCharge();
    }

    private void Update()
    {
        if (isCharging)
        {
            chargeTime_Now += Time.deltaTime;
            if (chargeTime_Now > chargeTime)
            {
                OnFire();
            }
        }
        else
        {
            fireTime_Now += Time.deltaTime;
            if (fireTime_Now > fireTime)
            {
                OnCharge();
            }
        }
        
    }

    private void OnCharge()
    {
        fireTime_Now = 0f;
        foreach (GameObject o in laserGO)
        {
            o.SetActive(false);
            effectPlayer.PlayUE(0);
        }

        isCharging = true;
    }

    private void OnFire()
    {
        chargeTime_Now = 0f;
        foreach (GameObject o in laserGO)
        {
            o.SetActive(true);
            effectPlayer.PlayUE(1);

        }

        isCharging = false;
    }

    public void SetActive(bool b)
    {
        if (!b)
        {
            OnCharge();
        }

        enabled = b;
    }
}
