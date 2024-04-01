using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject abilitiesUI;

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
}
