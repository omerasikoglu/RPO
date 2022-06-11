using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UnitType {
    rock = 1,
    paper = 2,
    scissors = 3,
    octopus = 4,
    general = 5,
};

[CreateAssetMenu(menuName = "ScriptableObjects/MinionType")]
public class MinionTypeSO : ScriptableObject {
    [SerializeField] private string minionName; public string MinionName => minionName;
    [SerializeField] private UnitType minionType; public UnitType MinionType => minionType;
    [SerializeField] private Sprite sprite; public Sprite Sprite => sprite;
    [SerializeField] private int manaCost; public int ManaCost => manaCost;
    [SerializeField] private string colorHex; public string ColorHex => minionName;
    [SerializeField] private Transform minionPrefab; public Transform MinionPrefab => minionPrefab;
    [SerializeField] private GameObject minionGO; public GameObject MinionGO => minionGO;

}
