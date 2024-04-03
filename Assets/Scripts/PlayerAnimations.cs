using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private bool isWalking;
    
    
    [SerializeField] private Animator currentAnimator;
    [SerializeField] private Animator normalAnimator;
    [SerializeField] private Animator liquidAnimator;
    [SerializeField] private Animator bouncyAnimator;

    public event Action<bool> OnTransformAnim; 

    private AllTransformations _currentForm = AllTransformations.Normal;
    void Start()
    {
        currentAnimator = normalAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWalking(bool walking)
    {
        if (isWalking.Equals(walking)) return;
        
        currentAnimator.SetBool("Walking", walking);
        isWalking = walking;
        
    }

    public void CastSpell()
    {
        if (currentAnimator != normalAnimator) return;
        
        currentAnimator.SetTrigger("CastSpell");
    }
    // TODO:
    public void StartJump()
    {
        
    }
    //TODO: 
    public void EndJump()
    {
        
    }

    public void Transform(AllTransformations newTransformation, Action transformAction = null)
    {
        if (_currentForm == newTransformation) return;

        StopAllCoroutines();
        StartCoroutine(TransformAnimation(_currentForm, newTransformation, transformAction));

        _currentForm = newTransformation;
                
    }

    public IEnumerator TransformAnimation(AllTransformations from, AllTransformations to, Action transformAction = null)
    {
        OnTransformAnim?.Invoke(true);
        currentAnimator.SetTrigger("TransformTo");
        yield return new WaitForSeconds(0.5f);
        transformAction?.Invoke();
        EnableSpriteAndAnimator(to);
        yield return new WaitForSeconds(0.5f);
        OnTransformAnim?.Invoke(false);
        
    }

    public void EnableSpriteAndAnimator(AllTransformations transformations)
    {
        switch (transformations)
        {
            case AllTransformations.Normal:
                //if (currentAnimator == normalAnimator) return; // In case it goes from normal to shrink (same animator)
                currentAnimator = normalAnimator;
                normalAnimator.gameObject.SetActive(true);
                normalAnimator.SetTrigger("TransformFrom");
                
                liquidAnimator.gameObject.SetActive(false);
                bouncyAnimator.gameObject.SetActive(false);
                break;
            case AllTransformations.Shrink:
                //if (currentAnimator == normalAnimator) return; // In case it goes from normal to shrink (same animator)
                currentAnimator = normalAnimator;
                normalAnimator.gameObject.SetActive(true);
                normalAnimator.SetTrigger("TransformFrom");
                
                liquidAnimator.gameObject.SetActive(false);
                bouncyAnimator.gameObject.SetActive(false);
                break;
            case AllTransformations.Liquify:
                currentAnimator = liquidAnimator;
                liquidAnimator.gameObject.SetActive(true);
                liquidAnimator.SetTrigger("TransformFrom");
                
                normalAnimator.gameObject.SetActive(false);
                bouncyAnimator.gameObject.SetActive(false);
                break;
            case AllTransformations.TurnBouncy:
                currentAnimator = bouncyAnimator;
                bouncyAnimator.gameObject.SetActive(true);
                bouncyAnimator.SetTrigger("TransformFrom");
                
                normalAnimator.gameObject.SetActive(false);
                liquidAnimator.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transformations), transformations, null);
        }
    }
}
