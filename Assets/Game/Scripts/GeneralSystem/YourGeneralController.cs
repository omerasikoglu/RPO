using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

public class YourGeneralController : MonoBehaviour { //FPS Shoulder Cam, was GeneralInputFPS

    [SerializeField, BoxGroup("Input")] private InputManager inputManager;
    [SerializeField, BoxGroup("Input")] private List<GeneralMovePoint> movePointList;
    [SerializeField, BoxGroup("Input")] private float deltaThreshold = 50f;

    private int activeRoad = 1;
    private bool canInput = true;

    private void OnEnable()
    {
        inputManager.OnSwipePerformed += MoveWithSwipe;
        RoadManager.Instance.OnActiveRoadChanged += Instance_OnActiveRoadTypeChanged;
    }

    private void Instance_OnActiveRoadTypeChanged(object sender, RoadManager.OnActiveRoadChangedEventArgs e) {
        //e.activeRoad
    }

    public void OnDisable() => inputManager.OnSwipePerformed -= MoveWithSwipe;

    private void MoveWithSwipe(Vector2 delta) {

        if (!canInput) return;
        if (Mathf.Abs(delta.x) < deltaThreshold) return;

        switch (delta.x) {
            //right swipe
            case > 0f when activeRoad == movePointList.Count: //if you stand top right
            Jump(movePointList.Last().Position);
            break;

            case > 0f:
            activeRoad++;
            Jump(movePointList[activeRoad - 1].Position);
            RoadManager.Instance.SetActiveRoadType((Road)activeRoad);
            break;

            //left swipe
            case < 0f when activeRoad == 1: //if you stand top left
            Jump(movePointList.First().Position);
            break;

            case < 0f:
            activeRoad--;
            Jump(movePointList[activeRoad - 1].Position);
            RoadManager.Instance.SetActiveRoadType((Road)activeRoad);
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
