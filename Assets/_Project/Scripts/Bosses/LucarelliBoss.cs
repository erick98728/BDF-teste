using System.Collections;
using Tester.Core;
using Tester.Player;
using UnityEngine;

namespace Tester.Bosses
{
    /// <summary>
    /// Prototype Lucarelli boss with two simple patterns: dash charge and close-range strike.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class LucarelliBoss : BossBase
    {
        [Header("Target")]
        [SerializeField] private Transform rubens;

        [Header("Pattern - Charge")]
        [SerializeField] private float chargeSpeed = 8f;
        [SerializeField] private float chargeDuration = 0.45f;
        [SerializeField] private int chargeDamage = 20;

        [Header("Pattern - Close Strike")]
        [SerializeField] private Transform strikePoint;
        [SerializeField] private float strikeRadius = 1.2f;
        [SerializeField] private int strikeDamage = 15;
        [SerializeField] private LayerMask playerLayer;

        [Header("Timing")]
        [SerializeField] private float timeBetweenAttacks = 1.2f;

        private Rigidbody2D rb;
        private bool isAttacking;
        private float nextAttackTime;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void Update()
        {
            if (IsDead || isAttacking || Time.time < nextAttackTime || rubens == null)
            {
                return;
            }

            float distanceToPlayer = Vector2.Distance(transform.position, rubens.position);
            if (distanceToPlayer <= strikeRadius * 1.5f)
            {
                StartCoroutine(CloseStrikeRoutine());
            }
            else
            {
                StartCoroutine(ChargeRoutine());
            }
        }

        private IEnumerator ChargeRoutine()
        {
            isAttacking = true;
            nextAttackTime = Time.time + timeBetweenAttacks;

            Vector2 direction = (rubens.position.x >= transform.position.x) ? Vector2.right : Vector2.left;
            SetFacing(direction.x);

            float timer = 0f;
            while (timer < chargeDuration)
            {
                rb.velocity = new Vector2(direction.x * chargeSpeed, rb.velocity.y);
                timer += Time.deltaTime;
                yield return null;
            }

            rb.velocity = new Vector2(0f, rb.velocity.y);
            isAttacking = false;
        }

        private IEnumerator CloseStrikeRoutine()
        {
            isAttacking = true;
            nextAttackTime = Time.time + timeBetweenAttacks;

            yield return new WaitForSeconds(0.2f);

            if (strikePoint == null)
            {
                strikePoint = transform;
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(strikePoint.position, strikeRadius, playerLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(strikeDamage);
                }
            }

            yield return new WaitForSeconds(0.15f);
            isAttacking = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!isAttacking)
            {
                return;
            }

            if (collision.collider.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(chargeDamage);
            }
        }

        protected override void Die()
        {
            Debug.Log("LucarelliBoss: derrotado. Iniciando sequência de recompensa.", this);

            AbilityManager abilityManager = null;
            if (rubens != null)
            {
                rubens.TryGetComponent(out abilityManager);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.HandleLucarelliDefeat(abilityManager);
            }
            else if (abilityManager != null)
            {
                abilityManager.UnlockDash();
            }

            base.Die();
        }

        private void SetFacing(float directionX)
        {
            if (Mathf.Abs(directionX) < 0.01f)
            {
                return;
            }

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(directionX);
            transform.localScale = scale;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (strikePoint == null)
            {
                return;
            }

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(strikePoint.position, strikeRadius);
        }
#endif
    }
}
