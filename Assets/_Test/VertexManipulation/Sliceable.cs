using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertexManipulation {
    public class Sliceable : MonoBehaviour {
        
        public bool isSolid = true;
        public bool isReverseWindTriangles = false;
        public bool isUsingGravity = false;
        [SerializeField] private bool isSharingVertices = false; public bool IsSharingVertices => isSharingVertices;
        [SerializeField] private bool isSmoothVertices = false; public bool IsSmoothVertices => isSmoothVertices;
    } 
}
