using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using NaughtyAttributes;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour {
    public event Action<Vector2> OnTouchPerformedWithCoords, OnCoordTouchEnded; //return coord
    public event Action<Vector2> OnSlidePerformed, OnSwipePerformed; //returns delta
    public event Action OnTouchPerformed, OnTouchEnded;

    private TouchControlMap touchControlMap;
    public bool IsTouching => touchControlMap.TouchActionMap.TouchContact.IsPressed();
    public Vector2 TouchCoords => touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>();

    private void Awake() => touchControlMap = new TouchControlMap();
    private void OnEnable() => touchControlMap.Enable();
    private void OnDisable() => touchControlMap.Disable();

    private void Start() {
        
        Observer();

        void Observer() {
            touchControlMap.TouchActionMap.Touch.performed += PerformTouch;
            touchControlMap.TouchActionMap.Slide.performed += PerformSlide;
            touchControlMap.TouchActionMap.Swipe.performed += PerformSwipe;
            touchControlMap.TouchActionMap.TouchContact.canceled += EndTouch;
            touchControlMap.TouchActionMap.TouchContact.performed += PerformTouch;
        }
    }

    private void PerformSwipe(InputAction.CallbackContext obj) {
        OnSwipePerformed?.Invoke(touchControlMap.TouchActionMap.Swipe.ReadValue<Vector2>());
    }

    private void PerformTouch(InputAction.CallbackContext context) {
        if (IsPointerOutsideTheBorder()) return;

        OnTouchPerformed?.Invoke();
        OnTouchPerformedWithCoords?.Invoke(touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>());
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