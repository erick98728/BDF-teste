using Tester.Player;
using UnityEngine;

namespace Tester.UI
{
    /// <summary>
    /// Minimal HUD bridge for prototype UI updates.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Canvas hudCanvas;

        [Header("Player Health")]
        [SerializeField] private PlayerHealth playerHealth;

        private int currentPlayerHealth;
        private int maxPlayerHealth;

        public Canvas HudCanvas => hudCanvas;
        public int CurrentPlayerHealth => currentPlayerHealth;
        public int MaxPlayerHealth => maxPlayerHealth;

        private void OnEnable()
        {
            SubscribeToPlayerHealth();
        }

        private void OnDisable()
        {
            UnsubscribeFromPlayerHealth();
        }

        public void SetPlayerHealth(PlayerHealth newPlayerHealth)
        {
            UnsubscribeFromPlayerHealth();
            playerHealth = newPlayerHealth;

            if (playerHealth == null)
            {
                ReceivePlayerHealth(0, 0);
                return;
            }

            ReceivePlayerHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);

            if (isActiveAndEnabled)
            {
                SubscribeToPlayerHealth();
            }
        }

        private void SubscribeToPlayerHealth()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.HealthChanged -= ReceivePlayerHealth;
            playerHealth.HealthChanged += ReceivePlayerHealth;
            ReceivePlayerHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        private void UnsubscribeFromPlayerHealth()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.HealthChanged -= ReceivePlayerHealth;
        }

        private void ReceivePlayerHealth(int currentHealth, int maximumHealth)
        {
            currentPlayerHealth = currentHealth;
            maxPlayerHealth = maximumHealth;
        }
    }
}
