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
        SetMinionType(MinionType.rock);
    }

    private void OnTriggerEnter(Collider collision) {

        #region base // BUG: cant colliding when inherit from base class

        HealthManager general = collision.attachedRigidbody.GetComponent<HealthManager>(); //general health
        if (general != null) {
            general.TakeDamage();
            TakeDamage(GetDamage(DamageQuality.instaDeath));
        }
        #endregion

        IDamageable damageable = collision.attachedRigidbody.GetComponent<IDamageable>();
       
        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;

        switch (damageable.GetMinionType()) {
            case (MinionType)1: CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (MinionType)2: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (MinionType)3: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (MinionType)4: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            damageable.TakeDamage(GetDamage(enemyHurt));
            TakeDamage(GetDamage(youHurt));
        }
    }

}
