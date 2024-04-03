using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool canMove = true;
    [SerializeField] protected float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Squarify(bool on)
    {
        if (on)
        {
            // TODO: Clean movement disable, move to waypoint class
            EnableMovement(false);
            gameObject.layer = LayerMask.NameToLayer("AnimalPlayerCollide");
        }
        else
        {
            EnableMovement(true);
            gameObject.layer = LayerMask.NameToLayer("Animal");
        }
    }

    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
