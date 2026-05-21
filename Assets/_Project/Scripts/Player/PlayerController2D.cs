using UnityEngine;

namespace Tester.Player
{
    /// <summary>
    /// Minimal player movement scaffold for future 2D controls.
    /// </summary>
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;

        public float MoveSpeed => moveSpeed;
    }
}
