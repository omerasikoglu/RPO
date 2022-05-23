using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[Serializable]
public enum Cam {
    TapToPlay = 1,
    InGame = 2,
    Final = 3,
}
public class CameraHandler : MonoBehaviour {

    [SerializeField] private List<CinemachineVirtualCamera> vCamList;

    private CinemachineVirtualCamera currentCam;
    private Cam? oldCam;

    private void OnEnable() => GameManager.OnStateChanged += GameManager_OnStateChanged;
    private void OnDisable() => GameManager.OnStateChanged -= GameManager_OnStateChanged;

    private void GameManager_OnStateChanged(GameState gameState) {

        switch (gameState) {
            case GameState.TapToPlay: OpenCam(Cam.TapToPlay); break;
            case GameState.InGame: OpenCam(Cam.InGame); break;
            case GameState.Win: OpenCam(Cam.Final); break;
            case GameState.Lose: OpenCam(Cam.Final); break;
            default: break;
        }
    }

    private void OpenCam(Cam newCam) {
        if (oldCam == newCam) return;

        oldCam = newCam;

        currentCam = vCamList[(int)newCam];

        ActivateNewCam(currentCam);

        void ActivateNewCam(CinemachineVirtualCamera newCam) {
            foreach (CinemachineVirtualCamera cam in vCamList) {
                cam.gameObject.SetActive(newCam == cam);
            }
        }
    }
}
