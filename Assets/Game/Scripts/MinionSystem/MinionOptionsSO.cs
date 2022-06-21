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

    public Team team;

    [SerializeField]
    private float
        movementSpeed = 5f,
        defaultDamage = 2f, defaultHealth = 2, defaultMana = 2f,
        maxDamage = 4f, maxHealth = 4f, maxMana = 4f;
    public float DefaultMovementSpeed => movementSpeed;
    public float DefaultHealth => defaultHealth;
    public float DefaultMana => defaultMana;
    public float DefaultDamage => defaultDamage;
    public float MaxHealth => maxHealth;
    public float MaxMana => maxMana;
    public float MaxDamage => maxDamage;

    [SerializeField] private bool isScaleRandomnessIsActive; public bool IsScaleRandomnessActive => isScaleRandomnessIsActive;
    [SerializeField, DisableIf(nameof(IsScaleRandomnessActive))] private Scale scale; public Scale DefaultScale => scale;


    #region Spell variables
    //[SerializeField] private float speedIncreaseAmount = 5f; public float SpeedIncreaseAmount => speedIncreaseAmount;
    #endregion


}
