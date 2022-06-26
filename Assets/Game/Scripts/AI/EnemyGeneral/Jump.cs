using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

public class Jump : EnemyGeneralAction {

    [SerializeField] private float buildupTime;
    [SerializeField] private float jumpTime;

    private Tween buildupTween, jumpTween;
    private bool isLanded;

    public override void OnStart() {
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
        animator.SetTrigger("Jump");
    }
    private void StartJump() {

    }

    public override TaskStatus OnUpdate() {
        return isLanded ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd() {
        buildupTween?.Kill();
        jumpTween?.Kill();
        isLanded = true;
    }
}
