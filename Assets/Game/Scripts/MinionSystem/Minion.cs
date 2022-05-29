using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum SizeEnum { small = 1, normal = 2, big = 3 };
public enum DamageQualityEnum { poor = 1, normal = 2, critical = 3, instaDeath = 4 };
public abstract class Minion : MonoBehaviour {

    public static Minion Create(Transform pfTransform, Vector3 pos) {

        Transform minionTransform = Instantiate(pfTransform, pos, Quaternion.identity);

        Minion minion = minionTransform.GetComponent<Minion>();
        minion.Setup();

        return minion;
    }

    public event Action OnDamageTaken, OnDead;

    [SerializeField] protected MinionOptionsSO options;

    private SizeEnum minionScale;

    [ShowNonSerializedField] private float currentHealth, currentMana, currentScale, currentMovementSpeed;
    [ShowNonSerializedField] private bool isImmune = false;

    public Transform GetPrefab() => options.PfMinion;

    public void Awake() {
        Setup();
    }
    public void Update() {
        transform.Translate(currentMovementSpeed * GetDirection() * Time.deltaTime);
    }
    #region First Create
    protected void Setup() {

        GetDirection();
        SetHealth(options.DefaultHealth);
        SetMana(options.DefaultMana);
        SetMovementSpeed(options.DefaultMovementSpeed);
        SetScale();
    }


    protected void SetScale() {

        minionScale = UnityEngine.Random.value switch
        {
            < .01f => SizeEnum.small,
            > .99f => SizeEnum.big,
            _ => SizeEnum.normal
        };

        SetStats(minionScale);

        void SetStats(SizeEnum scale) {

            switch (scale) {
                case SizeEnum.small: SetChangeAmounts(.5f, .5f, .75f); break;
                case SizeEnum.normal: SetChangeAmounts(1f, 1f, 1f); break;
                case SizeEnum.big: SetChangeAmounts(2f, 2f, 1.25f); break;
                default: SetChangeAmounts(1f, 1f, 1f); break;
            }

            void SetChangeAmounts(float healthAmount, float manaAmount, float scaleAmount) {
                currentHealth *= healthAmount; currentMana *= manaAmount; currentScale *= scaleAmount;
            }
        }
    }
    public Vector3 GetDirection() {

        return Vector3.forward;
    }
    public void SetMovementSpeed(float speed) {
        currentMovementSpeed = speed;
    }
    public void SetMana(float mana) {
        currentMana = mana;
    }
    public void SetHealth(float health) {
        currentHealth = health;
    }
    public void GainAbilty(SpellTypeEnum minionAbility) {
        switch (minionAbility) {
            case SpellTypeEnum.Disguise: break;
            case SpellTypeEnum.Jumper: break;
            case SpellTypeEnum.MoleWalker: break;
            case SpellTypeEnum.Runner: currentMovementSpeed += options.SpeedIncreaseAmount; break;
            case SpellTypeEnum.Ghost: break;
        }
    }

    #endregion

    #region Collision
    public void TakeDamage(float damageAmount) {

        if (isImmune) return;

        SetImmunity(true);

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, options.DefaultMana);
        OnDamageTaken?.Invoke();

        if (currentHealth <= 0f) Dead(); void Dead() {

            OnDead?.Invoke();
            Destroy(gameObject);

        }

        StartCoroutine(UtilsClass.WaitForFixedUpdate(() => { SetImmunity(false); }));

    }
    public TeamEnum GetTeam() {
        return options.team;
    }

    protected float GetDamage(DamageQualityEnum damageQuality) {
        return damageQuality switch
        {
            (DamageQualityEnum)1 => options.DefaultDamage * .5f,
            (DamageQualityEnum)2 => options.DefaultDamage,
            (DamageQualityEnum)3 => options.DefaultDamage * 2,
            (DamageQualityEnum)4 => options.DefaultDamage * 10,
            _ => options.DefaultDamage
        };
    }
    protected virtual void OnTriggerEnter(Collider collision) {

        //Check is teammate
        Minion minion = collision.attachedRigidbody.GetComponent<Minion>();
        if (minion != null && GetTeam() == minion.GetTeam()) return;

        Octopus octopus = collision.attachedRigidbody.GetComponent<Octopus>();
        if (octopus != null) {
            octopus.TakeDamage(GetDamage(DamageQualityEnum.poor));
            GetDamage(DamageQualityEnum.critical);
        }

        HealthManager player = collision.attachedRigidbody.GetComponent<HealthManager>();
        if (player != null) {
            player.GetDamage();
            GetDamage(DamageQualityEnum.instaDeath);
        }

    }


    #endregion

    public void SetImmunity(bool isImmune) {
        this.isImmune = isImmune;
    }

    [Button]
    void ggg() {
        TakeDamage(GetDamage(DamageQualityEnum.poor));
    }

}
