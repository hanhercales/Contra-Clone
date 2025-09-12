using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public Rigidbody2D player;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    private float horizontalInput;
    
    public bool isGrounded;
    public bool isMoving;
    public bool isCrouching;
    public bool isDead;
    public bool isHurt;
    public bool isShooting;
    public bool isAimingUp;
    public bool isAimingDown;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(horizontalInput * moveSpeed, player.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }
    }

    private void CheckIfGrounded()
    {
        bool currentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = currentlyGrounded;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
