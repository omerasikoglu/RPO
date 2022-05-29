using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Minion {

    protected override void OnTriggerEnter(Collider collision) {
        base.OnTriggerEnter(collision);

        Rock rock = collision.attachedRigidbody.GetComponent<Rock>();
        if (rock != null) {
            CalculateCombat(rock, DamageQualityEnum.normal, DamageQualityEnum.normal);
        }

        Scissors scissors = collision.attachedRigidbody.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageQualityEnum.poor, DamageQualityEnum.critical);
        }

        Paper paper = collision.attachedRigidbody.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageQualityEnum.critical, DamageQualityEnum.poor);
        }

        void CalculateCombat(Minion enemyMinion, DamageQualityEnum youHurt, DamageQualityEnum enemyHurt) {

            enemyMinion.TakeDamage(GetDamage(enemyHurt));
            TakeDamage(GetDamage(youHurt));
        }
    }

}
