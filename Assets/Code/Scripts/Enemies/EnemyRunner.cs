using System;
using UnityEngine;

public class EnemyRunner : Enemy
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int initialMoveDirection = 1;
    [SerializeField] private bool destroyOnContact = true;
    
    private Rigidbody2D rb;
    private int currentMoveDirection;
    private bool isFacingRight;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        currentMoveDirection = initialMoveDirection;
        isAttacking = true;
        
        isFacingRight = (initialMoveDirection > 0);
        Vector3 localScale = transform.localScale;
        if (isFacingRight)
        {
            if (localScale.x < 0) localScale.x *= -1f; // Ensure positive if facing right
        }
        else
        {
            if (localScale.x > 0) localScale.x *= -1f; // Ensure negative if facing left
        }
        transform.localScale = localScale;
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        // Always going down
        rb.velocity = new Vector2(currentMoveDirection * moveSpeed, rb.velocity.y);

        Flip();
    }

    void Flip()
    {
        bool shouldFaceRight = currentMoveDirection > 0;
        if (isFacingRight != shouldFaceRight)
        {
            isFacingRight = shouldFaceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player") && destroyOnContact)
        {
            // I'll think about this later.
            Die();
        }
    }
}
