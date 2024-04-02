using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject abilitiesUI;
    [SerializeField] private GameObject transformSelectorUI;
    [SerializeField] private Image selectedSpellImage;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EnableAbilitiesUI(bool enabled)
    {
        abilitiesUI.SetActive(enabled);
    }

    public void ToggleAbilitiesUI()
    {
        abilitiesUI.SetActive(!abilitiesUI.activeSelf);
    }

    public void ChangeSelectedSpellUI(Sprite spellSprite)
    {
        selectedSpellImage.sprite = spellSprite;
    }
    
    public void EnabledTransformSelector(bool enabled)
    {
        transformSelectorUI.SetActive(enabled);
    }
}
