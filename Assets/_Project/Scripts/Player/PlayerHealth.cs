using System;
using System.Collections;
using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Handles player life, damage, temporary invulnerability and death state.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;

        [Header("Damage")]
        [SerializeField] private float invulnerabilityDuration = 0.5f;

        private bool isInvulnerable;
        private bool isDead;
        private Coroutine invulnerabilityRoutine;

        public event Action<int, int> OnHealthChanged;
        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnDied;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsInvulnerable => isInvulnerable;
        public bool IsDead => isDead;

        private void Awake()
        {
            if (maxHealth <= 0)
            {
                maxHealth = 1;
            }

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            NotifyHealthChanged();
        }

        public void TakeDamage(int amount)
        {
            if (isDead || isInvulnerable)
            {
                return;
            }

            if (amount <= 0)
            {
                return;
            }

            currentHealth = Mathf.Max(0, currentHealth - amount);
            Debug.Log($"PlayerHealth: Rubens recebeu {amount} de dano. Vida atual: {currentHealth}/{maxHealth}", this);

            OnDamaged?.Invoke();
            NotifyHealthChanged();

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            if (invulnerabilityRoutine != null)
            {
                StopCoroutine(invulnerabilityRoutine);
            }

            invulnerabilityRoutine = StartCoroutine(InvulnerabilityTimer());
        }

        public void Heal(int amount)
        {
            if (isDead)
            {
                return;
            }

            if (amount <= 0)
            {
                return;
            }

            int oldHealth = currentHealth;
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

            if (currentHealth == oldHealth)
            {
                return;
            }

            Debug.Log($"PlayerHealth: Rubens curou {amount}. Vida atual: {currentHealth}/{maxHealth}", this);

            OnHealed?.Invoke();
            NotifyHealthChanged();
        }

        public void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            isInvulnerable = false;

            if (invulnerabilityRoutine != null)
            {
                StopCoroutine(invulnerabilityRoutine);
                invulnerabilityRoutine = null;
            }

            Debug.Log("PlayerHealth: Rubens morreu. Aguardando sistema de respawn/checkpoint.", this);
            OnDied?.Invoke();

            GameManager gameManager = Tester.Core.GameManager.Instance;
            if (gameManager != null)
            {
                gameManager.HandlePlayerDeath(this);
            }
        }

        public void RestoreFullHealth()
        {
            currentHealth = maxHealth;
            isDead = false;
            isInvulnerable = false;
            NotifyHealthChanged();
        }

        private IEnumerator InvulnerabilityTimer()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityDuration);
            isInvulnerable = false;
            invulnerabilityRoutine = null;
        }

        private void NotifyHealthChanged()
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}
