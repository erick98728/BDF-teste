using UnityEngine;

namespace Tester.Combat
{
    /// <summary>
    /// Minimal combat scaffold for future katana attack logic.
    /// </summary>
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Combat")]
        [SerializeField] private int attackDamage = 10;

        public int AttackDamage => attackDamage;
    }
}
