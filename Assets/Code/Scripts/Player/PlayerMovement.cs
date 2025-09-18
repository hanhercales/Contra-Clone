using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float moveSpeed = 3f; // Player's movement speed
    [SerializeField] private float jumpForce = 6f; // Player's jump force
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float groundCheckRadius = 0.4f;
    private float horizontalInput;
    public bool isFacingRight { private set; get; } = true;
    public bool isGrounded;
    public bool isCrouching;
    public bool isMoving;
    public bool isDead;
    public bool isHurt;
    public bool isShooting;
    public bool isAimingUp;
    public bool isAimingDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        isShooting = Input.GetKey(KeyCode.J);
        isAimingUp = Input.GetKey(KeyCode.W);
        isAimingDown = Input.GetKey(KeyCode.S);

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        Flip();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void CheckIfGrounded()
    {   
        bool currentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = currentlyGrounded;
    }
    
    private void Flip()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}