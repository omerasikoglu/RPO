using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Paper : Minion {
    //public static Paper Create(Vector3 spawnPos, Team team) {

    //    //Transform paperPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetUnit(UnitType.paper);
    //    Transform paperPrefab = MinionFactory.Instance.PullUnit(UnitType.paper, team).transform;
    //    Transform minionTransform = Instantiate(paperPrefab, spawnPos, Quaternion.identity);

    //    Paper paper = minionTransform.GetComponent<Paper>();
    //    paper.Init();

    //    return paper;
    //}

    private void Awake() {
        SetUnitType(UnitType.paper);
    }

    private void OnTriggerEnter(Collider collision) {

        IDamageable damageable = collision.GetComponentInParent<IDamageable>();

        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;

        switch (damageable.GetUnitType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: Debug.Log($"{GetUnitType()} {(UnitType)1}a {DamageQuality.critical} girdi"); CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)2: Debug.Log($"{GetUnitType()} {(UnitType)2}a girdi"); CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)3: Debug.Log($"{GetUnitType()} {(UnitType)3}a girdi"); CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)4: Debug.Log($"{GetUnitType()} {(UnitType)4}a girdi"); CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)5: Debug.Log($"{GetUnitType()} {(UnitType)5}a girdi"); CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            float scaleMultipliyer = damageable.GetCurrentScaleMultiplier();
            damageable.TakeDamage(enemyHurt, GetCurrentScaleMultiplier());
            TakeDamage(youHurt, scaleMultipliyer);
        }
    }
}
