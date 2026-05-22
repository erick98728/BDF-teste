using System;
using Tester.Combat;
using UnityEngine;

namespace Tester.Bosses
{
    /// <summary>
    /// Minimal health and defeat base for prototype bosses.
    /// </summary>
    public class BossBase : MonoBehaviour, IDamageable
    {
        [Header("Boss Health")]
        [Min(1)]
        [SerializeField] private int maxHealth = 120;
        [SerializeField] private int currentHealth = 120;

        private bool isDead;

        public event Action Defeated;

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

        public virtual void TakeDamage(int amount)
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
            OnDefeated();
            Destroy(gameObject);
        }

        protected virtual void OnDefeated()
        {
            Debug.Log($"{name} defeated.", this);
            Defeated?.Invoke();
        }

        private void ClampHealthValues()
        {
            maxHealth = Mathf.Max(1, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
