using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DNASlot : MonoBehaviour, IDropHandler
{
    [SerializeField] DraggableDnaPart currentDna;
    public DraggableDnaPart CurrentDNA => currentDna;

    public static event Action OnAnySlotUpdated;
    void Start()
    {
        SetInteractable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("dropped?");
        GameObject dropped = eventData.pointerDrag;
        if (dropped.TryGetComponent(out DraggableDnaPart part))
        {
            //Debug.Log("Dropped part");
            if(part == currentDna) return;
            if (currentDna == null)
            {
                SetCurrentDna(part);
            }
            else
            {
                //Debug.Log("Swap?");
                SwapSlots(part.SlotParent, currentDna, part);
            }
            
        }
        
        OnAnySlotUpdated?.Invoke();
    }

    public void SetCurrentDna(DraggableDnaPart dnaPart)
    {
        dnaPart.SetNewSlot(this);
        currentDna = dnaPart;
    }

    void SwapSlots(DNASlot otherSlot, DraggableDnaPart thisPart, DraggableDnaPart newPart)
    {
        otherSlot.SetCurrentDna(thisPart);
        this.SetCurrentDna(newPart);
    }

    public void SetInteractable(bool isEnabled)
    {
        GetComponent<Image>().raycastTarget = isEnabled;
        transform.GetChild(0).gameObject.GetComponentInChildren<Image>().raycastTarget = isEnabled;
    }
}
