using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    private bool buttonPressed;
    [SerializeField] Animator doorToOpen;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger?");
        if (other.gameObject.TryGetComponent(out AnimalController animal))
        {
            buttonPressed = true;
            //doorToOpen.SetBool("Open", true);
            visual.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out AnimalController animal))
        {
            buttonPressed = false;
            //doorToOpen.SetBool("Open", false);
            visual.sprite = unpressedSprite;
        }
    }
}
