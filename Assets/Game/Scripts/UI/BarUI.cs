using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public abstract class BarUI : MonoBehaviour
{
    [SerializeField] protected GeneralOptionsSO options;
    
    protected Transform colorfulBar;

    protected float currentValue;

    protected float maxValue;
    protected float fillSpeed => 1 / (float)maxValue;

    protected virtual void Awake() {
        colorfulBar = transform.GetComponentInChildren<Transform>().Find(StringData.BAR);
    }
}
