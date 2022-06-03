using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageQuality damageQuality);
    Team GetTeam();
    UnitType GetMinionType();
}
