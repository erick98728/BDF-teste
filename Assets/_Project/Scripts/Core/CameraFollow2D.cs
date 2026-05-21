using UnityEngine;

namespace Tester.Core
{
    /// <summary>
    /// Minimal camera follow placeholder for 2D prototype scenes.
    /// </summary>
    public class CameraFollow2D : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public Transform Target => target;
    }
}
