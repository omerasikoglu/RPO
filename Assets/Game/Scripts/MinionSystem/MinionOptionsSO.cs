using UnityEngine;
using NaughtyAttributes;

//TODO: Seperate the Spells to Spell SOs
public enum Team { red, green, blue };

public enum Road : int {
    road1 = 1,
    road2 = 2,
};

[CreateAssetMenu(menuName = "ScriptableObjects/MinionOptions")]
public class MinionOptionsSO : ScriptableObject {

    //public Team team;

    [SerializeField] private float movementSpeed = 5f, damageAmount = 2f, maxHealth = 2, maxMana = 2f;
    public float DefaultMovementSpeed => movementSpeed;
    public float DefaultHealth => maxHealth;
    public float DefaultMana => maxMana;
    public float DefaultDamage => damageAmount;

    [SerializeField] private bool isScaleRandomnessIsActive; public bool IsScaleRandomnessActive => isScaleRandomnessIsActive;
    [SerializeField, DisableIf(nameof(IsScaleRandomnessActive))] private Scale scale; public Scale DefaultScale => scale;


    #region Spell variables
    //[SerializeField] private float speedIncreaseAmount = 5f; public float SpeedIncreaseAmount => speedIncreaseAmount;
    #endregion


}
