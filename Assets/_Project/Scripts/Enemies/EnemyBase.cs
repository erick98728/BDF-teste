using Tester.Combat;
using UnityEngine;

namespace Tester.Enemies
{
    /// <summary>
    /// Simple health and death base for prototype enemies.
    /// </summary>
    [DisallowMultipleComponent]
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Enemy")]
        [Min(1)]
        [SerializeField] private int maxHealth = 30;
        [SerializeField] private int currentHealth = 30;

        private bool isDead;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsDead => isDead;

        protected virtual void Awake()
        {
            ClampHealthValues();
            isDead = currentHealth <= 0;
        }

        protected virtual void OnValidate()
        {
            ClampHealthValues();
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || isDead)
            {
                return;
            }

            currentHealth = Mathf.Max(0, currentHealth - amount);
            Debug.Log($"{name} took {amount} damage. Health: {currentHealth}/{maxHealth}.", this);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            Debug.Log($"{name} died.", this);
            Destroy(gameObject);
        }

        private void ClampHealthValues()
        {
            maxHealth = Mathf.Max(1, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
