using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    private EnemySpawner[] enemySpawners;

    [SerializeField]
    private EnemyEntity[] enemies;

    [SerializeField]
    private float timeBetweenSpawn = 5;

    [SerializeField]
    float lastSpawnTime = float.NegativeInfinity;

    [Header("Difficulty")]
    [SerializeField]
    private AnimationCurve killToCostIncrease;

    [Header("Counters")]
    [SerializeField]
    private int numberOfKills = 0;

    [SerializeField]
    private int currentlyAlive = 0;

    [SerializeField]
    private float availableCost = 1;

    [SerializeField]
    private float totalCost = 0;

    private float minCost = Single.PositiveInfinity;

    public static EnemyManager current;


    private void Awake()
    {
        current = this;
        totalCost = availableCost;
        foreach (EnemyEntity e in enemies)
        {
            if (e.Cost < minCost)
            {
                minCost = e.Cost;
            }
        }
    }

    private void FixedUpdate()
    {
        if (availableCost > Math.Max(minCost, availableCost / 4f))
        {
            if (Time.time - lastSpawnTime > timeBetweenSpawn)
            {
                EnemyEntity currentEnemy;
                while (availableCost > minCost)
                {
                    currentEnemy = GetEnemyToSpawn();
                    if (currentEnemy)
                    {
                        SpawnEnemy(currentEnemy);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    public static void AddKill_S(int cost)
    {
        current.AddKill(cost);
    }

    public void AddKill(int cost)
    {
        numberOfKills += 1;
        currentlyAlive -= 1;
        availableCost += cost;
        availableCost += killToCostIncrease.Evaluate(numberOfKills);
        totalCost += killToCostIncrease.Evaluate(numberOfKills);
    }

    EnemyEntity GetEnemyToSpawn()
    {
        List<EnemyEntity> currentCanSpawn = new List<EnemyEntity>(enemies);
        EnemyEntity currentEnemy = currentCanSpawn[Random.Range(0, currentCanSpawn.Count)];
        while (currentEnemy.Cost > availableCost)
        {
            currentCanSpawn.Remove(currentEnemy);
            if (currentCanSpawn.Count == 0)
            {
                Debug.Log("Can't find enemy to spawn");
                return null;
            }

            currentEnemy = currentCanSpawn[Random.Range(0, currentCanSpawn.Count)];
        }

        return currentEnemy;
    }

    void SpawnEnemy(EnemyEntity e)
    {
        Vector3 currentPoint = enemySpawners[Random.Range(0, enemySpawners.Length)].GetRandomPoint();
        Instantiate(e.gameObject, currentPoint,
            Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
        availableCost -= e.Cost;
        currentlyAlive += 1;
    }
}