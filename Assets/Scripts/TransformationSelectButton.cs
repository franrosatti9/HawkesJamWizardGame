using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransformationSelectButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TransformationSO transformation;
    [SerializeField] private PlayerAbilities playerAbilities;
    [SerializeField] private bool startInteractable = false;
    private bool unlocked = false;

    public TransformationSO Transformation => transformation;
    public static event Action OnAnySelected;
    void Start()
    {
        unlocked = startInteractable;
        GetComponent<Button>().interactable = startInteractable;
        if (name != null)
        {
            name.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUnlocked()
    {
        unlocked = true;
        GetComponent<Button>().interactable = true;
        icon.sprite = transformation.abilitySprite;
        if (name != null) name.text = transformation.abilityName;
    }

    public void Select()
    {
        if(!unlocked) return;
        playerAbilities.Transform(transformation);
        OnAnySelected?.Invoke();
    }

    public void OnHoverEnter()
    {
        // MAYBE ADD VFX, SOME SPARKS OR SOMETHING
        if (name == null) return;
        name.gameObject.SetActive(true);
    }

    public void OnHoverExit()
    {
        if (name == null) return;
        name.gameObject.SetActive(false);
    }
}
