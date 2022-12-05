using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum ProjectileLauncherState
{
    CanFire,
    TimeBetween,
    Reload
}

enum FireState
{
    On,
    Off
}


public class ProjectileLaunchScript : MonoBehaviour
{
    [SerializeField]
    private ProjectileLauncherState projectileLauncherState = ProjectileLauncherState.CanFire;

    [SerializeField]
    private FireState fireState = FireState.Off;

    [Header("Stats")]
    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private float projectileSpeed = 1f;

    [SerializeField]
    private float timeBetweenFire = 1f;

    [SerializeField]
    private int projectilePerFire = 1;

    [SerializeField]
    private float reloadTime = 2f;

    [Space(5f)]
    [SerializeField]
    private UnityEvent onShoot;

    [Space(10f)]
    [Header("Components")]
    [SerializeField]
    private Transform[] firePoints;

    private int firePointIndex = 0;

    private float fireTimeNow = 0;
    private int currentProjectileCount = 0;


    private void Start()
    {
        if (firePoints.Length == 0)
        {
            firePoints = new Transform[1];
            firePoints[0] = transform;
        }
    }

    public void Update()
    {
        switch (projectileLauncherState)
        {
            case ProjectileLauncherState.CanFire:
                switch (fireState)
                {
                    case FireState.On:
                        Fire();
                        break;
                    case FireState.Off:
                        break;
                }

                break;
            case ProjectileLauncherState.TimeBetween:
                fireTimeNow += Time.deltaTime;
                if (fireTimeNow >= timeBetweenFire)
                {
                    projectileLauncherState = ProjectileLauncherState.CanFire;
                }

                break;
            case ProjectileLauncherState.Reload:
                break;
        }
    }

    public void SetFire(bool b)
    {
        if (b)
        {
            fireState = FireState.On;
        }
        else
        {
            fireState = FireState.Off;
        }
    }

    public void LaunchProjectile()
    {
        Projectile newProjectile = Instantiate(projectile.gameObject).GetComponent<Projectile>();
        newProjectile.transform.position = firePoints[firePointIndex].position;
        newProjectile.LaunchProjectile(projectileSpeed, firePoints[firePointIndex].up);
        firePointIndex = (firePointIndex + 1) % firePoints.Length;
        
        onShoot.Invoke();
    }

    public void Fire()
    {
        LaunchProjectile();
        currentProjectileCount += 1;
        if (currentProjectileCount >= projectilePerFire)
        {
            Reload();
        }
        else
        {
            fireTimeNow = 0;
            projectileLauncherState = ProjectileLauncherState.TimeBetween;
        }
    }


    public void Reload()
    {
        projectileLauncherState = ProjectileLauncherState.Reload;
        StartCoroutine(ReloadWait());
    }

    IEnumerator ReloadWait()
    {
        yield return new WaitForSeconds(reloadTime);
        currentProjectileCount = 0;
        fireTimeNow = timeBetweenFire + 1f;
        projectileLauncherState = ProjectileLauncherState.CanFire;
    }
}