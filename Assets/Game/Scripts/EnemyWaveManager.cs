using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private float spawnTimer;

    public void Update()
    {

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnMinion();
        }

    }

    private void SpawnMinion()
    {
        int i = Random.Range(0, 4);
        MinionFactory.Instance.PullUnit(UnitType.rock, Team.red);


    }
}
