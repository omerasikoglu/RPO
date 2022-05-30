using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Minion {

    public static Scissors Create(Vector3 spawnPos) {

        Transform scissorsPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetScissors;
        Transform minionTransform = Instantiate(scissorsPrefab, spawnPos, Quaternion.identity);

        Scissors scissors = minionTransform.GetComponent<Scissors>();
        scissors.Setup();

        return scissors;
    }
    private void OnTriggerEnter(Collider collision) {

        #region base // BUG: cant colliding when inherit from base class
        Minion minion = collision.attachedRigidbody.GetComponent<Minion>();
        if (minion == null) return;
        if (GetTeam().Equals(minion.GetTeam())) return;

        Octopus octopus = collision.attachedRigidbody.GetComponent<Octopus>();
        if (octopus != null) {
            CalculateCombat(octopus, DamageQuality.critical, DamageQuality.poor);
        }

        HealthManager general = collision.attachedRigidbody.GetComponent<HealthManager>(); //mainTower health
        if (general != null) {
            general.TakeDamage();
            TakeDamage(GetDamage(DamageQuality.instaDeath));
        }
        #endregion

        Rock rock = collision.attachedRigidbody.GetComponent<Rock>();
        if (rock != null) {
            CalculateCombat(rock, DamageQuality.critical, DamageQuality.poor);
        }

        Scissors scissors = collision.attachedRigidbody.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageQuality.normal, DamageQuality.normal);
        }

        Paper paper = collision.attachedRigidbody.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageQuality.critical, DamageQuality.critical);
        }

        void CalculateCombat(Minion enemyMinion, DamageQuality youTakeDamage, DamageQuality damageQuality) {

            enemyMinion.TakeDamage(GetDamage(damageQuality));
            TakeDamage(GetDamage(youTakeDamage));
        }
    }
}
