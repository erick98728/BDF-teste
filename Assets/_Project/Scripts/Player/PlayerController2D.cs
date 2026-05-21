using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Basic 2D metroidvania-like controller for PC prototype movement.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 12f;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody2D rb;
        private float moveInput;
        private bool jumpPressed;
        private bool isGrounded;

        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public bool IsGrounded => isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (groundCheck == null)
            {
                Debug.LogWarning("PlayerController2D: GroundCheck não foi configurado no Inspector.", this);
            }
        }

        private void Update()
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jumpPressed = true;
            }

            HandleFacing();
        }

        private void FixedUpdate()
        {
            UpdateGroundedState();
            HandleMovement();
            HandleJump();
        }

        private void HandleMovement()
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        private void HandleJump()
        {
            if (!jumpPressed)
            {
                return;
            }

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpPressed = false;
        }

        private void UpdateGroundedState()
        {
            if (groundCheck == null)
            {
                isGrounded = false;
                return;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void HandleFacing()
        {
            if (Mathf.Abs(moveInput) < 0.01f)
            {
                return;
            }

            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(moveInput);
            transform.localScale = localScale;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
#endif
    }
}
