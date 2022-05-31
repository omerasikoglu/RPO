using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadInputController : MonoBehaviour {
    [SerializeField] private InputManager inputManager;


    [SerializeField] private List<Transform> generalMovePoints;
    [SerializeField] private Transform general;

    private RaycastHit[] hits = new RaycastHit[5];
    private Roads currentRoad;
    private bool isTouching => inputManager.IsTouching;
    public Vector3 GetRoadPosition(Roads road) {
        return road switch
        {
            (Roads)1 => Vector3.zero,
            (Roads)2 => Vector3.zero,
            _ => Vector3.zero
        };
    }
    public void Update() {
        //check IsTouchingGeneralPlaces
        if (!isTouching) return;
        Ray ray = UtilsClass.GetScreenPointToRay(inputManager.TouchCoords);

        int hitCount = Physics.RaycastNonAlloc(ray, hits, Mathf.Infinity);
        var results = hits.Take(hitCount);

        var isTouchable = results.Any(o => o.transform.CompareTag(StringData.TOUCHABLE));

        if (!isTouchable) return;
        //TODO: General moves here
        GeneralMovePoint point = results.First().transform.GetComponentInParent<GeneralMovePoint>();
        if (currentRoad == point.Road) return;
        
        currentRoad = point.Road;
        general.DOMove(point.Position, 2f);


    }

}
