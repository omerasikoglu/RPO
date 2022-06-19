using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler {

    public event System.EventHandler OnMouseEnter;
    public event System.EventHandler OnMouseExit;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnMouseEnter?.Invoke(this, EventArgs.Empty);
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnMouseExit?.Invoke(this, EventArgs.Empty);
        Debug.Log("OnPointerExit");
    }

    public void OnPointerMove(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        Debug.Log("OnPointerMove");
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        Debug.Log("OnPointerClick");
    }
}
