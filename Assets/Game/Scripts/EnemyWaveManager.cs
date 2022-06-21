using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private float spawnTimerMax;
    private float currentSpawnTimer;

    public void Awake()
    {
        currentSpawnTimer = spawnTimerMax;
    }
    public void Update()
    {

        currentSpawnTimer -= Time.deltaTime;
        if (currentSpawnTimer <= 0f)
        {
            SpawnMinion();
            currentSpawnTimer += spawnTimerMax;
        }

    }

    private void SpawnMinion()
    {
        int i = Random.Range(0, 4);
        MinionFactory.Instance.PullUnit(UnitType.rock, Team.red);


    }
}
