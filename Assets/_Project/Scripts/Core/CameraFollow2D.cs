using UnityEngine;

namespace Tester.Core
{
    [DisallowMultipleComponent]
    public class CameraFollow2D : MonoBehaviour
    {
        [Header("Target")]
        [Tooltip("Transform followed by the camera. Assign Rubens_Player here.")]
        [SerializeField] private Transform target;

        [Header("Follow Settings")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);
        [SerializeField] private float smoothTime = 0.15f;

        private Vector3 currentVelocity;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                smoothTime
            );
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}
