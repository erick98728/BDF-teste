using Tester.Player;
using Tester.UI;
using UnityEngine;

namespace Tester.World
{
    /// <summary>
    /// Trigger used below pits and map bounds to return Rubens to the respawn loop.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class DeathZone : MonoBehaviour
    {
        private Collider2D zoneCollider;

        private void Awake()
        {
            zoneCollider = GetComponent<Collider2D>();

            if (!zoneCollider.isTrigger)
            {
                Debug.LogWarning($"{name} needs a trigger Collider2D to work as a DeathZone.", this);
            }
        }

        private void Reset()
        {
            ConfigureTrigger();
        }

        private void OnValidate()
        {
            ConfigureTrigger();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth playerHealth = ResolvePlayerHealth(other);

            if (playerHealth == null || playerHealth.IsDead)
            {
                return;
            }

            Debug.Log($"{playerHealth.name} entered {name} and will respawn.", this);
            playerHealth.Die();
            HUDController.TryShowMessage("Rubens caiu.");
        }

        private static PlayerHealth ResolvePlayerHealth(Collider2D other)
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                return playerHealth;
            }

            if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player"))
            {
                return null;
            }

            return other.GetComponentInChildren<PlayerHealth>()
                ?? other.transform.root.GetComponentInChildren<PlayerHealth>();
        }

        private void ConfigureTrigger()
        {
            zoneCollider ??= GetComponent<Collider2D>();

            if (zoneCollider != null)
            {
                zoneCollider.isTrigger = true;
            }
        }
    }
}
