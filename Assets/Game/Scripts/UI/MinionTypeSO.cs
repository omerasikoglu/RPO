using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MinionType {
    rock,
    paper,
    scissors,
    octopus
};

[CreateAssetMenu(menuName = "ScriptableObjects/MinionType")]
public class MinionTypeSO : ScriptableObject {
    [SerializeField] private string minionName; public string MinionName => minionName;
    [SerializeField] private MinionType minionType; public MinionType MinionType => minionType;
    [SerializeField] private Sprite sprite; public Sprite Sprite => sprite;
    [SerializeField] private int manaCost; public int ManaCost => manaCost;
    [SerializeField] private string colorHex; public string ColorHex => minionName;
    [SerializeField] private Transform minionPrefab; public Transform MinionPrefab => minionPrefab;

}
