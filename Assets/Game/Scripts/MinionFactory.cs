using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Get currentRoad and minionType with EventArgs, Observer
//TODO: Eliminate spawnPos from this script

public class MinionFactory : Singleton<MinionFactory> {



    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private MinionTypeListSO minionTypeList;
    [SerializeField] private Transform poolRoot;

    private ObjectPool<Minion> rockPool, paperPool, scissorsPool, octopusPool;
    private List<ObjectPool<Minion>> parentPool;

    private Vector3 currentSpawnPosition;

    public void Awake() {
        currentSpawnPosition = GetMateSpawnPos((Road)1);
    }
    public void OnEnable() {

        InitPools(); void InitPools() {
            rockPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.rock), 10, poolRoot);
            paperPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.paper), 10, poolRoot);
            scissorsPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.scissors), 10, poolRoot);
            octopusPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.octopus), 10, poolRoot);
            parentPool = new List<ObjectPool<Minion>> { rockPool, paperPool, scissorsPool, octopusPool };
        }

    }

    private Vector3 GetMateSpawnPos(Road spawnPoint) => spawnPoint switch
    {
        (Road)1 => spawnPositionList[0]?.position ?? new Vector3(-2f, 0f, 2f),
        (Road)2 => spawnPositionList[1]?.position ?? new Vector3(2f, 0f, 2f),
        _ => Vector3.zero,
    };

    

    public GameObject PullUnit(UnitType unitType, Team team)
    {
        Vector3 rotation = GetRotation(team);

        Vector3 GetRotation(Team team) => team switch
        {
            Team.red => Vector3.back,
            Team.green => Vector3.forward,
            _ => Vector3.forward,
        };

        return unitType switch
        {
            UnitType.rock => parentPool[0].PullGameObject(currentSpawnPosition, rotation),
            UnitType.paper => parentPool[1].PullGameObject(currentSpawnPosition, rotation),
            UnitType.scissors => parentPool[2].PullGameObject(currentSpawnPosition, rotation),
            UnitType.octopus => parentPool[3].PullGameObject(currentSpawnPosition, rotation),
            _ => throw new NotImplementedException(),
        };
    }

    #region Test
    //[Button]
    //private void SpawnRock() {
    //    //Rock.Create(GetMateSpawnPos((Road)1));
    //    rockPool.PullGameObject(currentSpawnPosition);
    //}
    //[Button]
    //private void SpawnBigRock() {
    //    //Rock.Create(GetMateSpawnPos((Road)1));
    //    rockPool.PullGameObject(currentSpawnPosition);
    //}
    //[Button]
    //private void SpawnPaper() {
    //    //Paper.Create(GetMateSpawnPos((Road)1));
    //    paperPool.PullGameObject(GetMateSpawnPos((Road)1));
    //}
    //[Button]
    //private void SpawnScissors() {
    //    //Scissors.Create(GetMateSpawnPos((Road)1));
    //    parentPool[2].PullGameObject(GetMateSpawnPos((Road)1));
    //}
    //[Button]
    //private void SpawnOctopus() {
    //    //Octopus.Create(GetMateSpawnPos((Road)1));
    //    parentPool[3].PullGameObject(GetMateSpawnPos((Road)1));
    //}

    #endregion
}
