using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum Scale { small = 1, normal = 2, big = 3 };
public enum DamageQuality { one = 1, poor = 2, normal = 3, critical = 4, instaDeath = 5 };
public abstract class Minion : MonoBehaviour, IDamageable, IPoolable<Minion> {

    //public static Minion Create(Transform pfTransform, Vector3 pos) {

    //    Transform minionTransform = Instantiate(pfTransform, pos, Quaternion.identity);

    //    Minion minion = minionTransform.GetComponent<Minion>();
    //    minion.Setup();

    //    return minion;
    //}

    private Action<Minion> OnReturnedToPool;
    public event Action OnDamageTaken, OnDead;

    [SerializeField] protected MinionOptionsSO options;

    [ShowNonSerializedField] private Scale currentScale;
    [ShowNonSerializedField] private float currentHealth, currentMana, currentDamage, currentMovementSpeed;
    [ShowNonSerializedField] private bool isImmune = false;
    [ShowNonSerializedField] protected UnitType minionType;
    [ShowNonSerializedField] private Team team = Team.green;

    protected virtual void OnEnable() {
        Init();
    }

    public void Init() {

        SetImmunity(false);
        GetDirection();
        SetMovementSpeed(options.DefaultMovementSpeed);

        //Change via scale value
        SetHealth(options.DefaultHealth);
        SetMana(options.DefaultMana);
        SetDamage(options.DefaultDamage);
        SetScale();

        void SetHealth(float health) {
            currentHealth = health;
        }
        void SetMana(float mana) {
            currentMana = mana;
        }
        void SetDamage(float damage) {
            currentDamage = damage;
        }
        void SetScale() {

            if (options.IsScaleRandomnessActive) {
                currentScale = UnityEngine.Random.value switch
                {
                    < .01f => Scale.small,
                    > .99f => Scale.big,
                    _ => Scale.normal
                };
            }
            else {
                currentScale = options.DefaultScale;
            }

            UpdateScale(); void UpdateScale() {
                //if (currentScale.Equals(Scale.normal)) return;

                transform.DOScale(GetScaleAmount(currentScale), 1f);

                float GetScaleAmount(Scale currentScale) {
                    return currentScale switch
                    {
                        Scale.small => 0.5f,
                        Scale.normal => 1f,
                        Scale.big => 1.5f,
                        _ => 1f,
                    };
                }
            }

            SetStats(currentScale); void SetStats(Scale currentScale) {

                switch (currentScale) {
                    case Scale.small: UpdateStats(.5f); break;
                    case Scale.normal: UpdateStats(1f); break;
                    case Scale.big: UpdateStats(2f); break;
                    default:
                    UpdateStats(1f); break;

                    void UpdateStats(float statMultiplier) {
                        currentHealth *= statMultiplier;
                        currentMana *= statMultiplier;
                        currentDamage *= statMultiplier;
                    }
                }
            }
        }

    }
    private void SetMovementSpeed(float speed) {
        currentMovementSpeed = speed;
    }
    private void SetImmunity(bool isImmune) {
        this.isImmune = isImmune;
    }
    private Vector3 GetDirection() {

        return Vector3.forward;
    }

    protected virtual void SetMinionType(UnitType minionType) {
        this.minionType = minionType;
    }

    public void GainAbilty(SpellTypeEnum minionAbility) {
        switch (minionAbility) {
            case SpellTypeEnum.Disguise: break;
            case SpellTypeEnum.Jumper: break;
            case SpellTypeEnum.MoleWalker: break;
            case SpellTypeEnum.Runner: /*currentMovementSpeed += options.SpeedIncreaseAmount;*/ break;
            case SpellTypeEnum.Ghost: break;
        }
    }

    private void OnDisable() {
        ReturnToPool();
    }
    private void Update() {
        transform.Translate(currentMovementSpeed * GetDirection() * Time.deltaTime);
    }




    #region Implements
    public float GetCurrentScaleMultiplier() {
        return currentScale switch
        {
            Scale.small => .5f,
            Scale.normal => 1f,
            Scale.big => 2f,
            _ => 1f,
        };
    }
    public void TakeDamage(DamageQuality damageQuality, float enemyScaleMultiplier = 1) {

        if (isImmune) return;

        float damageAmount = GetDamage(damageQuality) * enemyScaleMultiplier;

        float GetDamage(DamageQuality damageQuality) {
            return damageQuality switch
            {
                (DamageQuality)1 => 1f,
                DamageQuality.poor => 0.5f * currentDamage,
                DamageQuality.normal => 1 * currentDamage,
                (DamageQuality)4 => 2 * currentDamage,
                (DamageQuality)5 => 10 * currentDamage,
                _ => 1f,
            };
        }


        SetImmunity(true);

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, options.DefaultMana);
        OnDamageTaken?.Invoke();

        if (currentHealth <= 0f) Dead(); void Dead() {

            OnDead?.Invoke();
            //Destroy(gameObject);
            gameObject.SetActive(false);

        }

        if (transform.gameObject.activeInHierarchy) {
            StartCoroutine(UtilsClass.WaitForFixedUpdate(() => { SetImmunity(false); }));
        }

    }

    public UnitType GetMinionType() => minionType;
    public Team GetTeam() => team;

    public void SetTeam(Team team)
    {
        this.team = team;
    }
    public void Initialize(Action<Minion> returnAction) {
        OnReturnedToPool = returnAction;
    }
    public void ReturnToPool() {
        SetScaleToDefault(); void SetScaleToDefault() {
            currentScale = Scale.normal;
            transform.localScale = Vector3.one;
        }
        OnReturnedToPool?.Invoke(this);
    }

    #endregion
}
