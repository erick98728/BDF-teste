using Tester.Player;
using UnityEngine;

namespace Tester.Interactables
{
    /// <summary>
    /// Blocks a passage until Rubens has the required prototype ability.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class AbilityGate : MonoBehaviour
    {
        [Header("Requirement")]
        [SerializeField] private PlayerAbility requiredAbility = PlayerAbility.Dash;
        [Tooltip("Optional reference for opening immediately when the ability unlocks.")]
        [SerializeField] private AbilityManager abilityManager;

        [Header("Gate")]
        [Tooltip("Solid collider that blocks the route while the gate is locked.")]
        [SerializeField] private Collider2D blockingCollider;
        [Tooltip("Optional visual child disabled when the gate opens.")]
        [SerializeField] private GameObject lockedVisual;
        [SerializeField] private bool hideLockedVisualWhenOpen = true;

        [Header("Debug")]
        [Min(0f)]
        [SerializeField] private float lockedMessageCooldown = 0.75f;

        private bool isOpen;
        private float nextLockedMessageTime;

        public bool IsOpen => isOpen;
        public PlayerAbility RequiredAbility => requiredAbility;

        private void Awake()
        {
            blockingCollider ??= GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            SubscribeToAbilityManager();
            RefreshGateState();
        }

        private void OnDisable()
        {
            UnsubscribeFromAbilityManager();
        }

        private void OnValidate()
        {
            lockedMessageCooldown = Mathf.Max(0f, lockedMessageCooldown);
        }

        private void Reset()
        {
            blockingCollider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckPlayerAbilities(collision.collider, true);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CheckPlayerAbilities(collision.collider, false);
        }

        public void SetAbilityManager(AbilityManager newAbilityManager)
        {
            if (abilityManager == newAbilityManager)
            {
                return;
            }

            UnsubscribeFromAbilityManager();
            abilityManager = newAbilityManager;
            SubscribeToAbilityManager();
            RefreshGateState();
        }

        private void CheckPlayerAbilities(Collider2D other, bool logWhenLocked)
        {
            if (isOpen)
            {
                return;
            }

            AbilityManager playerAbilities = other.GetComponentInParent<AbilityManager>();

            if (playerAbilities == null)
            {
                return;
            }

            SetAbilityManager(playerAbilities);

            if (abilityManager.HasAbility(requiredAbility))
            {
                OpenGate();
                return;
            }

            if (logWhenLocked)
            {
                LogLockedGate();
            }
        }

        private void RefreshGateState()
        {
            if (isOpen || abilityManager == null)
            {
                return;
            }

            if (abilityManager.HasAbility(requiredAbility))
            {
                OpenGate();
            }
        }

        private void OpenGate()
        {
            if (isOpen)
            {
                return;
            }

            isOpen = true;

            if (blockingCollider != null)
            {
                blockingCollider.enabled = false;
            }

            if (hideLockedVisualWhenOpen && lockedVisual != null)
            {
                lockedVisual.SetActive(false);
            }

            Debug.Log($"{name} opened. Required ability: {requiredAbility}.", this);
        }

        private void LogLockedGate()
        {
            if (Time.time < nextLockedMessageTime)
            {
                return;
            }

            nextLockedMessageTime = Time.time + lockedMessageCooldown;
            Debug.Log($"{name} is locked. Rubens needs {requiredAbility}.", this);
        }

        private void SubscribeToAbilityManager()
        {
            if (abilityManager == null)
            {
                return;
            }

            abilityManager.AbilitiesChanged -= RefreshGateState;
            abilityManager.AbilitiesChanged += RefreshGateState;
        }

        private void UnsubscribeFromAbilityManager()
        {
            if (abilityManager == null)
            {
                return;
            }

            abilityManager.AbilitiesChanged -= RefreshGateState;
        }
    }
}
