using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Minion {

    //public static Rock Create(Vector3 spawnPos, Team team) {

    //    Transform rockPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetUnit(UnitType.rock).transform;
    //    //Transform rockPrefab = MinionFactory.Instance.PullUnit(UnitType.rock, team).transform;
    //    Transform minionTransform = Instantiate(rockPrefab, spawnPos, Quaternion.identity);

    //    Rock rock = minionTransform.GetComponent<Rock>();
    //    rock.Init();

    //    return rock;
    //}

    private void Awake() {
        SetUnitType(UnitType.rock);
    }

    private void OnTriggerEnter(Collider collision) {  // BUG: cant colliding when inherit from base class

        IDamageable damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;
        //Debug.Log(GetUnitType());

        switch (damageable.GetUnitType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: Debug.Log($"{GetUnitType()} {(UnitType)1}a girdi"); CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)2: Debug.Log($"{GetUnitType()} {(UnitType)2}a {DamageQuality.poor} girdi"); CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)3: Debug.Log($"{GetUnitType()} {(UnitType)3}a girdi"); CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)4: Debug.Log($"{GetUnitType()} {(UnitType)4}a girdi"); CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)5: Debug.Log($"{GetUnitType()} {(UnitType)5}a girdi"); CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            float scaleMultiplier = damageable.GetCurrentScaleMultiplier();
            damageable.TakeDamage(enemyHurt, GetCurrentScaleMultiplier());
            TakeDamage(youHurt, scaleMultiplier);
        }
    }
}
