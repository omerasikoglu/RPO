using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

public class SpawnMinion : EnemyGeneralAction {

    public override void OnStart() {
        animator.SetTrigger("SpawnMinion");
    }

    private void Spawn() { // all random dumb AI (aka. easy game mode)


        //MinionFactory.Instance.PullUnit()


    }


}
