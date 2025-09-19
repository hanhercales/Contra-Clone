using UnityEngine;

public class EnemyRunner : Enemy
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int initialMoveDirection = 1; // 1 is right, -1 is left
    [SerializeField] private bool destroyOnContact = true;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private Transform playerTarget;
    
    private Rigidbody2D rb;
    private int currentMoveDirection;
    private bool isFacingRight = true;

    protected override void Awake()
    {
        base.Awake();
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
            else Debug.LogWarning("No player target found");
        }
        rb = GetComponent<Rigidbody2D>();
        currentMoveDirection = initialMoveDirection;
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
        if (distanceToPlayer < detectionRange)
        {
            isAttacking = true;
            rb.velocity = new Vector2(currentMoveDirection * moveSpeed, rb.velocity.y);
        }
        Flip(currentMoveDirection);
    }

    void Flip(int direction)
    {
        if (direction == 0) return;
        bool shouldFaceRight = direction > 0;
        if (isFacingRight != shouldFaceRight)
        {
            isFacingRight = shouldFaceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Flip the scale :>
            transform.localScale = localScale;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Player") && destroyOnContact)
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageToPlayer);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}