using System.Collections;
using Tester.Interactables;
using Tester.Player;
using Tester.UI;
using UnityEngine;

namespace Tester.Core
{
    /// <summary>
    /// Coordinates checkpoint registration, player respawn and boss reward flow for the prototype.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Respawn")]
        [SerializeField] private float respawnDelay = 0.35f;
        [SerializeField] private float respawnInvulnerabilityDuration = 1f;

        [Header("Boss Progression")]
        [SerializeField] private string dashUnlockedMessage = "Memória recuperada! Dash desbloqueado.";

        public static GameManager Instance { get; private set; }

        public bool LucarelliDefeated { get; private set; }

        private Vector3 lastCheckpointPosition;
        private bool hasCheckpoint;
        private bool isRespawning;

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

        public void RegisterCheckpoint(Vector3 checkpointPosition)
        {
            lastCheckpointPosition = checkpointPosition;
            hasCheckpoint = true;
            Debug.Log($"GameManager: checkpoint registrado em {checkpointPosition}.", this);
            NotifyHudMessage("Checkpoint ativado!");
        }

        public void NotifyHudMessage(string message)
        {
            HUDController[] hudControllers = FindObjectsByType<HUDController>(FindObjectsSortMode.None);
            for (int i = 0; i < hudControllers.Length; i++)
            {
                hudControllers[i].ShowMessage(message);
            }
        }

        public void HandlePlayerDeath(PlayerHealth playerHealth)
        {
            if (playerHealth == null || isRespawning)
            {
                return;
            }

            StartCoroutine(RespawnRoutine(playerHealth));
        }

        public void HandleLucarelliDefeat(AbilityManager abilityManager)
        {
            if (LucarelliDefeated)
            {
                return;
            }

            LucarelliDefeated = true;
            Debug.Log("GameManager: Lucarelli marcado como derrotado.", this);

            if (abilityManager != null)
            {
                abilityManager.UnlockDash();
            }

            Debug.Log($"GameManager: {dashUnlockedMessage}", this);
            NotifyHudMessage(dashUnlockedMessage);

            AbilityGate[] abilityGates = FindObjectsByType<AbilityGate>(FindObjectsSortMode.None);
            for (int i = 0; i < abilityGates.Length; i++)
            {
                abilityGates[i].TryUnlockFromAbilityManager(abilityManager);
            }
        }

        private IEnumerator RespawnRoutine(PlayerHealth playerHealth)
        {
            isRespawning = true;
            yield return new WaitForSeconds(respawnDelay);

            Vector3 respawnPosition = hasCheckpoint ? lastCheckpointPosition : playerHealth.transform.position;
            playerHealth.transform.position = respawnPosition;
            playerHealth.RestoreFullHealth();
            playerHealth.EnableTemporaryInvulnerability(respawnInvulnerabilityDuration);

            Debug.Log($"GameManager: Rubens respawnou em {respawnPosition}.", playerHealth);

            isRespawning = false;
        }
    }
}
