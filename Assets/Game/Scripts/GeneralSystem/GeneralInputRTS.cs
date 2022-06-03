using DG.Tweening;
using System.Linq;
using UnityEngine;

public class GeneralInputRTS : MonoBehaviour {

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform general;

    private int currentRoad = 1;

    private bool isTouching => inputManager.IsTouching;
    private RaycastHit[] hits = new RaycastHit[5];
    
    private void Update() => MoveWithColliders();
    private void MoveWithColliders() { //RTS Cam
        if (!isTouching) return;
        Ray ray = UtilsClass.GetScreenPointToRay(inputManager.TouchCoords);

        int hitCount = Physics.RaycastNonAlloc(ray, hits, Mathf.Infinity);
        var results = hits.Take(hitCount);

        bool isTouchable = results.Any(o => o.transform.CompareTag(StringData.TOUCHABLE));
        if (!isTouchable) return;

        GeneralMovePoint point = results.First().transform.GetComponentInParent<GeneralMovePoint>();

        //if (currentRoad == point.Road) return;

        currentRoad = (int)point.Road;
        general.DOJump(point.Position, 3f, 1, 1f).SetEase(Ease.Linear);
    }
}
