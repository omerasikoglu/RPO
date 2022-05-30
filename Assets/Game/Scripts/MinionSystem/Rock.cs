using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Minion {

    protected override void OnTriggerEnter(Collider collision) {
        base.OnTriggerEnter(collision);

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
