using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public abstract class BarUI : MonoBehaviour
{
    protected Transform greenBar;

    protected float currentValue;

    protected int maxValue => 5;  //TODO: Create healthOptionsSO and pull from there
    protected float multiplier => 1 / (float)maxValue;

    protected virtual void Awake() {
        greenBar = transform.GetComponentInChildren<Transform>().Find(StringData.BAR);
    }
}
