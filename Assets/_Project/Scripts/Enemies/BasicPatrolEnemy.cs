using Tester.Player;
using UnityEngine;

namespace Tester.Enemies
{
    /// <summary>
    /// Basic enemy that patrols in a fixed range and damages Rubens on contact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class BasicPatrolEnemy : EnemyBase
    {
        [Header("Patrol")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float patrolDistance = 3f;

        [Header("Attack")]
        [SerializeField] private int contactDamage = 10;
        [SerializeField] private float contactDamageCooldown = 0.75f;

        private Rigidbody2D rb;
        private Vector2 startPosition;
        private int direction = 1;
        private float nextDamageTime;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            startPosition = transform.position;

            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void FixedUpdate()
        {
            if (IsDead)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            Patrol();
        }

        private void Patrol()
        {
            float minX = startPosition.x - patrolDistance;
            float maxX = startPosition.x + patrolDistance;

            Vector2 position = rb.position;
            position.x += direction * moveSpeed * Time.fixedDeltaTime;

            if (position.x <= minX)
            {
                position.x = minX;
                direction = 1;
                FlipFacing();
            }
            else if (position.x >= maxX)
            {
                position.x = maxX;
                direction = -1;
                FlipFacing();
            }

            rb.MovePosition(position);
        }

        private void FlipFacing()
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.collider.TryGetComponent(out PlayerHealth playerHealth))
            {
                return;
            }

            TryDamagePlayer(playerHealth);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.TryGetComponent(out PlayerHealth playerHealth))
            {
                return;
            }

            TryDamagePlayer(playerHealth);
        }

        private void TryDamagePlayer(PlayerHealth playerHealth)
        {
            if (Time.time < nextDamageTime || contactDamage <= 0)
            {
                return;
            }

            nextDamageTime = Time.time + contactDamageCooldown;
            playerHealth.TakeDamage(contactDamage);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Vector3 center = Application.isPlaying ? (Vector3)startPosition : transform.position;
            Vector3 left = center + Vector3.left * patrolDistance;
            Vector3 right = center + Vector3.right * patrolDistance;
            Gizmos.DrawLine(left, right);
            Gizmos.DrawWireSphere(left, 0.12f);
            Gizmos.DrawWireSphere(right, 0.12f);
        }
#endif
    }
}
