using System.Collections.Generic;
using UnityEngine;

namespace Tester.Combat
{
    /// <summary>
    /// Basic short-range katana attack for the prototype.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Combat")]
        [Min(1)]
        [SerializeField] private int attackDamage = 10;
        [Min(0f)]
        [SerializeField] private float attackCooldown = 0.25f;

        [Header("Attack Area")]
        [Tooltip("Child transform that marks where the katana hit is checked.")]
        [SerializeField] private Transform attackPoint;
        [Min(0.01f)]
        [SerializeField] private float attackRadius = 0.65f;
        [SerializeField] private LayerMask enemyLayers;

        [Header("Input")]
        [SerializeField] private KeyCode attackKey = KeyCode.J;

        private readonly HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
        private float nextAttackTime;

        public int AttackDamage => attackDamage;

        private void Update()
        {
            if (Time.timeScale <= 0f)
            {
                return;
            }

            if (Input.GetKeyDown(attackKey))
            {
                TryAttack();
            }
        }

        private void OnValidate()
        {
            attackDamage = Mathf.Max(1, attackDamage);
            attackCooldown = Mathf.Max(0f, attackCooldown);
            attackRadius = Mathf.Max(0.01f, attackRadius);
        }

        public bool TryAttack()
        {
            if (Time.timeScale <= 0f)
            {
                return false;
            }

            if (Time.time < nextAttackTime)
            {
                return false;
            }

            if (attackPoint == null)
            {
                Debug.LogWarning("PlayerCombat needs an Attack Point before it can attack.", this);
                return false;
            }

            nextAttackTime = Time.time + attackCooldown;
            Attack();
            return true;
        }

        private void Attack()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRadius,
                enemyLayers
            );

            damagedTargets.Clear();

            foreach (Collider2D hit in hits)
            {
                IDamageable damageable = hit.GetComponentInParent<IDamageable>();

                if (damageable == null || !damagedTargets.Add(damageable))
                {
                    continue;
                }

                damageable.TakeDamage(attackDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
