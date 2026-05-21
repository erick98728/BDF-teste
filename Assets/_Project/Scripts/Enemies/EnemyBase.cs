using UnityEngine;

namespace Tester.Enemies
{
    /// <summary>
    /// Base stats scaffold for prototype enemies.
    /// </summary>
    public class EnemyBase : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private int maxHealth = 30;

        public int MaxHealth => maxHealth;
    }
}
