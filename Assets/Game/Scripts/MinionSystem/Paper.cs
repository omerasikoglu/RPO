using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Paper : Minion {

    [Button]
    public static Paper Create(Transform pfMinion,Vector3 spawnPosition) {

        //TODO: GetFromObjectPool

        Transform minionTransform = Instantiate(pfMinion, spawnPosition, Quaternion.identity);

        Paper paper = minionTransform.GetComponent<Paper>();
        paper.SetSize();


        return paper;
    }

    private void Setup()
    {

    }








    protected override void OnTriggerEnter(Collider collision) {
        base.OnTriggerEnter(collision);

        Rock rock = collision.GetComponent<Rock>();
        if (rock != null) {
            CalculateCombat(rock, DamageType.critical, DamageType.poor);
        }

        Scissors scissors = collision.GetComponent<Scissors>();
        if (scissors != null) {
            CalculateCombat(scissors, DamageType.poor, DamageType.critical);
        }

        Paper paper = collision.GetComponent<Paper>();
        if (paper != null) {
            CalculateCombat(paper, DamageType.normal, DamageType.normal);
        }

        void CalculateCombat(Minion minion, DamageType youTakeDamage, DamageType enemyTakeDamage) {

            minion.TakeDamage(GetDamage(enemyTakeDamage));
            GetDamage(youTakeDamage);
        }
    }
}
