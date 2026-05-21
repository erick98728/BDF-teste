using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Tracks unlockable player abilities for prototype progression.
    /// </summary>
    public class AbilityManager : MonoBehaviour
    {
        [Header("Abilities")]
        [SerializeField] private bool dashUnlocked;

        public bool DashUnlocked => dashUnlocked;
    }
}
