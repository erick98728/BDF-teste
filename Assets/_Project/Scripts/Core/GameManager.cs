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

        public void HandlePlayerDeath(PlayerHealth playerHealth)
        {
            Debug.Log("GameManager: morte do jogador detectada. Futuro ponto de integração com checkpoint/respawn.", playerHealth);
        }
    }
}
