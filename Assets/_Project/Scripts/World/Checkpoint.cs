using Tester.Core;
using Tester.Player;
using UnityEngine;

namespace Tester.World
{
    /// <summary>
    /// Trigger checkpoint that stores Rubens' prototype respawn position.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class Checkpoint : MonoBehaviour
    {
        [Header("Respawn")]
        [Tooltip("Optional point used for respawn. If empty, this checkpoint transform is used.")]
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private bool isActive;

        public bool IsActive => isActive;

        private Vector3 RespawnPosition => respawnPoint != null
            ? respawnPoint.position
            : transform.position;

        private void Reset()
        {
            Collider2D checkpointCollider = GetComponent<Collider2D>();
            checkpointCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponentInParent<PlayerHealth>() == null)
            {
                return;
            }

            Activate();
        }

        public void Activate()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("Checkpoint needs a GameManager to store respawn position.", this);
                return;
            }

            isActive = true;
            GameManager.Instance.SetCheckpoint(RespawnPosition);

            Debug.Log($"Checkpoint activated at {RespawnPosition}.", this);
        }
    }
}
