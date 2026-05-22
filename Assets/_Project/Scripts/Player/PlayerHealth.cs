using System;
using System.Collections;
using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Handles Rubens' prototype health, hit protection, and death notification.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health")]
        [Min(1)]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;

        [Header("Damage")]
        [Min(0f)]
        [SerializeField] private float invulnerabilityDuration = 0.75f;

        private Coroutine invulnerabilityRoutine;
        private bool isInvulnerable;
        private bool isDead;

        public event Action<int, int> HealthChanged;
        public event Action Died;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsInvulnerable => isInvulnerable;
        public bool IsDead => isDead;

        private void Awake()
        {
            ClampHealthValues();
            isDead = currentHealth <= 0;
        }

        private void OnValidate()
        {
            ClampHealthValues();
        }

        public bool TakeDamage(int amount)
        {
            if (amount <= 0 || isDead || isInvulnerable)
            {
                return false;
            }

            currentHealth = Mathf.Max(0, currentHealth - amount);
            HealthChanged?.Invoke(currentHealth, maxHealth);

            Debug.Log($"{name} took {amount} damage. Health: {currentHealth}/{maxHealth}.", this);

            if (currentHealth <= 0)
            {
                Die();
                return true;
            }

            StartInvulnerability();
            return true;
        }

        public bool Heal(int amount)
        {
            if (amount <= 0 || isDead || currentHealth >= maxHealth)
            {
                return false;
            }

            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            HealthChanged?.Invoke(currentHealth, maxHealth);

            Debug.Log($"{name} healed {amount}. Health: {currentHealth}/{maxHealth}.", this);
            return true;
        }

        public void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            StopInvulnerability();

            if (currentHealth > 0)
            {
                currentHealth = 0;
                HealthChanged?.Invoke(currentHealth, maxHealth);
            }

            Debug.Log($"{name} died. Checkpoint respawn can listen to PlayerHealth.Died.", this);
            Died?.Invoke();
        }

        public void ResetHealth()
        {
            isDead = false;
            StopInvulnerability();
            currentHealth = maxHealth;
            HealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void StartInvulnerability()
        {
            StopInvulnerability();

            // Set before the coroutine starts so same-frame hits are rejected.
            isInvulnerable = true;
            invulnerabilityRoutine = StartCoroutine(ClearInvulnerabilityAfterDelay());
        }

        private IEnumerator ClearInvulnerabilityAfterDelay()
        {
            yield return new WaitForSeconds(invulnerabilityDuration);

            isInvulnerable = false;
            invulnerabilityRoutine = null;
        }

        private void StopInvulnerability()
        {
            if (invulnerabilityRoutine != null)
            {
                StopCoroutine(invulnerabilityRoutine);
                invulnerabilityRoutine = null;
            }

            isInvulnerable = false;
        }

        private void ClampHealthValues()
        {
            maxHealth = Mathf.Max(1, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        [ContextMenu("Debug/Take 10 Damage")]
        private void DebugTakeDamage()
        {
            TakeDamage(10);
        }

        [ContextMenu("Debug/Heal 10")]
        private void DebugHeal()
        {
            Heal(10);
        }
    }
}
