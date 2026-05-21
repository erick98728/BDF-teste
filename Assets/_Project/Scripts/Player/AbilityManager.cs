using System;
using UnityEngine;

namespace Tester.Player
{
    public enum PlayerAbility
    {
        Dash = 0
    }

    /// <summary>
    /// Tracks unlockable player abilities for prototype progression.
    /// </summary>
    public class AbilityManager : MonoBehaviour
    {
        [Header("Abilities")]
        [SerializeField] private bool dashUnlocked;

        [Header("Debug (Prototype)")]
        [SerializeField] private bool enableDebugUnlockKey;
        [SerializeField] private KeyCode debugUnlockDashKey = KeyCode.F6;

        public event Action<PlayerAbility> OnAbilityUnlocked;

        public bool DashUnlocked => dashUnlocked;

        private void Update()
        {
            if (!enableDebugUnlockKey || dashUnlocked)
            {
                return;
            }

            if (Input.GetKeyDown(debugUnlockDashKey))
            {
                UnlockDash();
            }
        }

        public bool HasAbility(PlayerAbility ability)
        {
            return ability switch
            {
                PlayerAbility.Dash => dashUnlocked,
                _ => false
            };
        }

        public void UnlockDash()
        {
            if (dashUnlocked)
            {
                return;
            }

            dashUnlocked = true;
            Debug.Log("AbilityManager: Dash desbloqueado.", this);
            OnAbilityUnlocked?.Invoke(PlayerAbility.Dash);
        }
    }
}
