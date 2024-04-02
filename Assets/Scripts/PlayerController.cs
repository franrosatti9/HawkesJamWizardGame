using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private float hInput;
    [SerializeField] float originalSpeed;
    private float originalGravityScale;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpForce = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] LayerMask interactableMask;
    [Header("Transformations")] 
    private Vector3 defaultScale;
    [SerializeField] private Vector3 shrinkSize;
    [SerializeField] private float liquifySpeed = 3f;
    [SerializeField] private float liquifyGravityScale = 3f;
    [SerializeField] private PhysicsMaterial2D normalPMaterial;
    [SerializeField] private PhysicsMaterial2D bouncyPMaterial;

    private bool _mainControllerEnabled = true;
    private bool _inputEnabled = true;

    private PlayerAbilities playerAbilites;
    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAbilites = GetComponent<PlayerAbilities>();
        playerAnimations = GetComponent<PlayerAnimations>();
        defaultScale = transform.localScale;
        playerAbilites.OnPlayerTransformed += HandlePlayerTransformed;
    }

    private void Start()
    {
        originalSpeed = speed;
        originalGravityScale = liquifyGravityScale;
    }

    private void HandlePlayerTransformed(AllTransformations transformation)
    {
        switch (transformation)
        {
            case AllTransformations.Normal:
                _mainControllerEnabled = true;
                playerAnimations.Transform(transformation, ResetScaleAndValues);
                //ResetToNormal();
                break;
            case AllTransformations.Shrink:
                _mainControllerEnabled = true;
                playerAnimations.Transform(transformation, Shrink);
                //Shrink();
                break;
            case AllTransformations.Liquify:
                _mainControllerEnabled = true;
                playerAnimations.Transform(transformation, Liquify);
                break;
            case AllTransformations.TurnBouncy:
                TurnBouncy();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null);
        }
    }

    private void ResetScaleAndValues()
    {
        speed = originalSpeed;
        rb.gravityScale = originalGravityScale;
        gameObject.layer = 7; // Normal Player Layer
        rb.sharedMaterial = normalPMaterial;
        
        Vector3 newScale = defaultScale;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        transform.localScale = newScale;
    }

    private void Shrink()
    {
        speed = originalSpeed;
        rb.gravityScale = originalGravityScale;
        gameObject.layer = 7; // Normal Player Layer
        rb.sharedMaterial = normalPMaterial;
        
        Vector3 newScale = shrinkSize;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        transform.localScale = newScale;
        
    }

    void Liquify()
    {
        ResetScaleAndValues();
        speed = liquifySpeed;
        rb.gravityScale = liquifyGravityScale;
        gameObject.layer = 10; // LiquidPlayer Layer

    }

    void TurnBouncy()
    {
        ResetScaleAndValues();

        rb.sharedMaterial = bouncyPMaterial;
    }

    void Update()
    {
        if (!_inputEnabled) return;
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.ToggleAbilitiesUI();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UIManager.instance.EnabledTransformSelector(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            UIManager.instance.EnabledTransformSelector(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAbilites.SwitchCurrentTransformation();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            playerAbilites.UseSpell();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAbilites.Transform();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
        if (!_mainControllerEnabled) return;
        
        hInput = Input.GetAxisRaw("Horizontal");
        
        if(hInput != 0) playerAnimations.SetWalking(true);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && hInput < 0f || !isFacingRight && hInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Interact()
    {
        Debug.Log("Tried to Interact");
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 2f, interactableMask);
        if (hit != null && hit.gameObject.TryGetComponent<InteractableBase>(out InteractableBase interactable))
        {
            interactable.Interact(this);
            Debug.Log("Interacted Player");
        }
    }

    public void AddDNAStone()
    {
        playerAbilites.AddDNAStone();
    }
}
