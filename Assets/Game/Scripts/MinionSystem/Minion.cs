using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minion : MonoBehaviour {

    public event Action OnDamageTaken, OnDead;

    #region Enums
    protected enum SizeEnum { small = 1, normal = 2, big = 3 };
    private SizeEnum minionSize;
    protected enum DamageTypeEnum { poor = 1, normal = 2, critical = 3, instaDeath = 4 };
    protected enum MovementDirectionEnum { forward = 1, backward = 2 };
    #endregion

    [SerializeField] protected MinionOptionsSO options;

    private float currentHealth, currentMana;

    #region Static
    [SerializeField] private static List<Transform> spawnPositionList;
    protected static Vector3 GetSpawnPosition(SpawnPointEnum spawnPoint) {
        return spawnPoint switch
        {
            (SpawnPointEnum)1 => spawnPositionList[0].position != null ? spawnPositionList[0].position : new Vector3(-2f, 0f, 2f),
            (SpawnPointEnum)2 => spawnPositionList[1].position != null ? spawnPositionList[1].position : new Vector3(-2f, 0f, 2f),
        };
    }
    #endregion

    #region Public
    public void TakeDamage(float damageAmount) {

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, options.maxHealth);
        OnDamageTaken?.Invoke();

        if (currentHealth <= 0f) Dead(); void Dead() {

            OnDead?.Invoke();

        }

    }
    public void GainAbilty(SpellTypeEnum minionAbility) {

        switch (minionAbility) {
            case SpellTypeEnum.Disguise: break;
            case SpellTypeEnum.Jumper: break;
            case SpellTypeEnum.MoleWalker: break;
            case SpellTypeEnum.Runner: options.movementSpeed += options.SpeedIncreaseAmount; break;
            case SpellTypeEnum.Ghost: break;
        }
    }
    public TeamEnum GetTeam() {
        return options.Team;
    }
    #endregion


    protected void SetMinionSize() {

        minionSize = UnityEngine.Random.value switch
        {
            < .01f => SizeEnum.small,
            > .99f => SizeEnum.big,
            _ => SizeEnum.normal
        };

        SetStats(minionSize);

        void SetStats(SizeEnum size) {

            switch (size) {
                case SizeEnum.small: SetChangeAmounts(.5f, .5f, .75f); break;
                case SizeEnum.normal: SetChangeAmounts(1f, 1f, 1f); break;
                case SizeEnum.big: SetChangeAmounts(2f, 2f, 1.25f); break;
            }

            void SetChangeAmounts(float healthAmount, float manaAmount, float scaleAmount) {
                options.maxHealth *= healthAmount; options.maxMana *= manaAmount; options.scale *= scaleAmount;
            }
        }
    }
    protected float GetDamage(DamageTypeEnum damageType) {
        return damageType switch
        {
            (DamageTypeEnum)1 => options.damageAmount * .5f,
            (DamageTypeEnum)2 => options.damageAmount,
            (DamageTypeEnum)3 => options.damageAmount * 2,
            (DamageTypeEnum)4 => options.damageAmount * 10,
            _ => options.damageAmount
        };
    }
    private void SetMovementDirection(MovementDirectionEnum direction) {
        switch (direction) {
            case MovementDirectionEnum.forward: break;
            case MovementDirectionEnum.backward:
            transform.DORotate(new Vector3(0f, 180f, 0f), 0f);
            break;
        }
    }
    protected virtual void OnTriggerEnter(Collider collision) {

        //Check is teammate
        Minion minion = collision.GetComponent<Minion>();
        if (minion != null && GetTeam() == minion.GetTeam()) return;

        Octopus octopus = collision.GetComponent<Octopus>();
        if (octopus != null) {
            octopus.TakeDamage(GetDamage(DamageTypeEnum.poor));
            GetDamage(DamageTypeEnum.critical);
        }

        HealthManager player = collision.GetComponent<HealthManager>();
        if (player != null) {
            player.GetDamage();
            GetDamage(DamageTypeEnum.instaDeath);
        }

    }

}
