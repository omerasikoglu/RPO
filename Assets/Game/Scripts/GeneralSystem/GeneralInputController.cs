using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralInputController : MonoBehaviour {

    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<Transform> generalMovePoints;
    [SerializeField] private Transform general;

    [SerializeField] private List<GeneralMovePoint> moveList;


    private RaycastHit[] hits = new RaycastHit[5];
    private Roads currentRoad;
    private bool isTouching => inputManager.IsTouching;

    public void OnEnable() {
        //inputManager.OnSwipePerformed += MoveWithSwipe;
        inputManager.OnSlidePerformed += MoveWithSwipe;
    }
    public void Update() {
        //MoveWithColliders();
    }

    [SerializeField] private float deltaThreshold = 50f;
    private bool canInput = false;
    private void MoveWithSwipe(Vector2 delta) {
        if (canInput) return;

        Debug.Log(delta.x);
        Vector3 movePos = delta.x > 50f ? moveList[0].Position : moveList[1].Position;

        if (delta.x > deltaThreshold) {
            Jump(moveList[1].Position);
        }
        else if (delta.x < -deltaThreshold) {
            Jump(moveList[0].Position);
        }

        void Jump(Vector3 movePos, float duration = 1f) {
            general.DOJump(movePos, 2f, 1, duration);
            canInput = true;
            StartCoroutine(UtilsClass.Wait(() =>
            {
                canInput = false;
            }, duration));
        }
    }

    private void MoveWithColliders() {
        if (!isTouching) return;
        Ray ray = UtilsClass.GetScreenPointToRay(inputManager.TouchCoords);

        int hitCount = Physics.RaycastNonAlloc(ray, hits, Mathf.Infinity);
        var results = hits.Take(hitCount);

        bool isTouchable = results.Any(o => o.transform.CompareTag(StringData.TOUCHABLE));
        if (!isTouchable) return;

        GeneralMovePoint point = results.First().transform.GetComponentInParent<GeneralMovePoint>();
        //if (currentRoad == point.Road) return;

        currentRoad = point.Road;
        //general.DOMove(point.Position,1f);
        general.DOJump(point.Position, 1f, 1, 1f);
    }
}
