using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MinionOptions")]
public class MinionOptionsSO : ScriptableObject {
    [SerializeField] private Transform minion; public Transform Minion => minion;
    [SerializeField] private TeamEnum team; public TeamEnum Team => team;

    public float movementSpeed = 5f, scale = 1, damageAmount = 2f, maxHealth = 2, maxMana = 2f;

    #region Ability variables
    [SerializeField] private float speedIncreaseAmount = 5f; public float SpeedIncreaseAmount => speedIncreaseAmount;

    #endregion


}
