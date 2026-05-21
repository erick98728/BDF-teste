using System.Collections;
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

        [Header("Dash")]
        [SerializeField] private AbilityManager abilityManager;
        [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
        [SerializeField] private float dashSpeed = 14f;
        [SerializeField] private float dashDuration = 0.18f;
        [SerializeField] private float dashCooldown = 0.6f;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody2D rb;
        private float moveInput;
        private bool jumpPressed;
        private bool isGrounded;
        private bool isDashing;
        private float nextDashTime;
        private Coroutine dashRoutine;

        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public bool IsGrounded => isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (abilityManager == null)
            {
                abilityManager = GetComponent<AbilityManager>();
            }

            if (groundCheck == null)
            {
                Debug.LogWarning("PlayerController2D: GroundCheck não foi configurado no Inspector.", this);
            }
        }

        private void Update()
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded && !isDashing)
            {
                jumpPressed = true;
            }

            TryStartDash();
            HandleFacing();
        }

        private void FixedUpdate()
        {
            UpdateGroundedState();

            if (isDashing)
            {
                return;
            }

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

        private void TryStartDash()
        {
            if (abilityManager == null || !abilityManager.DashUnlocked)
            {
                return;
            }

            if (isDashing || Time.time < nextDashTime)
            {
                return;
            }

            if (!Input.GetKeyDown(dashKey))
            {
                return;
            }

            float dashDirection = Mathf.Abs(moveInput) > 0.01f ? Mathf.Sign(moveInput) : Mathf.Sign(transform.localScale.x);
            if (Mathf.Abs(dashDirection) < 0.01f)
            {
                dashDirection = 1f;
            }

            if (dashRoutine != null)
            {
                StopCoroutine(dashRoutine);
            }

            dashRoutine = StartCoroutine(DashRoutine(dashDirection));
        }

        private IEnumerator DashRoutine(float direction)
        {
            isDashing = true;
            nextDashTime = Time.time + dashCooldown;

            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(direction * dashSpeed, 0f);

            yield return new WaitForSeconds(dashDuration);

            rb.gravityScale = originalGravity;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            isDashing = false;
            dashRoutine = null;
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
            if (isDashing)
            {
                return;
            }

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
