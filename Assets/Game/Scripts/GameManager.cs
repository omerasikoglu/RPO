using System;
using UnityEngine;

[Serializable]
public enum GameState {
    TapToPlay = 1,
    InGame = 2,
    Win = 3,
    Lose = 4,
}
public class GameManager : Singleton<GameManager> {
    public static event Action<GameState> OnStateChanged;
    private GameState activeState;

    private void Awake() {
       
        InitPlayerPrefs();

        void InitPlayerPrefs() {
            PlayerPrefs.SetInt(StringData.PREF_MONEY, 0);
            PlayerPrefs.SetInt(StringData.PREF_LEVEL, 1);
        }
    }
    private void Start() => ChangeState(GameState.TapToPlay);
    public void ChangeState(GameState newState) {
        if (activeState == newState) return;

        activeState = newState;

        OnStateChanged?.Invoke(newState);
    }
}
