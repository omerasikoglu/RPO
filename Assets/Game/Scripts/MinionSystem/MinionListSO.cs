using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MinionList")]
public class MinionListSO : ScriptableObject {

    public List<MinionTypeSO> list;
}
