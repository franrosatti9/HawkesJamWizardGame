using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [SerializeField] DoorPillar doorToOpen;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private bool inversedButton = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SpellTarget animal))
        {
            if(!inversedButton) doorToOpen.Open();
            else doorToOpen.Close();
            
            visual.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SpellTarget animal))
        {
            if(!inversedButton) doorToOpen.Close();
            else doorToOpen.Open();
            
            visual.sprite = unpressedSprite;
        }
    }
    
}
