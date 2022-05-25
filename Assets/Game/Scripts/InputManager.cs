using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager> {
    public event Action<Vector2> OnCoordTouchPerformed, OnCoordTouchEnded; //return coord
    public event Action<Vector2> OnSlidePerformed; //returns delta
    public event Action OnTouchPerformed, OnTouchEnded;

    private TouchControlMap touchControlMap;

    private void Awake() => touchControlMap = new TouchControlMap();
    private void OnEnable() => touchControlMap.Enable();
    private void OnDisable() => touchControlMap.Disable();

    private void Start() {

        Observer();

        void Observer() {
            touchControlMap.TouchActionMap.Touch.performed += PerformTouch;
            touchControlMap.TouchActionMap.Slide.performed += PerformSlide;
            touchControlMap.TouchActionMap.TouchContact.canceled += EndTouch;
            touchControlMap.TouchActionMap.TouchContact.performed += PerformTouch;
        }
    }
    private void PerformTouch(InputAction.CallbackContext context) {
        if (IsPointerOutsideTheBorder()) return;

        OnTouchPerformed?.Invoke();
        OnCoordTouchPerformed?.Invoke(touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>());
    }
    private void PerformSlide(InputAction.CallbackContext context) {
        if (IsPointerOutsideTheBorder()) return;

        OnSlidePerformed?.Invoke(touchControlMap.TouchActionMap.Slide.ReadValue<Vector2>());
    }

    private void EndTouch(InputAction.CallbackContext context) {
        if (IsPointerOutsideTheBorder()) return;

        OnTouchEnded?.Invoke();
        OnCoordTouchEnded?.Invoke(touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>());
    }
    private bool IsPointerOutsideTheBorder() {
        return float.IsInfinity(touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>().x);
    }
}