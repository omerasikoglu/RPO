using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
public class HealthBarUI : BarUI {

    [SerializeField] private HealthManager healthManager;

    protected override void Awake() {
        base.Awake();
        Init();

        void Init() {
            maxValue = options.MaxHealth;
            currentValue = maxValue;
        }
    }
    public void OnEnable() {
        healthManager.OnDamaged += HealthManager_OnDamaged;
        healthManager.OnYouDied += HealthManager_OnYouDied;
    }
    public void OnDisable() {
        healthManager.OnDamaged -= HealthManager_OnDamaged;
        healthManager.OnYouDied -= HealthManager_OnYouDied;
    }

    [Button]
    private void HealthManager_OnDamaged() {
        currentValue -= 1;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        colorfulBar.DOScaleX(fillSpeed * currentValue, 1f);
    }
    private void HealthManager_OnYouDied() {
        // ? Whole bar drops
    }

}
