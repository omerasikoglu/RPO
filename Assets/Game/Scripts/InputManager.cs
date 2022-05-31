using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour {
    public event Action<Vector2> OnTouchWithCoordsPerformed, OnCoordTouchEnded; //return coord
    public event Action<Vector2> OnSlidePerformed; //returns delta
    public event Action OnTouchPerformed, OnTouchEnded;

    public event Action OnTouchingGeneralPlaces;

    private TouchControlMap touchControlMap;
    private bool IsTouching => touchControlMap.TouchActionMap.TouchContact.IsPressed();
    private Vector2 TouchCoords => touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>();


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

    private RaycastHit[] hits = new RaycastHit[5]; 
    private void Update()
    {
        if (!IsTouching) return;

        //check IsTouchingGeneralPlaces

        Ray ray = UtilsClass.GetScreenPointToRay(TouchCoords);

        int hitCount = Physics.RaycastNonAlloc(ray, hits, Mathf.Infinity, 1);
        var results = hits.Take(hitCount);

        var isTouchable = results.Any(o=>o.transform.CompareTag("Touchable"));

        if (!isTouchable) return;

        Debug.Log("general place1 ");

    }

    private void PerformTouch(InputAction.CallbackContext context) {
        if (IsPointerOutsideTheBorder()) return;

        OnTouchPerformed?.Invoke();
        
        var coords = touchControlMap.TouchActionMap.Touch.ReadValue<Vector2>();
        OnTouchWithCoordsPerformed?.Invoke(coords);

      


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