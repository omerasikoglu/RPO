using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Minion {
    public static Octopus Create(Vector3 spawnPos) {

        Transform octopusPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetOctopus;
        Transform minionTransform = Instantiate(octopusPrefab, spawnPos, Quaternion.identity);

        Octopus octopus = minionTransform.GetComponent<Octopus>();
        octopus.Setup();

        return octopus;
    }

    private void OnTriggerEnter(Collider collision) {

        #region base // BUG: cant colliding when inherit from base class

        HealthManager general = collision.attachedRigidbody.GetComponent<HealthManager>(); //general health
        if (general != null) {
            general.TakeDamage();
            TakeDamage(GetDamage(DamageQuality.instaDeath));
        }

        Minion minion = collision.attachedRigidbody.GetComponent<Minion>();
        if (minion == null) return;
        if (GetTeam().Equals(minion.GetTeam())) return;

        Octopus octopus = collision.attachedRigidbody.GetComponent<Octopus>();
        if (octopus != null) {
            CalculateCombat(octopus, DamageQuality.critical, DamageQuality.critical);
        }

        #endregion

        Rock rock = collision.attachedRigidbody.GetComponent<Rock>();
        if (rock != null) {
            CalculateCombat(rock, DamageQuality.poor, DamageQuality.critical);
        }

        Scissors scissors = collision.attachedRigidbody.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageQuality.poor, DamageQuality.critical);
        }

        Paper paper = collision.attachedRigidbody.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageQuality.poor, DamageQuality.critical);
        }

        void CalculateCombat(Minion enemyMinion, DamageQuality youTakeDamage, DamageQuality damageQuality) {

            enemyMinion.TakeDamage(GetDamage(damageQuality));
            TakeDamage(GetDamage(youTakeDamage));
        }
    }

}
