using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HealthManager : MonoBehaviour {

    public event Action OnDamaged;
    public event Action OnYouDied;

    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    private bool isTooltipTimerActive = false;


    public void Awake() {
        currentHealth = maxHealth;
    }

    [Button]
    public void GetDamage(int damageAmount = 1) {

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamaged?.Invoke();

        if (currentHealth <= 0) {
            OnYouDied?.Invoke();
        }
    }



}
