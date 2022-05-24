using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum MinionAbility {
    Disguise = 1, // looks like another minion type
    Jumper = 2, // JumpOtherRoad
    MoleWalker = 3, //pass 1st enemy
    Runner = 4, // x3 speed
};

[CreateAssetMenu(menuName = "ScriptableObjects/MinionAbility")]
public class MinionAbilitySO : ScriptableObject {

    [SerializeField] string abilityName; public string AbilityName => abilityName;
    [SerializeField] MinionAbility minionAbility; public MinionAbility MinionAbility => minionAbility;
    [SerializeField] private bool isActive; public bool IsActive => isActive;

    //public MinionAbility? GetAbilityIsActive()
    //{
    //    return isActive ? MinionAbility : null;
    //}

}
