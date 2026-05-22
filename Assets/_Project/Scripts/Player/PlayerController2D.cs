using System.Collections;
using UnityEngine;

namespace Tester.Player
{
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

        [Header("Dash")]
        [Tooltip("Ability state checked before Rubens can Dash.")]
        [SerializeField] private AbilityManager abilityManager;
        [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
        [Min(0f)]
        [SerializeField] private float dashSpeed = 16f;
        [Min(0.01f)]
        [SerializeField] private float dashDuration = 0.15f;
        [Min(0f)]
        [SerializeField] private float dashCooldown = 0.5f;

        private Rigidbody2D rb;
        private float horizontalInput;
        private bool jumpRequested;
        private bool dashRequested;
        private bool isGrounded;
        private bool isDashing;
        private bool facingRight = true;
        private float dashDirection = 1f;
        private float nextDashTime;
        private Coroutine dashRoutine;

        public bool IsGrounded => isGrounded;
        public bool FacingRight => facingRight;
        public bool IsDashing => isDashing;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            abilityManager ??= GetComponent<AbilityManager>();
        }

        private void Update()
        {
            if (Time.timeScale <= 0f)
            {
                ClearPendingInput();
                return;
            }

            ReadInput();
            CheckGround();
            HandleJumpRequest();
            HandleFlip();
            HandleDashRequest();
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

            if (Input.GetKeyDown(dashKey))
            {
                dashRequested = true;
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
            if (isDashing)
            {
                rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
                return;
            }

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

        private void HandleDashRequest()
        {
            if (!dashRequested)
            {
                return;
            }

            dashRequested = false;

            if (abilityManager == null || !abilityManager.DashUnlocked)
            {
                return;
            }

            if (isDashing || Time.time < nextDashTime)
            {
                return;
            }

            StartDash();
        }

        private void StartDash()
        {
            dashDirection = Mathf.Abs(horizontalInput) > 0f
                ? Mathf.Sign(horizontalInput)
                : facingRight ? 1f : -1f;

            nextDashTime = Time.time + dashCooldown;

            if (dashRoutine != null)
            {
                StopCoroutine(dashRoutine);
            }

            dashRoutine = StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            isDashing = true;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);

            yield return new WaitForSeconds(dashDuration);

            isDashing = false;
            dashRoutine = null;
        }

        private void ClearPendingInput()
        {
            horizontalInput = 0f;
            jumpRequested = false;
            dashRequested = false;
        }

        private void OnValidate()
        {
            dashSpeed = Mathf.Max(0f, dashSpeed);
            dashDuration = Mathf.Max(0.01f, dashDuration);
            dashCooldown = Mathf.Max(0f, dashCooldown);
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
}
