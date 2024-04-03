using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [SerializeField] Animator doorToOpen;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger?");
        if (other.gameObject.TryGetComponent(out AnimalController animal))
        {
            //doorToOpen.SetBool("Open", true);
            visual.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out AnimalController animal))
        {
            //doorToOpen.SetBool("Open", false);
            visual.sprite = unpressedSprite;
        }
    }
}
