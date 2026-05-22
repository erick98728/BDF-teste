using System;
using UnityEngine;

namespace Tester.Player
{
    public enum PlayerAbility
    {
        Dash
    }

    /// <summary>
    /// Tracks unlockable player abilities for prototype progression.
    /// </summary>
    public class AbilityManager : MonoBehaviour
    {
        [Header("Prototype Abilities")]
        [Tooltip("Keep disabled until a prototype reward or debug action unlocks the Dash.")]
        [SerializeField] private bool dashUnlocked;

        public event Action AbilitiesChanged;

        public bool DashUnlocked => dashUnlocked;

        public bool HasAbility(PlayerAbility ability)
        {
            return ability == PlayerAbility.Dash && dashUnlocked;
        }

        [ContextMenu("Debug/Unlock Dash")]
        public void UnlockDash()
        {
            if (dashUnlocked)
            {
                return;
            }

            dashUnlocked = true;
            Debug.Log("Dash unlocked.", this);
            AbilitiesChanged?.Invoke();
        }

        [ContextMenu("Debug/Lock Dash")]
        public void LockDash()
        {
            if (!dashUnlocked)
            {
                return;
            }

            dashUnlocked = false;
            Debug.Log("Dash locked.", this);
            AbilitiesChanged?.Invoke();
        }
    }
}
