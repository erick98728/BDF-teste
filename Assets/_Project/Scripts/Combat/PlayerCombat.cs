using Tester.Combat;
using UnityEngine;

namespace Tester.Combat
{
    /// <summary>
    /// Basic melee combat for Rubens using an overlap circle hit detection.
    /// </summary>
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private KeyCode attackKey = KeyCode.J;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 0.25f;

        [Header("Hitbox")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRadius = 0.5f;
        [SerializeField] private LayerMask enemyLayer;

        private float nextAttackTime;

        public int AttackDamage => attackDamage;

        private void Update()
        {
            if (!Input.GetKeyDown(attackKey))
            {
                return;
            }

            if (Time.time < nextAttackTime)
            {
                return;
            }

            DoAttack();
        }

        private void DoAttack()
        {
            if (attackPoint == null)
            {
                Debug.LogWarning("PlayerCombat: attackPoint não configurado.", this);
                return;
            }

            nextAttackTime = Time.time + attackCooldown;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
            int damagedTargets = 0;

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                IDamageable damageable = hitEnemies[i].GetComponent<IDamageable>();
                if (damageable == null)
                {
                    continue;
                }

                damageable.TakeDamage(attackDamage);
                damagedTargets++;
            }

            Debug.Log($"PlayerCombat: ataque executado. Alvos atingidos: {damagedTargets}", this);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
#endif
    }
}
