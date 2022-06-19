using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public event System.EventHandler OnPointerEnterEvent, OnPointerExitEvent, OnPointerClickEvent;

    private StringBuilder stringBuilder;


    public void OnPointerEnter(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerEnterEvent?.Invoke(this, EventArgs.Empty);
        stringBuilder.Append("OnPointerEnter");
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerExitEvent?.Invoke(this, EventArgs.Empty);
        stringBuilder.Append("OnPointerExit");
        //Debug.Log("OnPointerExit");
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        OnPointerClickEvent?.Invoke(this, EventArgs.Empty);
        stringBuilder.Append("OnPointerClick");
        //Debug.Log("OnPointerClick");
    }
}
