using Tester.Player;
using UnityEngine;

namespace Tester.UI
{
    /// <summary>
    /// Minimal HUD bridge that listens to player health updates.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;

        private void OnEnable()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.OnHealthChanged += HandleHealthChanged;
        }

        private void Start()
        {
            if (playerHealth == null)
            {
                return;
            }

            HandleHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        private void OnDisable()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.OnHealthChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int current, int max)
        {
            Debug.Log($"HUDController: Vida do Rubens = {current}/{max}", this);
            // Futuro: atualizar barra/texto de vida.
        }
    }
}
