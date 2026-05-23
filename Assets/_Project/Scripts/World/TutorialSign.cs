using Tester.Player;
using Tester.UI;
using UnityEngine;

namespace Tester.World
{
    /// <summary>
    /// Simple world trigger that shows a short prototype tutorial message.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class TutorialSign : MonoBehaviour
    {
        [Header("Message")]
        [TextArea]
        [SerializeField] private string message;
        [SerializeField] private bool showOnce = true;
        [Min(0.1f)]
        [SerializeField] private float messageDuration = 3f;

        private Collider2D signCollider;
        private bool hasShown;

        public string Message => message;
        public bool ShowOnce => showOnce;
        public float MessageDuration => messageDuration;
        public bool HasShown => hasShown;

        private void Awake()
        {
            signCollider = GetComponent<Collider2D>();

            if (!signCollider.isTrigger)
            {
                Debug.LogWarning($"{name} needs a trigger Collider2D to work as a TutorialSign.", this);
            }
        }

        private void Reset()
        {
            ConfigureTrigger();
        }

        private void OnValidate()
        {
            messageDuration = Mathf.Max(0.1f, messageDuration);
            ConfigureTrigger();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (showOnce && hasShown)
            {
                return;
            }

            if (!IsPlayer(other))
            {
                return;
            }

            Show();
        }

        public void Show()
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            hasShown = true;

            if (!HUDController.TryShowMessage(message, messageDuration))
            {
                Debug.Log(message, this);
            }
        }

        public void Configure(string signMessage, bool shouldShowOnce, float duration)
        {
            message = signMessage;
            showOnce = shouldShowOnce;
            messageDuration = Mathf.Max(0.1f, duration);
        }

        public void ResetShownState()
        {
            hasShown = false;
        }

        private static bool IsPlayer(Collider2D other)
        {
            if (other.GetComponentInParent<PlayerHealth>() != null)
            {
                return true;
            }

            return other.CompareTag("Player") || other.transform.root.CompareTag("Player");
        }

        private void ConfigureTrigger()
        {
            signCollider ??= GetComponent<Collider2D>();

            if (signCollider != null)
            {
                signCollider.isTrigger = true;
            }
        }
    }
}
