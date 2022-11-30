using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> cachedPoints;

    [SerializeField]
    private int cashedPoints_Size;

    [SerializeField]
    private Transform spawnZone;

    [SerializeField]
    private Transform[] spawnCheckPoints;

    [SerializeField]
    private LayerMask invalidLayers;

    private void Awake()
    {
        if (!spawnZone)
        {
            spawnZone = transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 cachedPoint in cachedPoints)
        {
            Gizmos.DrawSphere(cachedPoint,.1f);
        }
    }


    [ContextMenu("GenerateCache")]
    public void GenerateCache()
    {
        int trapCount = 0;
        int trapCount_MAX = cashedPoints_Size;
        cachedPoints = new List<Vector3>();
        Vector3 current;
        for (int i = 0; i < cashedPoints_Size; i++)
        {
            trapCount = 0;
            current = GetRandomPointInSpawnZone();
            while (!CheckPoint(current) && trapCount < trapCount_MAX)
            {
                current = GetRandomPointInSpawnZone();
            }
            cachedPoints.Add(current);
        }
    }

    Vector3 GetRandomPointInSpawnZone()
    {
        Vector3 randomPoint = new Vector3();
        var lossyScale = spawnZone.lossyScale;
        randomPoint.x += Random.Range(-lossyScale.x / 2f, lossyScale.x / 2f);
        randomPoint.y += Random.Range(-lossyScale.y / 2f, lossyScale.y / 2f);
        randomPoint = Quaternion.AngleAxis(spawnZone.eulerAngles.z, spawnZone.forward) * randomPoint;
        randomPoint += spawnZone.position;
        print("Found: "+randomPoint);
        return randomPoint;
    }

    bool CheckPoint(Vector3 point)
    {
        Vector3 dir;
        foreach (Transform spawnCheckPoint in spawnCheckPoints)
        {
            dir = point -spawnCheckPoint.position;
            if (!Physics2D.Raycast(spawnCheckPoint.position, dir.normalized, dir.magnitude, invalidLayers))
            {
                return true;
            }
        }


        return false;
    }
}