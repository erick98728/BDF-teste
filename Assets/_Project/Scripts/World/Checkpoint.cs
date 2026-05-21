using UnityEngine;

namespace Tester.World
{
    /// <summary>
    /// Marks a checkpoint location for future respawn systems.
    /// </summary>
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private bool isActive;

        public bool IsActive => isActive;
    }
}
