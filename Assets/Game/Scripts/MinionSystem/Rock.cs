using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Minion {

    public static Rock Create(Vector3 spawnPos) {

        Transform rockPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetRock;
        Transform minionTransform = Instantiate(rockPrefab, spawnPos, Quaternion.identity);

        Rock rock = minionTransform.GetComponent<Rock>();
        rock.Setup();

        return rock;
    }

    protected override void Awake() {
        base.Awake();
        SetMinionType(UnitType.rock);
    }

    private void OnTriggerEnter(Collider collision) {  // BUG: cant colliding when inherit from base class

        IDamageable damageable = collision.attachedRigidbody.GetComponent<IDamageable>();
        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;
        Debug.Log("done");

        switch (damageable.GetMinionType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)2: CalculateCombat(DamageQuality.critical, DamageQuality.poor);
                 break;
            case (UnitType)3: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)4: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)5: CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            damageable.TakeDamage(enemyHurt);
            TakeDamage(youHurt);
        }
    }

}
