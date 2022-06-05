using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovePoint : MonoBehaviour {
    [SerializeField] private Road road; public Road Road => road;
    [SerializeField] private Transform generalStandPosition; public Vector3 Position => generalStandPosition.position;


}
