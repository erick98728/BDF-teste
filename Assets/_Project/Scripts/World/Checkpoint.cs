using Tester.Core;
using Tester.Player;
using UnityEngine;

namespace Tester.World
{
    /// <summary>
    /// Trigger-based checkpoint that stores respawn position in GameManager.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private bool isActive;

        public bool IsActive => isActive;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out PlayerHealth playerHealth))
            {
                return;
            }

            ActivateCheckpoint(playerHealth.transform.position);
        }

        private void ActivateCheckpoint(Vector3 playerPosition)
        {
            isActive = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterCheckpoint(playerPosition);
            }

            Debug.Log($"Checkpoint: ativado em {transform.position}.", this);
        }
    }
}
