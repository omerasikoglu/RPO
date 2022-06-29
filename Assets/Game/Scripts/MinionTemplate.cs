using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionTemplate : MonoBehaviour
{

    private PointerEvents pointerEvents;

    public void Awake() {
        pointerEvents = GetComponent<PointerEvents>();

    }

    public void OnEnable()
    {
        pointerEvents.OnPointerClickEvent += ButtonClicked;
    }

    public void ButtonClicked(object sender, EventArgs eventArgs)
    {

        Debug.Log("clicked");

    }
}
