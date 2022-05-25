using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MinionContainerUI : MonoBehaviour {

    public event EventHandler<OnMinionSelectedEventArgs> OnMinionSelected;
    public class OnMinionSelectedEventArgs : EventArgs {
        public MinionTypeSO minionType;
    }


    [SerializeField] private MinionListSO MinionList;
    [SerializeField] private Transform minionTemplate;

    private Dictionary<MinionTypeSO, Transform> minionTypeTransformDic;

    #region Input
    [SerializeField] private InputManager inputManager;

    private void OnEnable() {
        inputManager.OnCoordTouchPerformed += InputManagerOnCoordTouchPerformed;
    }
    private void OnDisable() {
        inputManager.OnCoordTouchPerformed -= InputManagerOnCoordTouchPerformed;
    }

    private void InputManagerOnCoordTouchPerformed(Vector2 coord)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!ManaManager.Instance.HaveEnoughMana()) return;
        
        //TODO: Not enough mana tooltipUI
    }



    #endregion



        private void Awake() {
            minionTypeTransformDic = new Dictionary<MinionTypeSO, Transform>();

            PrototypePattern(); void PrototypePattern() {
                minionTemplate.gameObject.SetActive(false);

                foreach (var minionType in MinionList.list) {
                    Transform minionTransform = Instantiate(minionTemplate, transform);
                    minionTransform.gameObject.SetActive(true);

                    minionTransform.GetComponent<Image>().sprite = minionType.Sprite;
                    minionTypeTransformDic[minionType] = minionTransform;
                }
            }
        }


    }
