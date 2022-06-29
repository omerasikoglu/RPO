using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GeneralOptions")]
public class GeneralOptionsSO : ScriptableObject {
  
    [SerializeField] private Team team;

    [SerializeField] private float maxMana, maxHealth;

    public float MaxMana => maxMana;
    public float MaxHealth => maxHealth;
    public Team Team => team;
}
