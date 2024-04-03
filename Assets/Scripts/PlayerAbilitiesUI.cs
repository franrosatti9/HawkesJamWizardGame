using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerAbilitiesUI : MonoBehaviour
{
    [SerializeField] PlayerAbilities _playerAbilities;

    [SerializeField] private TextMeshProUGUI availableStonesText;
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private Image outputSprite;
    [SerializeField] private Sprite undiscoveredSprite;
    [SerializeField] private Button createCombinationButton;
    [SerializeField] private GameObject noStonesWarning;

    [SerializeField] DNASlot[] dnaSlots;

    [SerializeField] private List<AbilityCombinationSO> allCombinations;
    [SerializeField] private AbilitySO normalOutput;

    private AbilitySO output;

    private bool inputEnabled = false;

    private HashSet<AbilitySO> discoveredOutputs = new HashSet<AbilitySO>();

    private void Awake()
    {
        UpdateUI();
        gameObject.SetActive(false);
        
        
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        DNASlot.OnAnySlotUpdated += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        DNASlot.OnAnySlotUpdated -= UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // TEST MODE

            foreach (var combination in allCombinations)
            {
                _playerAbilities.AddDNAStone();
                output = combination.output;
                CreateCombination();
                Debug.Log("UNLOCK TESTING");
            }
        }
    }

    public void CheckCombination()
    {
        
        //DNAPartTypes[] currentParts =  dnaSlots.Select(slot => slot.CurrentDNA.DNAType).ToArray();
        
        DNAPartTypes[] currentParts = new DNAPartTypes[3];
        for (int i = 0; i < dnaSlots.Length; i++)
        {
            currentParts[i] = dnaSlots[i].CurrentDNA.DNAType;
        }
        output = null;

        foreach (var combination in allCombinations)
        {
            if (Utilities.ArrayAreEqual(combination.combination, currentParts))
            {
                output = combination.output;
                break;
            }
        }

        if (output != null)
        {
            if (discoveredOutputs.Contains(output))
            {
                outputText.text = output.abilityName;
                outputSprite.sprite = output.abilitySprite;
                createCombinationButton.interactable = false;
            }
            else
            {
                outputText.text = "???";
                outputSprite.sprite = undiscoveredSprite;
                createCombinationButton.interactable = true;
            }
        }
        else
        {
            outputText.text = "-";
            outputSprite.sprite = undiscoveredSprite;
            createCombinationButton.interactable = false;
        }
        
        Debug.Log("Current parts: " + currentParts.Length);
        Debug.Log("Output: " + output);
    }

    public void CreateCombination()
    {
        discoveredOutputs.Add(output);
        _playerAbilities.AddAbility(output);
        output = null;
        UpdateUI();
    }

    void UpdateUI()
    {
        CheckCombination();
        availableStonesText.text = _playerAbilities.DNAStones.ToString();

        if (_playerAbilities.DNAStones > 0 && !inputEnabled)
        {
            inputEnabled = true;
            noStonesWarning.SetActive(false);
            foreach (var slot in dnaSlots)
            {
                slot.SetInteractable(true);
            }
        }
        else if (_playerAbilities.DNAStones <= 0 && inputEnabled)
        {
            inputEnabled = false;
            noStonesWarning.SetActive(true);
            foreach (var slot in dnaSlots)
            {
                slot.SetInteractable(false);
            }
        }
    }
    
    
}
