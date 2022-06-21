using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public System.EventHandler OnPointerEnterEvent, OnPointerExitEvent, OnPointerClickEvent;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerEnterEvent?.Invoke(this, EventArgs.Empty);
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerExitEvent?.Invoke(this, EventArgs.Empty);
        //Debug.Log("OnPointerExit");
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerClickEvent?.Invoke(this, EventArgs.Empty);
        //Debug.Log("OnPointerClick");
    }
}
