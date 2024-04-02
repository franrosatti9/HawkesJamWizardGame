using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTarget : MonoBehaviour
{
    [SerializeField] bool canShrink;
    [SerializeField] bool canGrow;
    [SerializeField] bool canSquarify;

    private AnimalController animalController;
    private bool squarified = false;

    private Vector3 defaultScale;
    [SerializeField] private Vector3 shrinkSize;

    [SerializeField] SpriteRenderer mainSpriteRenderer;
    [SerializeField] private GameObject squareSpriteRenderer;
    [SerializeField] private Collider2D squareCollider;
    [SerializeField] private LayerMask defaultLayer;
    [SerializeField] private LayerMask squareLayer;
    void Start()
    {
        defaultScale = transform.localScale;
        TryGetComponent(out animalController);
    }

    private void ResetScaleAndValues()
    {
        //speed = originalSpeed;
        //rb.gravityScale = originalGravityScale;
        //rb.sharedMaterial = normalPMaterial;
        
        Vector3 newScale = defaultScale;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        transform.localScale = newScale;
    }

    public void HandleReceivedSpell(AllSpells type)
    {
        bool transformed = false;
        switch (type)
        {
            case AllSpells.Shrink:
                if(!canShrink) return;
                transformed = true;
                Shrink();
                break;
            case AllSpells.Grow:
                if(!canGrow) return;
                transformed = true;
                Grow();
                break;
            case AllSpells.Squarify:
                if (!canSquarify) return;
                transformed = true;
                Squarify();
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        // TODO: VISUAL EFFECT IF TRANSFORMED
        
        // if(transformed) vfx...
    }

    private void Squarify()
    {
        if (!squarified)
        {
            mainSpriteRenderer.gameObject.SetActive(false);
            squareSpriteRenderer.SetActive(true);
            squarified = true;
            
            if(animalController) animalController.Squarify(true);
            // TODO: IF THERES A WAYPOINT OR MOVEMENT CONTROLLER, DISABLE MOVEMENT, THEN RESUME WHEN DESQUARIFIED
        }
        else
        {
            // Desquarify (?
            mainSpriteRenderer.gameObject.SetActive(true);
            squareSpriteRenderer.SetActive(false);
            squarified = true;
            
            if(animalController) animalController.Squarify(false);
        }
    }

    private void Grow()
    {
        throw new NotImplementedException();
    }

    private void Shrink()
    {
        Vector3 newScale = shrinkSize;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        transform.localScale = newScale;
    }
}
