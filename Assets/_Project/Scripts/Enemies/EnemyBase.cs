using Tester.Combat;
using UnityEngine;

namespace Tester.Enemies
{
    /// <summary>
    /// Simple enemy health and damage receiver for prototype combat.
    /// </summary>
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Enemy Health")]
        [SerializeField] private int maxHealth = 30;
        [SerializeField] private int currentHealth = 30;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsDead => currentHealth <= 0;

        protected virtual void Awake()
        {
            if (maxHealth <= 0)
            {
                maxHealth = 1;
            }

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        public virtual void TakeDamage(int amount)
        {
            if (amount <= 0 || IsDead)
            {
                return;
            }

            currentHealth = Mathf.Max(0, currentHealth - amount);
            Debug.Log($"EnemyBase: {name} recebeu {amount} de dano. Vida: {currentHealth}/{maxHealth}", this);

            if (currentHealth == 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Debug.Log($"EnemyBase: {name} morreu.", this);
            Destroy(gameObject);
        }
    }
}
