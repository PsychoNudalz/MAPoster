using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private List<EnemyEntity> spawnedEnemies = new List<EnemyEntity>();

    [SerializeField]
    private float timeBetweenCheck = 5;

    [SerializeField]
    float lastCheckTime = float.NegativeInfinity;

    [SerializeField]
    private float spawnDelay = 0.5f;

    [SerializeField]
    private float spawnQueueDelay = 0.5f;

    struct SpawnSet
    {
        private EnemyEntity e;
        private Vector3 p;

        public EnemyEntity E => e;

        public Vector3 P => p;

        public SpawnSet(EnemyEntity e, Vector3 p)
        {
            this.e = e;
            this.p = p;
        }
    }

    [SerializeField]
    private Queue<SpawnSet> spawnQueue = new Queue<SpawnSet>();

    private Coroutine spawnQueueCoroutine;

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

    private float minimumSpawnCost;

    public static EnemyManager current;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem spawnEffects;


    public int NumberOfKills => numberOfKills;

    public static int GetNumberOfKills()
    {
        return current.numberOfKills;
    }

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

    private void Start()
    {
        minimumSpawnCost = minCost;
    }

    private void FixedUpdate()
    {
        if (Time.time - lastCheckTime > timeBetweenCheck || currentlyAlive + spawnQueue.Count == 0)
        {
            lastCheckTime = Time.time;

            if (availableCost > Math.Max(minCost, availableCost / 2f))
            {

                if (currentlyAlive != spawnedEnemies.Count)
                {
                    Debug.LogError("Alive to count not the same");
                }

                EnemyEntity currentEnemy;
                bool flag = true;
                while (flag && availableCost > minCost)
                {
                    currentEnemy = GetEnemyToSpawn();
                    if (currentEnemy)
                    {
                        AddStack(new SpawnSet(currentEnemy, GetRandomPoint()));
                        // SpawnEnemy(currentEnemy,GetRandomPoint());
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
        }
    }
    

    public static void AddKill_S(EnemyEntity e,int cost)
    {
        current.AddKill(e,cost);
    }

    public void AddKill(EnemyEntity e,int cost)
    {
        if (!spawnedEnemies.Contains(e))
        {
            Debug.LogError("Overkill: "+e);
            return;
        }
        spawnedEnemies.Remove(e);
        numberOfKills += 1;
        currentlyAlive -= 1;
        if (currentlyAlive < 0)
        {
            Debug.LogError("Negative alive");
        }

        ChangeCost(cost);
        IncreaseCostMax();
    }

    private void ChangeCost(int cost)
    {
        availableCost += cost;
        print("Change cost: " + cost+"  remaining: "+availableCost);
    }

    private void IncreaseCostMax()
    {
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

    void SpawnEnemy(EnemyEntity e, Vector3 currentPoint)
    {
        PlayEffect(currentPoint);

        StartCoroutine(DelayEnemySpawn(e, currentPoint));
    }

    private Vector3 GetRandomPoint()
    {
        return enemySpawners[Random.Range(0, enemySpawners.Length)].GetRandomPoint();
    }

    IEnumerator DelayEnemySpawn(EnemyEntity e, Vector3 currentPoint)
    {
        yield return new WaitForSeconds(spawnDelay);
        // float randomRotation = Random.Range(0, 360);
        // randomRotation = Mathf.Lerp(randomRotation, Vector3.Angle(transform.up, transform.position - currentPoint),
        // 1);
        Vector3 dir = (transform.position - currentPoint).normalized;
        float randomRotation = Vector3.SignedAngle(transform.up, dir, Vector3.forward);
        EnemyEntity newEnemy= Instantiate(e.gameObject, currentPoint,
            Quaternion.Euler(0, 0, randomRotation), transform).GetComponent<EnemyEntity>();
        newEnemy.gameObject.name += Time.time;
        spawnedEnemies.Add(newEnemy);
    }

    void PlayEffect(Vector3 pos)
    {
        spawnEffects.transform.position = pos;
        spawnEffects.Play();
    }

    // Stack Handling

    void AddStack(SpawnSet spawnSet)
    {
        ChangeCost(-spawnSet.E.Cost);

        currentlyAlive += 1;

        spawnQueue.Enqueue(spawnSet);
        if (spawnQueueCoroutine == null)
        {
            spawnQueueCoroutine = StartCoroutine(SpawnStackCoroutine());
        }
    }

    IEnumerator SpawnStackCoroutine()
    {
        SpawnSet ss = spawnQueue.Dequeue();
        SpawnEnemy(ss.E, ss.P);
        yield return new WaitForSeconds(spawnQueueDelay);
        if (spawnQueue.Count > 0)
        {
            spawnQueueCoroutine = StartCoroutine(SpawnStackCoroutine());
        }
        else
        {
            spawnQueueCoroutine = null;
        }
    }
}