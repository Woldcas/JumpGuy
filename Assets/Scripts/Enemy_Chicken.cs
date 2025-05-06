using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chicken : Enemy
{
    [Header("Chicken Specific")]
    [SerializeField] private float aggroDuration;
    [SerializeField] private float detectionRange;

    private bool canFlip = true;
    private float agroTimer;
    private bool playerDetected;

    protected override void Update()
    {
        base.Update();

        agroTimer -= Time.deltaTime;

        if (isDead)
            return;

        if (playerDetected)
        {
            canMove = true;
            agroTimer = aggroDuration;
        }

        if(agroTimer < 0)
            canMove = false;

        HandleMovement();

        if (isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInfrontDetected || isWallDetected)
        {
            
            Flip();
            canMove = false;
            rb.velocity = Vector2.zero;
           
        }
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        
        HandleFlip(player.transform.position.x);

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }

    protected override void HandleFlip(float xValue)
    {
        if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
        {
            if (canFlip)
            {
                canFlip = false;
                Invoke(nameof(Flip), .3f);
            }
        }
    }

    protected override void Flip()
    {
        base.Flip();
        canFlip = true;
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);
    }
}
