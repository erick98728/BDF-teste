using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [Tooltip("Child transform positioned just below Rubens' feet.")]
    [SerializeField] private Transform groundCheck;
    [Tooltip("Radius used around GroundCheck to detect walkable floor colliders.")]
    [SerializeField] private float groundCheckRadius = 0.18f;
    [Tooltip("Only colliders on these layers count as ground for jumping.")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Input")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool jumpRequested;
    private bool isGrounded;
    private bool facingRight = true;

    public bool IsGrounded => isGrounded;
    public bool FacingRight => facingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadInput();
        CheckGround();
        HandleJumpRequest();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        // Raw input keeps keyboard movement immediate for the current PC prototype.
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey))
        {
            jumpRequested = true;
        }
    }

    private void CheckGround()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void HandleJumpRequest()
    {
        if (!jumpRequested)
        {
            return;
        }

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpRequested = false;
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        if (horizontalInput > 0f && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0f && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
