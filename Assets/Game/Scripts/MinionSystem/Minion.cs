using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using DG.Tweening;
using UnityEngine;

public abstract class Minion : MonoBehaviour {

    public event Action OnDamageTaken, OnDead;

    #region Enums
    public enum Team { red, green, blue };
    protected enum Size { small = 1, normal = 2, big = 3 };
    protected enum DamageType { poor = 1, normal = 2, critical = 3 }; 
    protected enum MovementDirection { forward = 1, backward = 2 }; 
    #endregion

    [SerializeField] protected Team team;
    [SerializeField] protected MinionSO options;

    private float currentHealth, currentMana;


    

    protected Size SetSize() {

        Size size = UnityEngine.Random.value switch
        {
            < .01f => Size.small,
            > .99f => Size.big,
            _ => Size.normal
        };

        SetStats(size);

        return size;

        void SetStats(Size size) {

            switch (size) {
                case Size.small: SetChangeAmounts(.5f, .5f, .75f); break;
                case Size.normal: SetChangeAmounts(1f, 1f, 1f); break;
                case Size.big: SetChangeAmounts(2f, 2f, 1.25f); break;
            }

            void SetChangeAmounts(float healthAmount, float manaAmount, float scaleAmount) {
                options.maxHealth *= healthAmount; options.maxMana *= manaAmount; options.scale *= scaleAmount;
            }
        }
    }

    protected float GetDamage(DamageType damageType) {
        return damageType switch
        {
            (DamageType)1 => options.damageAmount * .5f,
            (DamageType)2 => options.damageAmount,
            (DamageType)3 => options.damageAmount * 2,
            _ => options.damageAmount
        };
    }

    private void SetMovementDirection(MovementDirection direction) {
        switch (direction)
        {
            case MovementDirection.forward: break;
            case MovementDirection.backward:
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

    public void GainAbilty(MinionAbility minionAbility) {

        switch (minionAbility) {
            case MinionAbility.Disguise: break;
            case MinionAbility.Jumper: break;
            case MinionAbility.MoleWalker: break;
            case MinionAbility.Runner:
                options.movementSpeed += options.SpeedIncreaseAmount;
            break;
        }
    }

    public Team GetTeam() {
        return team;
    }

    protected virtual void OnTriggerEnter(Collider collision) {

        //Check is teammate
        Minion minion = collision.GetComponent<Minion>();
        if (minion != null && team == minion.GetTeam()) return;

        Octopus octopus = collision.GetComponent<Octopus>();
        if (octopus != null) {
            octopus.TakeDamage(GetDamage(DamageType.poor));
            GetDamage(DamageType.critical);
        }

    }

}
