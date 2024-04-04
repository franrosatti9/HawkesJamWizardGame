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
    [SerializeField] private GameObject infoTextPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image selectedSpellImage;
    [SerializeField] private TextMeshProUGUI selectedSpellText;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject endGameScreen;

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
        if (pauseMenu.activeSelf) return true;
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
        if (endGameScreen.activeSelf) return;
        transformSelectorUI.EnableSelector(enable);
    }

    public bool IsTransformSelectorOn()
    {
        return transformSelectorUI.gameObject.activeSelf;
    }

    public void ShowInfoText(string text)
    {
        infoTextPanel.SetActive(true);
        infoText.text = text;
        LeanTween.cancel(infoTextPanel);
        LeanTween.moveLocalY(infoTextPanel,-500, 0.25f);
    }

    public void HideInfoText()
    {
        if (!infoTextPanel.activeSelf) return; // Don't animate if not shown
        LeanTween.cancel(infoTextPanel);
        LeanTween.moveLocalY(infoTextPanel, -800, 0.25f).setOnComplete(() => infoTextPanel.SetActive(false));
    }

    public bool IsAbilitiesUIOn()
    {
        return abilitiesUI.activeSelf;
    }

    public void ToggleCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        
        GameManager.Instance.SwitchState(GameState.Paused);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);

        if (abilitiesUI.activeSelf || endGameScreen.activeSelf) return;
        if (transformSelectorUI.gameObject.activeSelf)
        {
            transformSelectorUI.EnableSelector(false);
            return; // Return to not try change state twice
        }
        GameManager.Instance.SwitchState(GameState.Gameplay);
    }

    public void ResetPlayerButton()
    {
        ClosePauseMenu();
        GameManager.Instance.RestartToCheckpoint();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void EndGameScreen()
    {
        endGameScreen.SetActive(true);
        // TODO: TRY ANIMATE WITH LEAN TWEEN IF HAVE ENOUGH TIME
    }

    public void CloseMainMenu()
    {
        mainMenuCanvas.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
