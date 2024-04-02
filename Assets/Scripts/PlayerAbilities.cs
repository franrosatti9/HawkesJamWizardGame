using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    //private Dictionary<AllSpells, AbilitySO> unlockedSpells = new Dictionary<AllSpells, AbilitySO>(); 
    //private Dictionary<AllTransformations, AbilitySO> unlockedTransformations = new Dictionary<AllTransformations, AbilitySO>();

    private List<SpellSO> unlockedSpells = new List<SpellSO>();
    [SerializeField] private List<TransformationSO> unlockedTransformations = new();
    [SerializeField] private Sprite noSpellsSprite;
    [SerializeField] private Transform spellCastPosition;
    private SpellSO selectedSpell;
    private TransformationSO currentTransformation;

    

    [SerializeField] int dnaStones = 0;
    public int DNAStones => dnaStones;

    public event Action<AllTransformations> OnPlayerTransformed;
    public event Action<TransformationSO> OnTransformationUnlocked;
    void Start()
    {
        // Select "Normal" form first
        currentTransformation = unlockedTransformations[0];
        SwitchCurrentSpell();
    }

    public void SwitchCurrentSpell()
    {
        if (unlockedSpells.Count == 0)
        {
            UIManager.instance.ChangeSelectedSpellUI(noSpellsSprite);
            return;
        }
        int nextSpellIndex = unlockedSpells.FindIndex(s => s == selectedSpell) + 1;
        if (nextSpellIndex >= unlockedSpells.Count)
        {
            nextSpellIndex = 0;
        }
        selectedSpell = unlockedSpells[nextSpellIndex];
        
        UIManager.instance.ChangeSelectedSpellUI(selectedSpell.abilitySprite);
        // CHANGE SPELL UI, MAYBE BULLET COLOR OR PREFAB 
        Debug.Log("Selected Spell: " + selectedSpell);
    }
    
    public void SwitchCurrentTransformation()
    {
        int nextTransformationIndex = unlockedTransformations.FindIndex(s => s == currentTransformation) + 1;
        if (nextTransformationIndex >= unlockedTransformations.Count)
        {
            nextTransformationIndex = 0;
        }
        currentTransformation = unlockedTransformations[nextTransformationIndex];
        
        // CHANGE TRANSFORMATION UI, OR MAKE A SELECTING WHEEL 
        Debug.Log("Selected Transformation: " + currentTransformation);
    }

    public void Transform(TransformationSO newTransformation)
    {
        if (!unlockedTransformations.Contains(newTransformation)) return;  
        currentTransformation = newTransformation;

        OnPlayerTransformed?.Invoke(currentTransformation.transformationType);
        
    }

    public void Transform()
    {
        OnPlayerTransformed?.Invoke(currentTransformation.transformationType);
    }

    public void AddTransformation(TransformationSO newTransformation)
    {
        unlockedTransformations.Add(newTransformation);
    }

    public void AddSpell(SpellSO newSpell)
    {
        if (!unlockedSpells.Contains(newSpell)) unlockedSpells.Add(newSpell);
    }

    public void UseSpell()
    {
        
        //selectedSpell

        var spell = Instantiate(selectedSpell.spellPrefab, spellCastPosition.position, quaternion.identity);
        spell.transform.right = transform.right * transform.localScale.x;

        // INSTANTIATE SELECTEDSPELL'S PREFAB AND SET VELOCITY AND ROTATION
    }

    public void AddAbility(AbilitySO newAbility)
    {
        if (dnaStones <= 0) return;
        
        if (newAbility.GetType() == typeof(TransformationSO))
        {
            if (!unlockedTransformations.Contains(newAbility))
            {
                AddTransformation((TransformationSO)newAbility);
                OnTransformationUnlocked?.Invoke((TransformationSO)newAbility);
            }
        }
        else if (newAbility.GetType() == typeof(SpellSO))
        {
            AddSpell((SpellSO) newAbility);
        }
        
        SpendDNAStone();
    }

    public void AddDNAStone()
    {
        dnaStones++;
        // SFX
    }

    public void SpendDNAStone()
    {
        dnaStones--;
        // MAYBE SFX
    }
    
}

public enum AllSpells
{
    Shrink,
    Grow,
    Squarify
    
}

public enum AllTransformations
{
    Normal,
    Shrink,
    Liquify,
    TurnBouncy
}
