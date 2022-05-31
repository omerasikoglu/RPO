using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertexManipulation {
    public class Sliceable : MonoBehaviour {
        [SerializeField] private bool isSolid = true; public bool IsSolid => isSolid;
        [SerializeField] private bool isReverseWindTriangles = false; public bool IsReverseWireTriangles => isReverseWindTriangles;
        [SerializeField] private bool isUsingGravity = false; public bool IsUsingGravity => isUsingGravity;
        [SerializeField] private bool isSharingVertices = false; public bool IsSharingVertices => isSharingVertices;
        [SerializeField] private bool isSmoothVertices = false; public bool IsSmoothVertices => isSmoothVertices;
    } 
}
