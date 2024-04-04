using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignProgressController : MonoBehaviour
{
    private bool unlockedFirstAbility = false;
    [SerializeField] private InfoSign initialSign;
    [SerializeField] private InfoSign tutSign1;
    [SerializeField] private InfoSign tutSign2;

    public void OnUnlockFirstAbility()
    {
        initialSign.SetMessage("Come back after discovering all 6 abilities to take your final exam! Explore, and remember, colors will be pleased to help you.");
        
        tutSign1.DestroySign();
        tutSign2.DestroySign();
    }

    public void OnUnlockAllAbilities()
    {
        // OPEN BRIDGE
    }

    public void CheckProgress(int abilitiesUnlocked)
    {
        if (abilitiesUnlocked == 1)
        {
            OnUnlockFirstAbility();
        }
        else if (abilitiesUnlocked >= 6)
        {
            OnUnlockAllAbilities();
        }
    }
}
