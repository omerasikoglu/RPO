using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum Scale { small = 1, normal = 2, big = 3 };
public enum DamageQuality { one = 1, poor = 2, normal = 3, critical = 4, instaDeath = 5 };
public abstract class Minion : MonoBehaviour, IDamageable {

    //public static Minion Create(Transform pfTransform, Vector3 pos) {

    //    Transform minionTransform = Instantiate(pfTransform, pos, Quaternion.identity);

    //    Minion minion = minionTransform.GetComponent<Minion>();
    //    minion.Setup();

    //    return minion;
    //}

    public event Action OnDamageTaken, OnDead;

    [SerializeField] protected MinionOptionsSO options;

    [ShowNonSerializedField] private float currentHealth, currentMana, currentScale, currentMovementSpeed;
    [ShowNonSerializedField] private bool isImmune = false;
    [ShowNonSerializedField] protected UnitType minionType;

    protected virtual void Awake() {
        Setup();
    }
    private void Update() {
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

    protected virtual void SetMinionType(UnitType minionType) {
        this.minionType = minionType;
    }
    private Scale currentMinionScale;
    protected void SetScale() {

        currentMinionScale = UnityEngine.Random.value switch
        {
            < .01f => Scale.small,
            > .99f => Scale.big,
            _ => Scale.normal
        };

        SetStats(currentMinionScale);

        void SetStats(Scale scale) {

            switch (scale) {
                case Scale.small: SetChangeAmounts(.5f, .5f, .5f); break;
                case Scale.normal: SetChangeAmounts(1f, 1f, 1f); break;
                case Scale.big: SetChangeAmounts(2f, 2f, 1.5f); break;
                default: SetChangeAmounts(1f, 1f, 1f); break;
            }

            void SetChangeAmounts(float healthAmount, float manaAmount, float scaleAmount) {
                currentHealth *= healthAmount; currentMana *= manaAmount; currentScale = scaleAmount;
            }
        }

        UpdateScale();

        void UpdateScale() {
            if (currentScale.Equals(1)) return;

            transform.DOScale(currentScale, 1f);
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

    #region interface
    public void TakeDamage(DamageQuality damageQuality) {

        if (isImmune) return;

        float damageAmount = GetDamage(damageQuality);

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

    public UnitType GetMinionType() {
        return minionType;
    }
    public Team GetTeam() {
        return options.team;
    }

    #endregion


    #region Collision


    protected float GetDamage(DamageQuality damageQuality) {
        return damageQuality switch
        {
            (DamageQuality)1 => 1,
            (DamageQuality)2 => options.DefaultDamage * .5f,
            (DamageQuality)3 => options.DefaultDamage,
            (DamageQuality)4 => options.DefaultDamage * 2,
            (DamageQuality)5 => options.DefaultDamage * 10,
            _ => options.DefaultDamage
        };
    }
    private void SetImmunity(bool isImmune) {
        this.isImmune = isImmune;
    }





    #endregion

}
