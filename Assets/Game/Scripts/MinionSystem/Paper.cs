using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Paper : Minion {
    public static Paper Create(Vector3 spawnPos) {

        Transform paperPrefab = Resources.Load<MinionTypeListSO>(typeof(MinionTypeListSO).Name).GetPaper;
        Transform minionTransform = Instantiate(paperPrefab, spawnPos, Quaternion.identity);

        Paper paper = minionTransform.GetComponent<Paper>();
        paper.Setup();

        return paper;
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
            CalculateCombat(octopus, DamageQuality.critical, DamageQuality.poor);
        }

        #endregion

        Rock rock = collision.attachedRigidbody.GetComponent<Rock>();
        if (rock != null) {
            CalculateCombat(rock, DamageQuality.poor, DamageQuality.critical);
        }

        Scissors scissors = collision.attachedRigidbody.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageQuality.critical, DamageQuality.poor);
        }

        Paper paper = collision.attachedRigidbody.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageQuality.normal, DamageQuality.normal);
        }

        void CalculateCombat(Minion enemyMinion, DamageQuality youTakeDamage, DamageQuality damageQuality) {

            enemyMinion.TakeDamage(GetDamage(damageQuality));
            TakeDamage(GetDamage(youTakeDamage));
        }
    }
}
