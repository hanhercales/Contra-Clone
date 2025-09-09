using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensures the player always has a Rigidbody2D
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private LayerMask groundLayer; // Define what is considered 'ground'
    [SerializeField] private Transform groundCheck; // A point to check if the player is grounded
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    // Collider adjustment
    [SerializeField] private Collider2D playerCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    
    private float horizontalInput; // Stores the current horizontal input received
    public bool isFacingRight {private set; get;} = true;
        
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
        if (playerCollider != null)
        {
            if (playerCollider is BoxCollider2D boxCollider)
            {
                originalColliderSize = boxCollider.size;
                originalColliderOffset = boxCollider.offset;
            }
            else
            {
                Debug.LogWarning("Player collider is not a BoxCollider2D");
            }
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        if(rb.velocity != Vector2.zero) isMoving = true;
        else isMoving = false;
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Crouching
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || (!isGrounded && isCrouching))
        {
            StopCrouching();
        }
        
        // Shooting and Aiming
        isShooting = Input.GetKey(KeyCode.J); // Shooting
        isAimingUp = Input.GetKey(KeyCode.W); // Aiming up
        isAimingDown = Input.GetKey(KeyCode.S) && !isCrouching; // Aiming down

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        if (isCrouching)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            isMoving = false; // Set isMoving to false
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            isMoving = (horizontalInput != 0);
        }

        Flip();
    }
    
    public void SetHorizontalMovement(float input)
    {
        horizontalInput = input;
    }


    public void Jump()
    {
        if (isGrounded && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;

            if (isCrouching) StopCrouching();
        }
    }

    public void Crouch()
    {
        if (!isCrouching && isGrounded)
        {
            isCrouching = true;
            if (playerCollider is BoxCollider2D boxCollider)
            {
                boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
                boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * 0.25f);
            }
        }
    }

    public void StopCrouching()
    {
        if (isCrouching)
        {
            isCrouching = false;
            if (playerCollider is BoxCollider2D boxCollider)
            {
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;
            }
        }
    }
    
    // public void StopMovement() { rb.velocity = new Vector2(0, rb.velocity.y); }

    private void CheckIfGrounded()
    {
        bool currentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = currentlyGrounded;
    }

    private void Flip()
    {
        if (isCrouching || isDead || isHurt) return;
        
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