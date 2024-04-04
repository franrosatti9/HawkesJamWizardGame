using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignProgressController : MonoBehaviour
{
    private bool unlockedFirstAbility = false;
    [SerializeField] private InfoSign initialSign;
    [SerializeField] private InfoSign tutSign1;
    [SerializeField] private InfoSign tutSign2;
    [SerializeField] private GameObject bridge;
    [SerializeField] private Vector2 finalBridgePosition;

    public void OnUnlockFirstAbility()
    {
        initialSign.SetMessage("Come back after discovering getting all 7 stones to take your final exam! Explore, and remember, colors will be pleased to help you.");
        
        tutSign1.DestroySign();
        tutSign2.DestroySign();
    }

    public void OnAllStonesGrabbed()
    {
        // OPEN BRIDGE

        LeanTween.move(bridge, finalBridgePosition, 4f);
    }

    public void CheckProgress(int abilitiesUnlocked)
    {
        if (abilitiesUnlocked == 1)
        {
            OnUnlockFirstAbility();
        }
    }

    public void CheckStones(int stoneAmount)
    {
        if (stoneAmount >= 7)
        {
            OnAllStonesGrabbed();
        }
    }
}
