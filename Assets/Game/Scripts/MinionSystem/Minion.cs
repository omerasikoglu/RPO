using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public enum TeamEnum { red, green, blue };

public abstract class Minion : MonoBehaviour {

    public event Action OnDamageTaken, OnDead;

    #region Enums

    protected enum SizeEnum { small = 1, normal = 2, big = 3 };
    protected enum DamageTypeEnum { poor = 1, normal = 2, critical = 3 };
    protected enum MovementDirectionEnum { forward = 1, backward = 2 };

    #endregion

    [SerializeField] protected MinionOptionsSO options;

    private float currentHealth, currentMana;


    protected SizeEnum SetSize() {

        SizeEnum size = UnityEngine.Random.value switch
        {
            < .01f => SizeEnum.small,
            > .99f => SizeEnum.big,
            _ => SizeEnum.normal
        };

        SetStats(size);

        return size;

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
            case SpellTypeEnum.Runner:
            options.movementSpeed += options.SpeedIncreaseAmount;
            break;
        }
    }

    public TeamEnum GetTeam() {
        return options.Team;
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

    }

}
