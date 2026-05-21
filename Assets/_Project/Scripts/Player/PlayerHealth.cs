using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Minimal health container for player survivability systems.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
    }
}
