using System.Collections;
using Tester.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tester.UI
{
    /// <summary>
    /// Simple prototype HUD for player health, dash status and temporary messages.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private AbilityManager abilityManager;

        [Header("UI Texts")]
        [SerializeField] private Text healthText;
        [SerializeField] private Text dashStatusText;
        [SerializeField] private Text eventMessageText;
        [SerializeField] private Text essenceText;

        [Header("Message")]
        [SerializeField] private float messageDuration = 2.5f;

        [Header("Optional Resource")]
        [SerializeField] private int essenceValue;

        private Coroutine messageRoutine;

        private void Awake()
        {
            if (playerHealth == null)
            {
                playerHealth = FindFirstObjectByType<PlayerHealth>();
            }

            if (abilityManager == null)
            {
                abilityManager = FindFirstObjectByType<AbilityManager>();
            }
        }

        private void OnEnable()
        {
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged += HandleHealthChanged;
            }

            if (abilityManager != null)
            {
                abilityManager.OnAbilityUnlocked += HandleAbilityUnlocked;
            }
        }

        private void Start()
        {
            if (playerHealth != null)
            {
                HandleHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
            }

            RefreshDashStatus();
            RefreshEssence();

            if (eventMessageText != null)
            {
                eventMessageText.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged -= HandleHealthChanged;
            }

            if (abilityManager != null)
            {
                abilityManager.OnAbilityUnlocked -= HandleAbilityUnlocked;
            }
        }

        public void ShowMessage(string message)
        {
            Debug.Log($"HUDController: {message}", this);

            if (eventMessageText == null)
            {
                return;
            }

            if (messageRoutine != null)
            {
                StopCoroutine(messageRoutine);
            }

            messageRoutine = StartCoroutine(ShowTemporaryMessageRoutine(message));
        }

        private void HandleHealthChanged(int current, int max)
        {
            string formatted = $"Vida: {current}/{max}";

            if (healthText != null)
            {
                healthText.text = formatted;
            }

            Debug.Log($"HUDController: {formatted}", this);
        }

        private void HandleAbilityUnlocked(PlayerAbility ability)
        {
            if (ability == PlayerAbility.Dash)
            {
                RefreshDashStatus();
                ShowMessage("Dash desbloqueado!");
            }
        }

        private void RefreshDashStatus()
        {
            bool isDashUnlocked = abilityManager != null && abilityManager.HasAbility(PlayerAbility.Dash);
            string text = isDashUnlocked ? "Dash: DESBLOQUEADO" : "Dash: BLOQUEADO";

            if (dashStatusText != null)
            {
                dashStatusText.text = text;
            }

            Debug.Log($"HUDController: {text}", this);
        }

        private void RefreshEssence()
        {
            if (essenceText == null)
            {
                return;
            }

            essenceText.text = $"Essência da Névoa: {essenceValue}";
        }

        private IEnumerator ShowTemporaryMessageRoutine(string message)
        {
            eventMessageText.gameObject.SetActive(true);
            eventMessageText.text = message;

            yield return new WaitForSeconds(messageDuration);

            eventMessageText.gameObject.SetActive(false);
            messageRoutine = null;
        }
    }
}
