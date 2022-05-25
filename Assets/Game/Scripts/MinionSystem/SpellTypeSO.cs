using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum SpellTypeEnum {
    Disguise = 1, // looks like another minion type
    Jumper = 2, // JumpOtherRoad
    MoleWalker = 3, //pass 1st enemy
    Runner = 4, // x3 speed
    Ghost = 5, // reveal herself when close-up to enemy
};

[CreateAssetMenu(menuName = "ScriptableObjects/SpellType")]
public class SpellTypeSO : ScriptableObject {

    [SerializeField] string SpellName; public string spellName => SpellName;
    [SerializeField] SpellTypeEnum spellType; public SpellTypeEnum SpellType => spellType;
    [SerializeField] private bool isActive; public bool IsActive => isActive;
    [SerializeField] private int manaCost; public int ManaCost => manaCost;

    //public SpellTypeEnum? GetAbilityIsActive()
    //{
    //    return isActive ? SpellTypeEnum : null;
    //}

}
