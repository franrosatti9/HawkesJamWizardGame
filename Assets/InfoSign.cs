using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSign : MonoBehaviour
{
    [SerializeField] private string message;
    private bool playerInFront = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.instance.ShowInfoText(message);
            playerInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.instance.HideInfoText();
            playerInFront = false;
        }
    }

    public void SetMessage(string newMessage)
    {
        message = newMessage;
    }

    public void DestroySign()
    {
        if(playerInFront) UIManager.instance.HideInfoText();
        
        Destroy(this);
    }
}
