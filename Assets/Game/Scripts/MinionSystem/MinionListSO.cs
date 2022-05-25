using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MinionList")]
public class MinionListSO : ScriptableObject {

    [SerializeField] private List<MinionTypeSO> list;
    public List<MinionTypeSO> List => list;
}
