using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFactory : Singleton<MinionFactory> {


    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private MinionTypeListSO minionTypeList;

    protected Vector3 GetMateSpawnPos(Roads spawnPoint) {
        return spawnPoint switch
        {
            (Roads)1 => spawnPositionList[0] != null ? spawnPositionList[0].position : new Vector3(-2f, 0f, 2f),
            (Roads)2 => spawnPositionList[1] != null ? spawnPositionList[1].position : new Vector3(2f, 0f, 2f),
            _ => new Vector3(-2f, 0f, 2f)
        };
    }

    [Button] private void SpawnRock()
    {
        Rock.Create(spawnPositionList[0].position);
    }
    [Button]
    private void SpawnPaper() {
        Paper.Create(spawnPositionList[0].position);
    }
    [Button]
    private void SpawnScissors() {
        Scissors.Create(spawnPositionList[0].position);
    }
    [Button]
    private void SpawnOctopus() {
        Octopus.Create(spawnPositionList[0].position);
    }

}
