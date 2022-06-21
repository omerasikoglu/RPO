using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Get currentRoad and minionType with EventArgs, Observer
//TODO: Eliminate spawnPos from this script

public class MinionFactory : Singleton<MinionFactory> {

    [SerializeField] private List<Transform> greenSpawnPointList, redSpawnPointList;
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
            rockPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.rock), 5, poolRoot);
            paperPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.paper), 5, poolRoot);
            scissorsPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.scissors), 5, poolRoot);
            octopusPool = new ObjectPool<Minion>(minionTypeList.GetUnit(UnitType.octopus), 5, poolRoot);
            parentPool = new List<ObjectPool<Minion>> { rockPool, paperPool, scissorsPool, octopusPool };
        }
    }

    private Vector3 GetMateSpawnPos(Road spawnPoint) => spawnPoint switch
    {
        (Road)1 => greenSpawnPointList[0]?.position ?? new Vector3(-2f, 0f, 2f),
        (Road)2 => greenSpawnPointList[1]?.position ?? new Vector3(2f, 0f, 2f),
        _ => Vector3.zero,
    };

    private void SetSpawnPoint(Team team) {
        switch (team) {
            case Team.red:
            currentSpawnPosition = redSpawnPointList[0].position; break;
            case Team.green:
            currentSpawnPosition = greenSpawnPointList[0].position; break;
            default:
            throw new ArgumentOutOfRangeException(nameof(team), team, null);
        }
    }

    public GameObject PullUnit(UnitType unitType, Team team) {
        Vector3 rotation = GetRotation(team);
        SetSpawnPoint(team);

        Vector3 GetRotation(Team team) => team switch
        {
            Team.red => new Vector3(0f,180f,0f),
            Team.green => Vector3.zero,
            _ => Vector3.zero,
        };

        GameObject go = unitType switch
        {
            UnitType.rock => parentPool[0].PullGameObject(currentSpawnPosition),
            UnitType.paper => parentPool[1].PullGameObject(currentSpawnPosition),
            UnitType.scissors => parentPool[2].PullGameObject(currentSpawnPosition),
            UnitType.octopus => parentPool[3].PullGameObject(currentSpawnPosition),
            _ => throw new NotImplementedException(),
        };

        go.GetComponent<Minion>().SetTeam(team);
        go.transform.rotation = Quaternion.Euler(rotation);
        return go;
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
