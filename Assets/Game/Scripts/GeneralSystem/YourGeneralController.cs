using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

public class YourGeneralController : MonoBehaviour { //FPS Shoulder Cam, was GeneralInputFPS

    [SerializeField, BoxGroup("Input")] private InputManager inputManager;
    [SerializeField, BoxGroup("Input")] private List<GeneralMovePoint> movePointList;
    [SerializeField, BoxGroup("Input")] private float deltaThreshold = 50f;

    private int currentRoad = 1;
    private bool canInput = true;

    private void OnEnable() => inputManager.OnSwipePerformed += MoveWithSwipe;
    public void OnDisable() => inputManager.OnSwipePerformed -= MoveWithSwipe;

    private void MoveWithSwipe(Vector2 delta) {

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
            //set current spawn point
            break;
            //left swipe
            case < 0f when currentRoad == 1: //if you stand top left
            Jump(movePointList.First().Position);
            break;
            case < 0f:
            currentRoad--;
            Jump(movePointList[currentRoad - 1].Position);
            //set current spawn point
            break;
        }

        void Jump(Vector3 movePos, float duration = 1f) {
            transform.DOJump(movePos, 2f, 1, duration);
            canInput = false;
            StartCoroutine(UtilsClass.Wait(() =>
            {
                canInput = true;
            }, duration));
        }
    }
}
