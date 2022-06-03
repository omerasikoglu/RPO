using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HealthManager : MonoBehaviour, IDamageable {

    public event Action OnDamaged;
    public event Action OnYouDied;

    [SerializeField] private int maxHealth = 5;

    private float currentHealth;
    private bool isTooltipTimerActive = false;


    public void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageQuality damageQuality)
    {
        currentHealth -= 1;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamaged?.Invoke();

        if (currentHealth <= 0) {
            OnYouDied?.Invoke();
            Destroy(gameObject);
        }
    }

    public Team GetTeam()
    {
        throw new NotImplementedException();
    }

    public UnitType GetMinionType()
    {
        throw new NotImplementedException();
    }
}
