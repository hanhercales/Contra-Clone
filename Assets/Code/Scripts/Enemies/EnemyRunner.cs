using UnityEngine;

public class EnemyRunner : Enemy
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int initialMoveDirection = 1;
    [SerializeField] private bool destroyOnContact = true;

    private Rigidbody2D rb;
    private int currentMoveDirection;
    private bool isFacingRight = true;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        currentMoveDirection = initialMoveDirection;
        isAttacking = true;
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = new Vector2(currentMoveDirection * moveSpeed, rb.velocity.y);
        Flip(currentMoveDirection);
    }

    void Flip(int direction)
    {
        if (direction == 0) return; // Don't flip if not moving

        bool shouldFaceRight = direction > 0;
        if (isFacingRight != shouldFaceRight)
        {
            isFacingRight = shouldFaceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Flip the X scale
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
    }
}