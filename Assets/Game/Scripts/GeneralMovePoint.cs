using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovePoint : MonoBehaviour {
    [SerializeField] private Roads road; public Roads Road => road;
    [SerializeField] private Transform generalStandPosition; public Vector3 Position => generalStandPosition.position;


}
