using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MinionContainerUI : MonoBehaviour {

    //public event EventHandler<OnMinionSelectedEventArgs> OnMinionSelected;
    //public class OnMinionSelectedEventArgs : EventArgs {
    //    public MinionTypeSO minionType;
    //}


    [SerializeField] private MinionTypeListSO minionTypeList;
    [SerializeField] private Transform minionTemplate;

    private Dictionary<MinionTypeSO, Transform> minionTypeTransformDic;

    //#region Input
    //[SerializeField] private InputManager inputManager;

    //private void OnEnable() {
    //    inputManager.OnTouchWithCoordsPerformed += InputManagerOnTouchWithCoordsPerformed;
    //}
    //private void OnDisable() {
    //    inputManager.OnTouchWithCoordsPerformed -= InputManagerOnTouchWithCoordsPerformed;
    //}

    //private void InputManagerOnTouchWithCoordsPerformed(Vector2 coord)
    //{
    //    if (EventSystem.current.IsPointerOverGameObject()) return;
    //    //if (!ManaManager.Instance.HaveEnoughMana()) return;

    //    //TODO: Not enough mana tooltipUI
    //}



    //#endregion



    private void Awake() {
        minionTypeTransformDic = new Dictionary<MinionTypeSO, Transform>();

        PrototypePattern(); void PrototypePattern() {
            minionTemplate.gameObject.SetActive(false);

            foreach (var minionType in minionTypeList.List) {

                if (!minionType.IsShowingToUI) continue;

                Transform minionTransform = Instantiate(minionTemplate, transform);
                minionTransform.gameObject.SetActive(true);

                minionTransform.GetComponent<Image>().sprite = minionType.Sprite;
                minionTypeTransformDic[minionType] = minionTransform;

                    minionTransform.GetComponent<PointerEvents>().OnPointerEnterEvent = (sender, e) =>
                    {
                        MinionFactory.Instance.PullUnit(minionType.MinionType, Team.green);
                    };



            }
        }
    }
    public void somefunc(object sender, System.EventArgs args) {
        Debug.Log("onpointerclick");

    }


}
