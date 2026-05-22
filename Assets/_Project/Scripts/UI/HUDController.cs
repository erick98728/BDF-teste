using System.Collections;
using Tester.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tester.UI
{
    /// <summary>
    /// Minimal Canvas HUD for prototype health, abilities, and short event messages.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        private static HUDController activeHud;

        [SerializeField] private Canvas hudCanvas;

        [Header("Player")]
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private AbilityManager abilityManager;

        [Header("Text References")]
        [Tooltip("Text used for Rubens' current and maximum health.")]
        [SerializeField] private Text healthText;
        [Tooltip("Text used for the current Dash lock state.")]
        [SerializeField] private Text dashStateText;
        [Tooltip("Text used for short prototype event messages.")]
        [SerializeField] private Text messageText;

        [Header("Labels")]
        [SerializeField] private string healthLabel = "Vida";
        [SerializeField] private string dashLockedLabel = "Dash: Bloqueado";
        [SerializeField] private string dashUnlockedLabel = "Dash: Desbloqueado";

        [Header("Messages")]
        [Min(0.1f)]
        [SerializeField] private float messageDuration = 2.5f;

        private int currentPlayerHealth;
        private int maxPlayerHealth;
        private Coroutine messageRoutine;
        private bool dashUnlocked;
        private bool hasReceivedHealth;
        private bool hasReceivedDashState;

        public static HUDController Active => activeHud;
        public Canvas HudCanvas => hudCanvas;
        public int CurrentPlayerHealth => currentPlayerHealth;
        public int MaxPlayerHealth => maxPlayerHealth;
        public bool DashUnlocked => dashUnlocked;

        private void Awake()
        {
            hudCanvas ??= GetComponentInParent<Canvas>();
        }

        private void OnEnable()
        {
            activeHud = this;
            ResolvePlayerReferences();
            SubscribeToPlayerHealth();
            SubscribeToAbilityManager();
            HideMessage();
        }

        private void OnDisable()
        {
            UnsubscribeFromPlayerHealth();
            UnsubscribeFromAbilityManager();

            if (messageRoutine != null)
            {
                StopCoroutine(messageRoutine);
                messageRoutine = null;
            }

            HideMessage();

            if (activeHud == this)
            {
                activeHud = null;
            }
        }

        private void OnValidate()
        {
            messageDuration = Mathf.Max(0.1f, messageDuration);
        }

        private void Reset()
        {
            hudCanvas = GetComponentInParent<Canvas>();
        }

        public static bool TryShowMessage(string message)
        {
            if (activeHud == null)
            {
                return false;
            }

            activeHud.ShowMessage(message);
            return true;
        }

        public void SetPlayerHealth(PlayerHealth newPlayerHealth)
        {
            if (playerHealth == newPlayerHealth)
            {
                if (playerHealth != null)
                {
                    UpdateHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
                }

                return;
            }

            UnsubscribeFromPlayerHealth();
            playerHealth = newPlayerHealth;
            hasReceivedHealth = false;

            if (playerHealth == null)
            {
                UpdateHealth(0, 0);
                return;
            }

            UpdateHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);

            if (isActiveAndEnabled)
            {
                SubscribeToPlayerHealth();
            }
        }

        public void SetAbilityManager(AbilityManager newAbilityManager)
        {
            if (abilityManager == newAbilityManager)
            {
                ReceiveAbilitiesChanged();
                return;
            }

            UnsubscribeFromAbilityManager();
            abilityManager = newAbilityManager;
            hasReceivedDashState = false;

            if (isActiveAndEnabled)
            {
                SubscribeToAbilityManager();
                return;
            }

            ReceiveAbilitiesChanged();
        }

        public void UpdateHealth(int currentHealth, int maximumHealth)
        {
            maxPlayerHealth = Mathf.Max(0, maximumHealth);
            currentPlayerHealth = maxPlayerHealth > 0
                ? Mathf.Clamp(currentHealth, 0, maxPlayerHealth)
                : Mathf.Max(0, currentHealth);
            hasReceivedHealth = true;

            if (healthText == null)
            {
                return;
            }

            healthText.text = maxPlayerHealth > 0
                ? $"{healthLabel}: {currentPlayerHealth}/{maxPlayerHealth}"
                : $"{healthLabel}: --/--";
        }

        public void UpdateDashState(bool isDashUnlocked)
        {
            bool dashWasJustUnlocked = hasReceivedDashState && !dashUnlocked && isDashUnlocked;

            dashUnlocked = isDashUnlocked;
            hasReceivedDashState = true;

            if (dashStateText != null)
            {
                dashStateText.text = dashUnlocked ? dashUnlockedLabel : dashLockedLabel;
            }

            if (dashWasJustUnlocked)
            {
                ShowMessage("Dash desbloqueado.");
            }
        }

        public void ShowMessage(string message)
        {
            if (messageRoutine != null)
            {
                StopCoroutine(messageRoutine);
                messageRoutine = null;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                HideMessage();
                return;
            }

            if (messageText == null)
            {
                return;
            }

            messageText.text = message;
            messageText.gameObject.SetActive(true);
            messageRoutine = StartCoroutine(HideMessageAfterDelay());
        }

        private IEnumerator HideMessageAfterDelay()
        {
            yield return new WaitForSecondsRealtime(messageDuration);

            HideMessage();
            messageRoutine = null;
        }

        private void ResolvePlayerReferences()
        {
            if (playerHealth == null)
            {
                playerHealth = UnityEngine.Object.FindFirstObjectByType<PlayerHealth>();
            }

            if (abilityManager == null && playerHealth != null)
            {
                abilityManager = playerHealth.GetComponent<AbilityManager>();
            }

            if (abilityManager == null)
            {
                abilityManager = UnityEngine.Object.FindFirstObjectByType<AbilityManager>();
            }
        }

        private void SubscribeToPlayerHealth()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.HealthChanged -= ReceivePlayerHealth;
            playerHealth.Died -= ReceivePlayerDeath;
            playerHealth.HealthChanged += ReceivePlayerHealth;
            playerHealth.Died += ReceivePlayerDeath;
            UpdateHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        private void UnsubscribeFromPlayerHealth()
        {
            if (playerHealth == null)
            {
                return;
            }

            playerHealth.HealthChanged -= ReceivePlayerHealth;
            playerHealth.Died -= ReceivePlayerDeath;
        }

        private void ReceivePlayerHealth(int currentHealth, int maximumHealth)
        {
            bool lostHealth = hasReceivedHealth && currentHealth < currentPlayerHealth;

            UpdateHealth(currentHealth, maximumHealth);

            if (lostHealth)
            {
                ShowMessage("Rubens recebeu dano.");
            }
        }

        private void ReceivePlayerDeath()
        {
            ShowMessage("Rubens morreu.");
        }

        private void SubscribeToAbilityManager()
        {
            if (abilityManager == null)
            {
                UpdateDashState(false);
                return;
            }

            abilityManager.AbilitiesChanged -= ReceiveAbilitiesChanged;
            abilityManager.AbilitiesChanged += ReceiveAbilitiesChanged;
            ReceiveAbilitiesChanged();
        }

        private void UnsubscribeFromAbilityManager()
        {
            if (abilityManager == null)
            {
                return;
            }

            abilityManager.AbilitiesChanged -= ReceiveAbilitiesChanged;
        }

        private void ReceiveAbilitiesChanged()
        {
            UpdateDashState(abilityManager != null && abilityManager.DashUnlocked);
        }

        private void HideMessage()
        {
            if (messageText == null)
            {
                return;
            }

            messageText.text = string.Empty;
            messageText.gameObject.SetActive(false);
        }
    }
}
