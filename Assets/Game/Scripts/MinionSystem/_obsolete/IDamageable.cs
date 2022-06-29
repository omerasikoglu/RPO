using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    void TakeDamage(DamageQuality damageQuality, float enemyScaleModifier = 1);
    float GetCurrentScaleMultiplier(); //only minion TODO: Seperate this inter
    Team GetTeam();
    UnitType GetUnitType();
}
