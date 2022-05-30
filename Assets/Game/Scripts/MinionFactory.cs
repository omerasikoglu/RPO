using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFactory : Singleton<MinionFactory> {


    [SerializeField] private List<Transform> spawnPositionList;

    protected Vector3 GetMateSpawnPos(RoadSpawnPoint spawnPoint) {
        return spawnPoint switch
        {
            (RoadSpawnPoint)1 => spawnPositionList[0] != null ? spawnPositionList[0].position : new Vector3(-2f, 0f, 2f),
            (RoadSpawnPoint)2 => spawnPositionList[1] != null ? spawnPositionList[1].position : new Vector3(2f, 0f, 2f),
            _ => new Vector3(-2f, 0f, 2f)
        };
    }


}
