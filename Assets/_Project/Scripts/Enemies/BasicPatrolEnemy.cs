using Tester.Player;
using UnityEngine;

namespace Tester.Enemies
{
    /// <summary>
    /// Simple Bosque enemy that walks between two limits and hurts Rubens on contact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class BasicPatrolEnemy : EnemyBase
    {
        [Header("Patrol")]
        [Min(0f)]
        [SerializeField] private float moveSpeed = 2f;
        [Tooltip("Horizontal distance from the spawn point to each patrol limit.")]
        [Min(0f)]
        [SerializeField] private float patrolDistance = 2f;

        [Header("Contact Damage")]
        [Min(1)]
        [SerializeField] private int contactDamage = 10;
        [Min(0f)]
        [SerializeField] private float damageInterval = 0.75f;

        private Rigidbody2D rb;
        private Vector2 patrolOrigin;
        private int moveDirection = 1;
        private float nextDamageTime;

        protected override void Awake()
        {
            base.Awake();

            rb = GetComponent<Rigidbody2D>();
            patrolOrigin = rb.position;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            moveSpeed = Mathf.Max(0f, moveSpeed);
            patrolDistance = Mathf.Max(0f, patrolDistance);
            contactDamage = Mathf.Max(1, contactDamage);
            damageInterval = Mathf.Max(0f, damageInterval);
        }

        private void FixedUpdate()
        {
            if (IsDead)
            {
                StopHorizontalMovement();
                return;
            }

            if (moveSpeed <= 0f || patrolDistance <= 0f)
            {
                StopHorizontalMovement();
                return;
            }

            TurnAtPatrolLimit();
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryDamagePlayer(collision.collider);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TryDamagePlayer(collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryDamagePlayer(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryDamagePlayer(other);
        }

        private void TurnAtPatrolLimit()
        {
            float leftLimit = patrolOrigin.x - patrolDistance;
            float rightLimit = patrolOrigin.x + patrolDistance;

            if (moveDirection > 0 && rb.position.x >= rightLimit)
            {
                SetMoveDirection(-1);
            }
            else if (moveDirection < 0 && rb.position.x <= leftLimit)
            {
                SetMoveDirection(1);
            }
        }

        private void SetMoveDirection(int newDirection)
        {
            moveDirection = newDirection;

            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * moveDirection;
            transform.localScale = localScale;
        }

        private void StopHorizontalMovement()
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        private void TryDamagePlayer(Collider2D other)
        {
            if (IsDead || Time.time < nextDamageTime)
            {
                return;
            }

            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if (playerHealth == null)
            {
                return;
            }

            nextDamageTime = Time.time + damageInterval;
            playerHealth.TakeDamage(contactDamage);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 origin = Application.isPlaying
                ? patrolOrigin
                : transform.position;

            Vector3 leftLimit = origin + Vector3.left * patrolDistance;
            Vector3 rightLimit = origin + Vector3.right * patrolDistance;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(leftLimit, rightLimit);
            Gizmos.DrawWireSphere(leftLimit, 0.08f);
            Gizmos.DrawWireSphere(rightLimit, 0.08f);
        }
    }
}
