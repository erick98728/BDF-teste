using System;
using Tester.Combat;
using UnityEngine;

namespace Tester.Bosses
{
    /// <summary>
    /// Base class for prototype bosses with health, damage and death events.
    /// </summary>
    public class BossBase : MonoBehaviour, IDamageable
    {
        [Header("Boss Health")]
        [SerializeField] private int maxHealth = 200;
        [SerializeField] private int currentHealth = 200;

        public event Action<BossBase> OnBossDefeated;

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
            Debug.Log($"BossBase: {name} recebeu {amount} de dano. Vida: {currentHealth}/{maxHealth}", this);

            if (currentHealth == 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Debug.Log($"BossBase: {name} derrotado.", this);
            OnBossDefeated?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
