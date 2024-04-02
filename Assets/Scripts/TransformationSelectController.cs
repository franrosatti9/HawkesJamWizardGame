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

    private void OnEnable()
    {
        TransformationSelectButton.OnAnySelected += OnSelectedHandler;
        
    }

    private void OnDisable()
    {
        TransformationSelectButton.OnAnySelected -= OnSelectedHandler;
        //playerAbilities.OnTransformationUnlocked -= UnlockTransformation;
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
