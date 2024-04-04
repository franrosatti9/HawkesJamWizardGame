using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private DoorPillar finalDoor;

    [SerializeField] PlayerController player;

    [SerializeField] private RectTransform fade;

    private Transform lastCheckpoint;
    private bool restartingPlayer = false;
    public SignProgressController SignProgress { get; private set; }

    private GameState currentState;
    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SignProgress = GetComponent<SignProgressController>();
    }

    private void Start()
    {
        LeanTween.color(fade, Color.clear, 1f);

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
            if (currentState == GameState.Gameplay) return;
            UIManager.instance.EnabledTransformSelector(false);
            //SwitchState(GameState.Gameplay);
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

    public void RestartToCheckpoint()
    {
        // FADE
        if(restartingPlayer) return;
        restartingPlayer = true;
        LeanTween.color(fade, Color.black, 0.5f).setLoopPingPong(1);
        Invoke(nameof(MovePlayerToCheckpoint), 0.5f);

    }

    void MovePlayerToCheckpoint()
    {
        player.transform.position = lastCheckpoint ? lastCheckpoint.position : Vector3.zero;
        restartingPlayer = false;
    }
    
    public void SetLastCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void OpenFinalDoor()
    {
        
    }
}

public enum GameState
{
    Gameplay,
    Paused
}
