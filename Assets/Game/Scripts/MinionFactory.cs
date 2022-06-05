using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Get currentRoad and minionType with EventArgs, Observer

public class MinionFactory : Singleton<MinionFactory> {


    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private MinionTypeListSO minionTypeList;

    private Vector3 GetMateSpawnPos(Road spawnPoint) => spawnPoint switch
    {
        (Road)1 => spawnPositionList[0]?.position ?? new Vector3(-2f, 0f, 2f),
        (Road)2 => spawnPositionList[1]?.position ?? new Vector3(2f, 0f, 2f),
        _ => Vector3.zero,
    };

    [Button]
    private void SpawnRock() {
        Rock.Create(GetMateSpawnPos((Road)1));
    }
    [Button]
    private void SpawnPaper() {
        Paper.Create(GetMateSpawnPos((Road)1));
    }
    [Button]
    private void SpawnScissors() {
        Scissors.Create(GetMateSpawnPos((Road)1));
    }
    [Button]
    private void SpawnOctopus() {
        Octopus.Create(GetMateSpawnPos((Road)1));
    }

}
