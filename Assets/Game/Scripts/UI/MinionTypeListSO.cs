using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for UI

[CreateAssetMenu(menuName = "ScriptableObjects/MinionList")]
public class MinionTypeListSO : ScriptableObject {

    [SerializeField] private List<MinionTypeSO> list;

    public List<MinionTypeSO> List => list;
    public GameObject GetUnit(UnitType unitType) => unitType switch
    {
        UnitType.rock => GetRock,
        UnitType.paper => GetPaper,
        UnitType.scissors => GetScissors,
        UnitType.octopus => GetOctopus,
        _ => throw new NotImplementedException(),
    };

    private GameObject GetRock => GetMinionPrefab(0);
    private GameObject GetPaper => GetMinionPrefab(1);
    private GameObject GetScissors => GetMinionPrefab(2);
    private GameObject GetOctopus => GetMinionPrefab(3);

    private GameObject GetMinionPrefab(int minionIndex) => list[minionIndex].MinionGO;




}
