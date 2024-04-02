using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformationSelectController : MonoBehaviour
{
    [SerializeField] private TransformationSelectButton[] transformationButtons;
    [SerializeField] private PlayerAbilities playerAbilities;

    private void Awake()
    {
        playerAbilities.OnTransformationUnlocked += UnlockTransformation;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        playerAbilities.OnTransformationUnlocked -= UnlockTransformation;
    }

    private void OnEnable()
    {
        TransformationSelectButton.OnAnySelected += OnSelectedHandler;
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
        }
    }

    private void OnSelectedHandler()
    {
        // Close selector when pressing a button
        EnableSelector(false);
    }

    

    public void EnableSelector(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    
}
