using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralInputFPS : MonoBehaviour {

    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<GeneralMovePoint> movePointList;
    [SerializeField] private Transform general;

    private int currentRoad = 1;

    private void OnEnable() => inputManager.OnSwipePerformed += MoveWithSwipe;
    public void OnDisable() => inputManager.OnSwipePerformed -= MoveWithSwipe;

    [SerializeField] private float deltaThreshold = 50f;
    private bool canInput = true;
    private void MoveWithSwipe(Vector2 delta) { //FPS Shoulder Cam

        if (!canInput) return;
        if (Mathf.Abs(delta.x) < deltaThreshold) return;

        switch (delta.x) {
            //right swipe
            case > 0f when currentRoad == movePointList.Count: //if you stand top right
            Jump(movePointList.Last().Position);
            break;
            case > 0f:
            currentRoad++;
            Jump(movePointList[currentRoad - 1].Position);
            break;
            //left swipe
            case < 0f when currentRoad == 1: //if you stand top left
            Jump(movePointList.First().Position);
            break;
            case < 0f:
            currentRoad--;
            Jump(movePointList[currentRoad - 1].Position);
            break;
        }

        void Jump(Vector3 movePos, float duration = 1f) {
            general.DOJump(movePos, 2f, 1, duration);
            canInput = false;
            StartCoroutine(UtilsClass.Wait(() =>
            {
                canInput = true;
            }, duration));
        }
    }
}
