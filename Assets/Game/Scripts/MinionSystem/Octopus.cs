using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Minion {
    public static Octopus Create(Vector3 spawnPos) {

        //Transform octopusPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetUnit(UnitType.octopus);
        Transform octopusPrefab = MinionFactory.Instance.PullUnit(UnitType.octopus).transform;
        Transform minionTransform = Instantiate(octopusPrefab, spawnPos, Quaternion.identity);

        Octopus octopus = minionTransform.GetComponent<Octopus>();
        octopus.Init();

        return octopus;
    }
    private void Awake() {
        SetMinionType(UnitType.octopus);
    }
    private void OnTriggerEnter(Collider collision) {

        IDamageable damageable = collision.GetComponentInParent<IDamageable>();

        if (damageable == null) return;
        if (GetTeam().Equals(damageable.GetTeam())) return;

        switch (damageable.GetMinionType()) { // ? could be get calculated values before the combat from some method
            case (UnitType)1: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)2: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)3: CalculateCombat(DamageQuality.poor, DamageQuality.critical); break;
            case (UnitType)4: CalculateCombat(DamageQuality.normal, DamageQuality.normal); break;
            case (UnitType)5: CalculateCombat(DamageQuality.instaDeath, DamageQuality.one); break;
        }

        void CalculateCombat(DamageQuality youHurt, DamageQuality enemyHurt) {
            damageable.TakeDamage(enemyHurt);
            TakeDamage(youHurt);
        }
    }

}
