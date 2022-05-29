using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

//TODO: Convert this to ManaGenerator
public class ManaBarUI : BarUI {

    private float manaRegenTimer = .5f;

    protected override void Awake() {
        base.Awake();
        currentValue = 0;
    }

    public void Update() {

        if (currentValue >= maxValue) return;

        currentValue += manaRegenTimer * Time.deltaTime;
        if (currentValue >= maxValue) currentValue = maxValue;
        greenBar.DOScaleX(multiplier * currentValue, .1f);

    }



    [Button]
    public void SpendMana(int manaAmount = 1) {

        if (!HaveEnoughMana(manaAmount)) return;

        bool HaveEnoughMana(int spellManaAmount) {
            return currentValue >= spellManaAmount;
        }

        currentValue -= manaAmount;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);

    }

}
