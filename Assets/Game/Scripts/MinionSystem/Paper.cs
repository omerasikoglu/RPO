using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Paper : Minion {
    public static Paper Create(Vector3 spawnPos, Team team) {

        //Transform paperPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetUnit(UnitType.paper);
        Transform paperPrefab = MinionFactory.Instance.PullUnit(UnitType.paper, team).transform;
        Transform minionTransform = Instantiate(paperPrefab, spawnPos, Quaternion.identity);

        Paper paper = minionTransform.GetComponent<Paper>();
        paper.Init();

        return paper;
    }

    private void Awake() {
        SetMinionType(UnitType.paper);
    }

    private void OnTriggerEnter(Collider collision) {

        IDamageable damageable = collision.GetComponentInParent<IDamageable>();

        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;

        switch (damageable.GetMinionType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)2: CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)3: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)4: CalculateCombat(DamageQuality.critical, DamageQuality.poor); break;
            case (UnitType)5: CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            damageable.TakeDamage(enemyHurt, GetCurrentScaleMultiplier());
            TakeDamage(youHurt, damageable.GetCurrentScaleMultiplier());
        }
    }
}
