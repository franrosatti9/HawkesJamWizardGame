using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected string interactText;
    [SerializeField] protected bool interactOnce;
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected TextMeshProUGUI canvasText;
    protected bool _interactable = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact(PlayerController player);
}
