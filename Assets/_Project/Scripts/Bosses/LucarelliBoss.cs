using System.Collections;
using Tester.Player;
using UnityEngine;

namespace Tester.Bosses
{
    /// <summary>
    /// First prototype version of Lucarelli with a charge and a close mist strike.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class LucarelliBoss : BossBase
    {
        [Header("Target")]
        [Tooltip("Optional reference. If empty, Lucarelli searches for Rubens in the scene.")]
        [SerializeField] private PlayerHealth playerHealth;

        [Header("Defeat Reward")]
        [Tooltip("Optional reference. If empty, Lucarelli uses the target player's AbilityManager.")]
        [SerializeField] private AbilityManager rewardAbilityManager;

        [Header("Attack Timing")]
        [Min(0f)]
        [SerializeField] private float initialAttackDelay = 1f;
        [Min(0.1f)]
        [SerializeField] private float attackInterval = 1.4f;

        [Header("Charge Attack")]
        [Min(0f)]
        [SerializeField] private float chargeSpeed = 9f;
        [Min(0.05f)]
        [SerializeField] private float chargeDuration = 0.35f;
        [Min(1)]
        [SerializeField] private int chargeDamage = 18;
        [Min(0f)]
        [SerializeField] private float chargeDamageInterval = 0.5f;

        [Header("Close Area Attack")]
        [Tooltip("Optional point for the short area strike. If empty, Lucarelli uses his own position.")]
        [SerializeField] private Transform closeAttackPoint;
        [Min(0.05f)]
        [SerializeField] private float closeAttackRadius = 1.1f;
        [Min(0f)]
        [SerializeField] private float closeAttackWindup = 0.25f;
        [Min(1)]
        [SerializeField] private int closeAttackDamage = 14;

        private Rigidbody2D rb;
        private Coroutine attackRoutine;
        private bool isCharging;
        private bool useChargeNext = true;
        private float facingDirection = 1f;
        private float nextAttackTime;
        private float nextChargeDamageTime;

        public PlayerHealth TargetPlayer => playerHealth;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ResolvePlayerTarget();
            nextAttackTime = Time.time + initialAttackDelay;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            initialAttackDelay = Mathf.Max(0f, initialAttackDelay);
            attackInterval = Mathf.Max(0.1f, attackInterval);
            chargeSpeed = Mathf.Max(0f, chargeSpeed);
            chargeDuration = Mathf.Max(0.05f, chargeDuration);
            chargeDamage = Mathf.Max(1, chargeDamage);
            chargeDamageInterval = Mathf.Max(0f, chargeDamageInterval);
            closeAttackRadius = Mathf.Max(0.05f, closeAttackRadius);
            closeAttackWindup = Mathf.Max(0f, closeAttackWindup);
            closeAttackDamage = Mathf.Max(1, closeAttackDamage);
        }

        private void Update()
        {
            if (IsDead || Time.timeScale <= 0f)
            {
                return;
            }

            if (playerHealth == null)
            {
                ResolvePlayerTarget();
                return;
            }

            if (attackRoutine != null || Time.time < nextAttackTime)
            {
                return;
            }

            FacePlayer();

            attackRoutine = useChargeNext
                ? StartCoroutine(ChargeAttackRoutine())
                : StartCoroutine(CloseAttackRoutine());

            useChargeNext = !useChargeNext;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryDealChargeDamage(collision.collider);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TryDealChargeDamage(collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryDealChargeDamage(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryDealChargeDamage(other);
        }

        public void SetTarget(PlayerHealth newPlayerHealth)
        {
            playerHealth = newPlayerHealth;
        }

        private IEnumerator ChargeAttackRoutine()
        {
            FacePlayer();
            isCharging = true;
            rb.linearVelocity = new Vector2(facingDirection * chargeSpeed, rb.linearVelocity.y);

            Debug.Log("Lucarelli charges forward.", this);

            yield return new WaitForSeconds(chargeDuration);

            StopHorizontalMovement();
            isCharging = false;
            FinishAttack();
        }

        private IEnumerator CloseAttackRoutine()
        {
            FacePlayer();
            StopHorizontalMovement();

            Debug.Log("Lucarelli prepares a close mist strike.", this);

            if (closeAttackWindup > 0f)
            {
                yield return new WaitForSeconds(closeAttackWindup);
            }

            DamagePlayerInCloseArea();
            FinishAttack();
        }

        private void DamagePlayerInCloseArea()
        {
            Vector3 attackPosition = CloseAttackPosition;
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition, closeAttackRadius);

            foreach (Collider2D hit in hits)
            {
                PlayerHealth hitPlayer = hit.GetComponentInParent<PlayerHealth>();

                if (hitPlayer == null)
                {
                    continue;
                }

                hitPlayer.TakeDamage(closeAttackDamage);
                Debug.Log("Lucarelli releases a close mist strike.", this);
                return;
            }

            Debug.Log("Lucarelli's close mist strike missed.", this);
        }

        private void TryDealChargeDamage(Collider2D other)
        {
            if (!isCharging || Time.time < nextChargeDamageTime)
            {
                return;
            }

            PlayerHealth hitPlayer = other.GetComponentInParent<PlayerHealth>();

            if (hitPlayer == null)
            {
                return;
            }

            if (hitPlayer.TakeDamage(chargeDamage))
            {
                nextChargeDamageTime = Time.time + chargeDamageInterval;
                Debug.Log("Lucarelli hits Rubens during the charge.", this);
            }
        }

        private void FinishAttack()
        {
            nextAttackTime = Time.time + attackInterval;
            attackRoutine = null;
        }

        private void ResolvePlayerTarget()
        {
            playerHealth = UnityEngine.Object.FindFirstObjectByType<PlayerHealth>();
        }

        private void FacePlayer()
        {
            if (playerHealth == null)
            {
                return;
            }

            float horizontalDistance = playerHealth.transform.position.x - transform.position.x;

            if (!Mathf.Approximately(horizontalDistance, 0f))
            {
                facingDirection = Mathf.Sign(horizontalDistance);
            }

            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * facingDirection;
            transform.localScale = localScale;
        }

        private void StopHorizontalMovement()
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        protected override void OnDefeated()
        {
            StopHorizontalMovement();
            UnlockDashReward();
            base.OnDefeated();
        }

        private void UnlockDashReward()
        {
            AbilityManager abilityManager = ResolveRewardAbilityManager();

            if (abilityManager == null)
            {
                Debug.LogWarning("Lucarelli was defeated, but no AbilityManager was found for the Dash reward.", this);
                return;
            }

            abilityManager.UnlockDash();
            Debug.Log("Fragmento de memoria recuperado. Dash desbloqueado.", this);
        }

        private AbilityManager ResolveRewardAbilityManager()
        {
            if (rewardAbilityManager != null)
            {
                return rewardAbilityManager;
            }

            if (playerHealth == null)
            {
                ResolvePlayerTarget();
            }

            return playerHealth != null
                ? playerHealth.GetComponent<AbilityManager>()
                : null;
        }

        private Vector3 CloseAttackPosition => closeAttackPoint != null
            ? closeAttackPoint.position
            : transform.position;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(CloseAttackPosition, closeAttackRadius);
        }
    }
}
