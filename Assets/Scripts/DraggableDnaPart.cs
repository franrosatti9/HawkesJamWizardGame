using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableDnaPart : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private DNAPartTypes dnaType;
    [SerializeField] DNASlot slotParent;
    public DNASlot SlotParent => slotParent;
    public DNAPartTypes DNAType => dnaType;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(slotParent.transform);
        transform.localPosition = Vector2.zero;
    }

    public void SetNewSlot(DNASlot newSlot)
    {
        slotParent = newSlot;
        transform.SetParent(newSlot.transform);
        transform.localPosition = Vector2.zero;
        
    }
}

public enum DNAPartTypes
{
    A,
    T,
    C,
    G
}
