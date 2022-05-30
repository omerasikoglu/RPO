using UnityEngine;

public enum Team { red, green, blue };
public enum RoadSpawnPoint { road1 = 1, road2 = 2 };

[CreateAssetMenu(menuName = "ScriptableObjects/MinionOptions")]
public class MinionOptionsSO : ScriptableObject {
    [SerializeField] private Transform pfMinion; public Transform PfMinion => pfMinion;

    public Team team;
    public RoadSpawnPoint spawnPoint;

    [SerializeField] private float movementSpeed = 5f, scale = 1, damageAmount = 2f, maxHealth = 2, maxMana = 2f;
    public float DefaultMovementSpeed => movementSpeed;
    public float DefaultHealth => maxHealth;
    public float DefaultMana => maxMana;
    public float DefaultDamage => damageAmount;
    public float DefaultScale => scale;


    #region Spell variables
    [SerializeField] private float speedIncreaseAmount = 5f; public float SpeedIncreaseAmount => speedIncreaseAmount;

    #endregion


}
