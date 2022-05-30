using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Paper : Minion {
    //public static Paper Create() {

    //    //TODO: GetFromObjectPool

    //    Transform minionTransform = Instantiate(pfMinion, GetSpawnPos(spawnPoint), Quaternion.identity);

    //    Paper paper = minionTransform.GetComponent<Paper>();
    //    paper.Setup();


    //    return paper;
    //}


    protected override void OnTriggerEnter(Collider collision) {
        base.OnTriggerEnter(collision);

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
