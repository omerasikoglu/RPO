using System;
using UnityEngine;

[Serializable]
public enum SpellTypeEnum {
    Disguise = 1, // looks like another minion type
    Jumper = 2, // JumpOtherRoads
    MoleWalker = 3, //pass 1st enemy
    Runner = 4, // x3 speed
    Ghost = 5, // reveal herself when close-up to enemy
    RiseOfTheOctopuses = 6,       //sonraki 10 sn boyunca spawnlanan tüm minyonlarýn octopus olacak
    OnePunchMan =7, //generallere tek atar
};

[CreateAssetMenu(menuName = "ScriptableObjects/SpellType")]
public class SpellTypeSO : ScriptableObject {

    [SerializeField] string SpellName; public string spellName => SpellName;
    [SerializeField] SpellTypeEnum spellType; public SpellTypeEnum SpellType => spellType;
    [SerializeField] private Sprite sprite; public Sprite Sprite => sprite;
    [SerializeField] private bool isActive; public bool IsActive => isActive;
    [SerializeField] private int manaCost; public int ManaCost => manaCost;

    //public SpellTypeEnum? GetAbilityIsActive()
    //{
    //    return isActive ? SpellTypeEnum : null;
    //}

}
