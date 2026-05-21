using UnityEngine;

namespace Tester.Core
{
    /// <summary>
    /// Simple smooth camera follow for 2D prototype scenes.
    /// </summary>
    public class CameraFollow2D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);
        [SerializeField] private float smoothTime = 0.15f;

        private Vector3 currentVelocity;

        public Transform Target => target;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
    }
}
