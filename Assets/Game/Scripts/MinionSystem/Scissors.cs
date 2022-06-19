using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Minion {

    public static Scissors Create(Vector3 spawnPos) {

        //Transform scissorsPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetUnit(UnitType.scissors);
        Transform scissorsPrefab = MinionFactory.Instance.PullUnit(UnitType.scissors).transform;
        Transform minionTransform = Instantiate(scissorsPrefab, spawnPos, Quaternion.identity);

        Scissors scissors = minionTransform.GetComponent<Scissors>();
        scissors.Init();

        return scissors;
    }
    private void Awake() {
        SetMinionType(UnitType.scissors);
    }
    private void OnTriggerEnter(Collider collision) {

        IDamageable damageable = collision.GetComponentInParent<IDamageable>();

        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;

        switch (damageable.GetMinionType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)2: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)3: CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)4: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)5: CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            damageable.TakeDamage(enemyHurt, GetCurrentScaleMultiplier());
            TakeDamage(youHurt, damageable.GetCurrentScaleMultiplier());
        }
    }
}
