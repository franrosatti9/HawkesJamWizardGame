using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformationSelectController : MonoBehaviour
{
    [SerializeField] private TransformationSelectButton[] transformationButtons;
    [SerializeField] private PlayerAbilities playerAbilities;
    [SerializeField] private GameObject cantTransformWarning;

    private bool canTransform = true;

    private void Awake()
    {
        playerAbilities.OnTransformationUnlocked += UnlockTransformation;
        InitButtons();
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
        CheckIfAllowedTransformation(playerAbilities.CanTransform);
    }
    
    private void OnDestroy()
    {
        playerAbilities.OnTransformationUnlocked -= UnlockTransformation;
    }

    private void OnEnable()
    {
        TransformationSelectButton.OnAnySelected += OnSelectedHandler;

        CheckIfAllowedTransformation(playerAbilities.CanTransform);
        // TODO: LEAN TWEEN ANIMATION?

    }

    

    private void OnDisable()
    {
        TransformationSelectButton.OnAnySelected -= OnSelectedHandler;
        //playerAbilities.OnTransformationUnlocked -= UnlockTransformation;
    }

    private void Update()
    {
        
        //TODO: Improve but not important
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transformationButtons[0].Select();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transformationButtons[1].Select();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transformationButtons[2].Select();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transformationButtons[3].Select();
        }
    }

    public void UnlockTransformation(TransformationSO transformations)
    {
        Debug.Log("Unlock");
        TransformationSelectButton button = transformationButtons.FirstOrDefault(t => t.Transformation == transformations);
        if (button != null)
        {
            button.SetUnlocked();
            button.SetInteractableIfCanTransform(playerAbilities.CanTransform);
        }
    }

    void InitButtons()
    {
        for (int i = 0; i < transformationButtons.Length; i++)
        {
            transformationButtons[i].Initialize();
        }
    }

    private void OnSelectedHandler()
    {
        // Close selector when pressing a button
        EnableSelector(false);
    }

    private void CheckIfAllowedTransformation(bool allowed)
    {
        if (allowed == canTransform) return; // Don't do the for loop if already updated buttons
        
        for (int i = 0; i < transformationButtons.Length; i++)
        {
            transformationButtons[i].SetInteractableIfCanTransform(allowed);
        }

        canTransform = allowed;
        cantTransformWarning.SetActive(!allowed);
    }

    public void EnableSelector(bool enabled)
    {
        //gameObject.SetActive(enabled);

        if (!enabled)
        {
            LeanTween.scale(gameObject, Vector3.zero, 0.15f).setOnComplete(CloseSelector).setIgnoreTimeScale(true);
            //GameManager.Instance.SwitchState(GameState.Gameplay);
        }
        else
        {
            gameObject.SetActive(true);
            LeanTween.scale(gameObject, Vector3.one, 0.15f).setIgnoreTimeScale(true);
        }
    }

    void CloseSelector()
    {
        gameObject.SetActive((false));
        GameManager.Instance.SwitchState(GameState.Gameplay);
    }

    
}
