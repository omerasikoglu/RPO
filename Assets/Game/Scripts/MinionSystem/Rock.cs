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
            CalculateCombat(rock, DamageQuality.normal, DamageQuality.normal);
        }

        Scissors scissors = collision.attachedRigidbody.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageQuality.poor, DamageQuality.critical);
        }

        Paper paper = collision.attachedRigidbody.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageQuality.critical, DamageQuality.poor);
        }

        void CalculateCombat(Minion enemyMinion, DamageQuality youHurt, DamageQuality enemyHurt) {

            enemyMinion.TakeDamage(GetDamage(enemyHurt));
            TakeDamage(GetDamage(youHurt));
        }
    }

}
