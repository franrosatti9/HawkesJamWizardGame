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
    private bool shrunk = false;
    private bool grown = false;

    private Vector3 defaultScale;
    [Header("Shrink")]
    [SerializeField] private Vector3 shrinkSize;
    [SerializeField] private Transform newWaypointAfterShrink;
    [SerializeField] private int waypointIndexToOverride;

    [Header("Squarify")]
    [SerializeField] SpriteRenderer mainSpriteRenderer;
    [SerializeField] private GameObject squareSpriteRenderer;
    [SerializeField] private Collider2D squareCollider;

    [Header("Grow")] 
    [SerializeField] private Vector3 growSize;

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
        // TODO: SEPARATE CODE IN COMPONENTS MAYBE
        bool transformed = false;
        Vector3 scale = Vector3.one;
        switch (type)
        {
            case AllSpells.Shrink:
                if(!canShrink) break;
                transformed = true;
                Shrink();
                break;
            case AllSpells.Grow:
                if(!canGrow) break;
                transformed = true;
                Grow();
                break;
            case AllSpells.Squarify:
                if (!canSquarify) break;
                transformed = true;
                Squarify();
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        if (!transformed)
        {
            // TODO: FAIL VFX AND SFX
        }
  
    }

    private void Squarify()
    {
        if (!squarified)
        {
            mainSpriteRenderer.gameObject.SetActive(false);
            squareSpriteRenderer.SetActive(true);
            squarified = true;
            
            if(animalController) animalController.Squarify(true);
        }
        else
        {
            // Desquarify (?
            mainSpriteRenderer.gameObject.SetActive(true);
            squareSpriteRenderer.SetActive(false);
            squarified = false;
            
            if(animalController) animalController.Squarify(false);
        }
        
        VFXManager.Instance.CreateVFXAtPoint(mainSpriteRenderer.transform.position, AllVfx.SmokeParticles, defaultScale);
    }

    private void Grow()
    {
        if (!grown)
        {
            LeanTween.scale(gameObject, growSize, 0.25f).setEaseOutBounce();
            VFXManager.Instance.CreateVFXAtPoint(mainSpriteRenderer.transform.position, AllVfx.SmokeParticles, growSize);
            grown = true;
            //transform.localScale = growSize;
        }
        else
        {
            grown = false;
            LeanTween.scale(gameObject, defaultScale, 0.25f).setEaseOutBounce();
            VFXManager.Instance.CreateVFXAtPoint(mainSpriteRenderer.transform.position, AllVfx.SmokeParticles, defaultScale);
            //transform.localPosition = defaultScale;
        }
    }

    private void Shrink()
    {
        Vector3 newScale = shrinkSize;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        LeanTween.scale(gameObject, newScale, 0.3f);
        //transform.localScale = newScale;
        
        VFXManager.Instance.CreateVFXAtPoint(mainSpriteRenderer.transform.position, AllVfx.SmokeParticles, defaultScale);

        if (animalController
            && animalController is WaypointAnimalController waypointController)
        {
            waypointController.OverrideWaypoint(newWaypointAfterShrink, waypointIndexToOverride);
        }
        
        // TODO: UNSHRINK FUNCTION, MAYBE CONTROL WITH SHRINKONCE BOOL

        canShrink = false;
    }
}
