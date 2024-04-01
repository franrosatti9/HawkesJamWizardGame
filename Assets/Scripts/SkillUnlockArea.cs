using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlockArea : InteractableBase
{
    private bool _playerInArea = false;
    
    // Maybe put some particles or VFX when interacting
    private GameObject interactedSprites;
    void Start()
    {
        canvas.SetActive(false);
        canvasText.text = interactText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(PlayerController player)
    {
        if(!_playerInArea) return;

        if (interactOnce)
        {
            Activated(false);
            _interactable = false;
            Destroy(GetComponent<Collider2D>());
            Destroy(canvas);
            canvas = null;
            canvasText = null;
        }
        
        player.AddDNAStone();
        Debug.Log("Interacted Stone");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_interactable) return;
        Activated(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Activated(false);
    }

    void Activated(bool enabled)
    {
        canvas.SetActive(enabled);
        _playerInArea = enabled;
    }
}
