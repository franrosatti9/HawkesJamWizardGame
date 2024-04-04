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
    private float defaultJumpForce;
    private float defaultGroundCheckRadius;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D mainCollider;

    [SerializeField] LayerMask interactableMask;
    [Header("Transformations")] 
    private Vector3 defaultScale;
    [SerializeField] private Vector3 shrinkSize;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float shrinkJumpForce;
    [SerializeField] private float shrinkGroundCheckRadius = 0.1f;
    [SerializeField] private float liquifySpeed = 3f;
    [SerializeField] private float liquifyGravityScale = 3f;
    [SerializeField] private PhysicsMaterial2D normalPMaterial;
    [SerializeField] private PhysicsMaterial2D bouncyPMaterial;
    [SerializeField] private PhysicsMaterial2D superBouncyPMaterial;
    [SerializeField] private Collider2D smallCollider;
    
    [Header("Spells")]
    [SerializeField] private float spellFireRate = 2f;
    private float spellCooldown;

    private bool _mainControllerEnabled = true;
    private bool _inputEnabled = true;

    private PlayerAbilities playerAbilites;
    private PlayerAnimations playerAnimations;

    public PlayerAbilities PlayerAbilities => playerAbilites;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAbilites = GetComponent<PlayerAbilities>();
        playerAnimations = GetComponent<PlayerAnimations>();
        

    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameState;
        playerAnimations.OnTransformAnim += EnableInputAndGravity;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameState;
        playerAnimations.OnTransformAnim -= EnableInputAndGravity;
    }

    private void HandleGameState(GameState newState)
    {
        _inputEnabled = newState == GameState.Gameplay;
    }

    private void Start()
    {
        playerAbilites.OnPlayerTransformed += HandlePlayerTransformed;
        
        defaultScale = transform.localScale;
        defaultJumpForce = jumpForce;
        defaultGroundCheckRadius = 0.2f;
        originalSpeed = speed;
        originalGravityScale = rb.gravityScale;
    }

    private void HandlePlayerTransformed(AllTransformations transformation)
    {
        switch (transformation)
        {
            case AllTransformations.Normal:
                playerAnimations.Transform(transformation, ResetScaleAndValues);
                //ResetToNormal();
                break;
            case AllTransformations.Shrink:
                playerAnimations.Transform(transformation, Shrink);
                //Shrink();
                break;
            case AllTransformations.Liquify:
                playerAnimations.Transform(transformation, Liquify);
                break;
            case AllTransformations.TurnBouncy:
                playerAnimations.Transform(transformation, TurnBouncy);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null);
        }
    }

    private void ResetScaleAndValues()
    {
        speed = originalSpeed;
        jumpForce = defaultJumpForce;
        rb.gravityScale = originalGravityScale;
        gameObject.layer = 7; // Normal Player Layer
        rb.sharedMaterial = normalPMaterial;
        mainCollider.enabled = true;
        _mainControllerEnabled = true;
        groundCheckRadius = defaultGroundCheckRadius;
        
        Vector3 newScale = defaultScale;
        newScale.x *= Mathf.Sign(transform.localScale.x);

        if (newScale != transform.localScale) LeanTween.scale(gameObject, newScale, 0.3f).setEaseInOutBounce();
        //transform.localScale = newScale;
    }

    private void Shrink()
    {   
        speed = shrinkSpeed;
        jumpForce = shrinkJumpForce;
        rb.gravityScale = originalGravityScale;
        gameObject.layer = 7; // Normal Player Layer
        rb.sharedMaterial = normalPMaterial;
        _mainControllerEnabled = true;
        groundCheckRadius = shrinkGroundCheckRadius;
        
        Vector3 newScale = shrinkSize;
        newScale.x *= Mathf.Sign(transform.localScale.x);
        //transform.localScale = newScale;

        LeanTween.scale(gameObject, newScale, 0.3f).setEaseInOutBounce();

    }

    void Liquify()
    {
        ResetScaleAndValues();
        mainCollider.enabled = false;
        smallCollider.enabled = true;
        speed = liquifySpeed;
        rb.gravityScale = liquifyGravityScale;
        gameObject.layer = 10; // LiquidPlayer Layer

    }

    void TurnBouncy()
    {
        ResetScaleAndValues();

        //_mainControllerEnabled = false;
        mainCollider.enabled = false;
        smallCollider.enabled = true;
        rb.sharedMaterial = bouncyPMaterial;
    }

    void Update()
    {
        if (!_inputEnabled) return;
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAbilites.SwitchCurrentSpell();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            CastSpell();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAbilites.Transform();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
        if (spellCooldown > 0) spellCooldown -= Time.deltaTime;
        
        if (!_mainControllerEnabled) return;
        
        hInput = Input.GetAxisRaw("Horizontal");
        
        if(hInput != 0) playerAnimations.SetWalking(true);
        else playerAnimations.SetWalking(false);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            if (rb.sharedMaterial == bouncyPMaterial) rb.sharedMaterial = superBouncyPMaterial;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump")){
            if(rb.sharedMaterial == superBouncyPMaterial) rb.sharedMaterial = bouncyPMaterial;
            
            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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

    public void CastSpell()
    {
        if (spellCooldown > 0f || !playerAbilites.SpellSelected()
                               || !IsGrounded() || !playerAbilites.InNormalMode()) return; // Don't use spell if on cooldown or not unlocked, or while jumping
        spellCooldown = spellFireRate; // Reset cooldown if success
        DisableMovement();
        
        playerAnimations.CastSpell();
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

    public void DisableMovement()
    {
        _mainControllerEnabled = false;
        hInput = 0;
    }

    public void EnableMovement()
    {
        _mainControllerEnabled = true;
    }

    public void EnableInputAndGravity(bool transforming)
    {
        Debug.Log("Input Enabled = " + !transforming);
        
        hInput = 0;
        _inputEnabled = !transforming;
        rb.isKinematic = transforming;
        rb.velocity = Vector2.zero;
    }
    

    private void OnDestroy()
    {
        playerAbilites.OnPlayerTransformed -= HandlePlayerTransformed;
    }
}
