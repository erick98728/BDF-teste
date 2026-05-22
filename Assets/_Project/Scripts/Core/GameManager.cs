using System.Collections;
using Tester.Player;
using UnityEngine;

namespace Tester.Core
{
    /// <summary>
    /// Entry-level game coordinator for prototype bootstrap.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Respawn")]
        [Min(0f)]
        [SerializeField] private float respawnDelay = 0.15f;

        private Vector3 respawnPosition;
        private bool hasRespawnPosition;
        private bool isRespawning;

        public Vector3 RespawnPosition => respawnPosition;
        public bool HasRespawnPosition => hasRespawnPosition;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterPlayer(PlayerHealth playerHealth)
        {
            if (playerHealth == null || hasRespawnPosition)
            {
                return;
            }

            respawnPosition = playerHealth.transform.position;
            hasRespawnPosition = true;

            Debug.Log($"Initial respawn position registered at {respawnPosition}.", playerHealth);
        }

        public void SetCheckpoint(Vector3 checkpointPosition)
        {
            respawnPosition = checkpointPosition;
            hasRespawnPosition = true;

            Debug.Log($"Checkpoint respawn position set to {respawnPosition}.", this);
        }

        public void RespawnPlayer(PlayerHealth playerHealth)
        {
            if (playerHealth == null || isRespawning)
            {
                return;
            }

            if (!hasRespawnPosition)
            {
                RegisterPlayer(playerHealth);
            }

            StartCoroutine(RespawnAfterDelay(playerHealth));
        }

        private IEnumerator RespawnAfterDelay(PlayerHealth playerHealth)
        {
            isRespawning = true;

            if (respawnDelay > 0f)
            {
                yield return new WaitForSecondsRealtime(respawnDelay);
            }

            if (playerHealth == null)
            {
                isRespawning = false;
                yield break;
            }

            MovePlayerToRespawn(playerHealth);
            playerHealth.ResetHealth();

            Debug.Log($"Rubens respawned at {respawnPosition}.", playerHealth);
            isRespawning = false;
        }

        private void MovePlayerToRespawn(PlayerHealth playerHealth)
        {
            Rigidbody2D playerBody = playerHealth.GetComponent<Rigidbody2D>();

            if (playerBody == null)
            {
                playerHealth.transform.position = respawnPosition;
                return;
            }

            playerBody.linearVelocity = Vector2.zero;
            playerBody.angularVelocity = 0f;
            playerBody.position = respawnPosition;
        }
    }
}
