using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensures the player always has a Rigidbody2D
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStateMachine playerStateMachine; // Reference to the FSM for calling state updates (e.g., SetGrounded)
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer; // Define what is considered 'ground'
    [SerializeField] private Transform groundCheck; // A point to check if the player is grounded
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    private float horizontalInput; // Stores the current horizontal input received
    private bool jumpInputReceived; // Flag for jump input
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStateMachine = GetComponent<PlayerStateMachine>(); // Get the FSM reference
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        if (Input.GetButtonDown("Jump"))
        {
            jumpInputReceived = true;
        }

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        Flip();
    }
    
    public void SetHorizontalMovement(float input)
    {
        horizontalInput = input;
    }


    public void Jump()
    {
        if (playerStateMachine.IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpInputReceived = false;
        }
    }
    
    // public void Crouch() { ... }
    // public void StopMovement() { rb.velocity = new Vector2(0, rb.velocity.y); }

    private void CheckIfGrounded()
    {
        bool currentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        playerStateMachine.SetPlayerGrounded(currentlyGrounded); 
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Flip the X scale
            transform.localScale = localScale;
        }
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