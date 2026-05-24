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

        [Header("Optional Bounds")]
        [SerializeField] private bool useBounds;
        [SerializeField] private float minX = -10f;
        [SerializeField] private float maxX = 10f;
        [SerializeField] private float minY = -5f;
        [SerializeField] private float maxY = 5f;

        private Vector3 currentVelocity;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 targetPosition = target.position + offset;
            Vector3 nextPosition = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                smoothTime
            );

            transform.position = useBounds ? ClampToBounds(nextPosition) : nextPosition;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void SetBounds(bool enabled, float minimumX, float maximumX, float minimumY, float maximumY)
        {
            useBounds = enabled;
            minX = minimumX;
            maxX = maximumX;
            minY = minimumY;
            maxY = maximumY;
        }

        private Vector3 ClampToBounds(Vector3 position)
        {
            float lowerX = Mathf.Min(minX, maxX);
            float upperX = Mathf.Max(minX, maxX);
            float lowerY = Mathf.Min(minY, maxY);
            float upperY = Mathf.Max(minY, maxY);

            position.x = Mathf.Clamp(position.x, lowerX, upperX);
            position.y = Mathf.Clamp(position.y, lowerY, upperY);
            position.z = target != null ? target.position.z + offset.z : offset.z;

            return position;
        }

        private void OnValidate()
        {
            smoothTime = Mathf.Max(0f, smoothTime);
        }

        private void OnDrawGizmosSelected()
        {
            if (!useBounds)
            {
                return;
            }

            float lowerX = Mathf.Min(minX, maxX);
            float upperX = Mathf.Max(minX, maxX);
            float lowerY = Mathf.Min(minY, maxY);
            float upperY = Mathf.Max(minY, maxY);
            Vector3 center = new Vector3((lowerX + upperX) * 0.5f, (lowerY + upperY) * 0.5f, transform.position.z);
            Vector3 size = new Vector3(upperX - lowerX, upperY - lowerY, 0f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(center, size);
        }
    }
}