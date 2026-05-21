using Tester.Player;
using UnityEngine;

namespace Tester.Interactables
{
    /// <summary>
    /// Blocks player progression until a required ability is unlocked.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AbilityGate : MonoBehaviour
    {
        [Header("Gate")]
        [SerializeField] private PlayerAbility requiredAbility = PlayerAbility.Dash;
        [SerializeField] private bool disableVisualWhenUnlocked = true;

        private Collider2D[] gateColliders;
        private Renderer[] gateRenderers;
        private bool isUnlocked;

        private void Awake()
        {
            gateColliders = GetComponents<Collider2D>();
            gateRenderers = GetComponentsInChildren<Renderer>(true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isUnlocked || !collision.collider.TryGetComponent(out PlayerHealth playerHealth))
            {
                return;
            }

            TryUnlockFromAbilityManager(playerHealth.GetComponent<AbilityManager>());

            if (!isUnlocked)
            {
                Debug.Log($"AbilityGate: passagem bloqueada. Requer habilidade {requiredAbility}.", this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isUnlocked || !other.TryGetComponent(out PlayerHealth playerHealth))
            {
                return;
            }

            TryUnlockFromAbilityManager(playerHealth.GetComponent<AbilityManager>());

            if (!isUnlocked)
            {
                Debug.Log($"AbilityGate: passagem bloqueada. Requer habilidade {requiredAbility}.", this);
            }
        }

        public void TryUnlockFromAbilityManager(AbilityManager abilityManager)
        {
            if (isUnlocked || abilityManager == null)
            {
                return;
            }

            if (!abilityManager.HasAbility(requiredAbility))
            {
                return;
            }

            UnlockGate();
        }

        private void UnlockGate()
        {
            isUnlocked = true;

            for (int i = 0; i < gateColliders.Length; i++)
            {
                gateColliders[i].enabled = false;
            }

            if (disableVisualWhenUnlocked)
            {
                for (int i = 0; i < gateRenderers.Length; i++)
                {
                    gateRenderers[i].enabled = false;
                }
            }

            Debug.Log($"AbilityGate: passagem liberada pela habilidade {requiredAbility}.", this);
        }
    }
}
