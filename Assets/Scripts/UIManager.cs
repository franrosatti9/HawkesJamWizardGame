using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject abilitiesUI;
    [SerializeField] private TransformationSelectController transformSelectorUI;
    [SerializeField] private Image selectedSpellImage;
    [SerializeField] private TextMeshProUGUI selectedSpellText;

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

    public bool ToggleAbilitiesUI()
    {
        abilitiesUI.SetActive(!abilitiesUI.activeSelf);
        return abilitiesUI.activeSelf;
    }

    public void ChangeSelectedSpellUI(Sprite spellSprite, string spellName)
    {
        selectedSpellImage.sprite = spellSprite;
        selectedSpellText.text = spellName;
    }
    
    public void EnabledTransformSelector(bool enable)
    {
        transformSelectorUI.EnableSelector(enable);
    }

    public bool IsTransformSelectorOn()
    {
        return transformSelectorUI.gameObject.activeSelf;
    }

    public bool IsAbilitiesUIOn()
    {
        return abilitiesUI.activeSelf;
    }
}
