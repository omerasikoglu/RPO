using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

//TODO: Split ManaManager and ManaGenerator
public class ManaManager : Singleton<ManaManager> {

    [SerializeField] private GeneralOptionsSO options;

    public event EventHandler OnManaAmountChanged;

    private float currentMana = 0f;
    private float manaRegenTimer = .5f;

    //public void Update() {

    //    if (currentMana >= maxMana) return;

    //    currentMana += manaRegenTimer * Time.deltaTime;
    //    if (currentMana >= maxMana) currentMana = maxMana;

    //}



    //[Button]
    //public void SpendMana(int manaAmount = 1) {

    //    if (!HaveEnoughMana(manaAmount)) return;

    //    bool HaveEnoughMana(int spellManaAmount) {
    //        return currentMana >= spellManaAmount;
    //    }

    //    currentMana -= manaAmount;
    //    currentMana = Mathf.Clamp(currentMana, 0, maxMana);

    //}


}
