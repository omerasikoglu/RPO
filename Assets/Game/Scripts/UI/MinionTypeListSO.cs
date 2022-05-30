using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for UI

[CreateAssetMenu(menuName = "ScriptableObjects/MinionList")]
public class MinionTypeListSO : ScriptableObject {

    [SerializeField] private List<MinionTypeSO> list;

    public List<MinionTypeSO> List => list;
    public Transform GetRock => GetMinionPrefab(0);
    public Transform GetPaper => GetMinionPrefab(1);
    public Transform GetScissors => GetMinionPrefab(2);
    public Transform GetOctopus => GetMinionPrefab(3);

    private Transform GetMinionPrefab(int minionIndex) => list[minionIndex].MinionPrefab;

}
