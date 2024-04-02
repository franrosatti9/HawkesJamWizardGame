using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState currentState;
    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !UIManager.instance.IsTransformSelectorOn())
        {
            bool on = UIManager.instance.ToggleAbilitiesUI();
            if (on)
            {
                SwitchState(GameState.Paused);
            }
            else
            {
                SwitchState(GameState.Gameplay);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentState == GameState.Paused) return;
            UIManager.instance.EnabledTransformSelector(true);
            SwitchState(GameState.Paused);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !UIManager.instance.IsAbilitiesUIOn())
        {

            UIManager.instance.EnabledTransformSelector(false);
            SwitchState(GameState.Gameplay);
        }
    }

    public void SwitchState(GameState newState)
    {
        if (currentState == newState) return;
        
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
        
        switch (newState)
        {
            case GameState.Gameplay:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    Gameplay,
    Paused
}
