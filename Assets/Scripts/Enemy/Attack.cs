using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float cleavage = 0.5f;
        [SerializeField] private float effectiveDistance = 0.5f;

        private static readonly Collider[] Hits = new Collider[1];

        private Transform _heroTransform;

        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;

        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        public float Cleavage
        {
            get => cleavage;
            set => cleavage = value;
        }

        public float EffectiveDistance
        {
            get => effectiveDistance;
            set => effectiveDistance = value;
        }

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(GetStartPoint(), cleavage, 1f);
                hit.transform.GetComponent<IHealth>().TakeDamage(damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), cleavage, Hits, _layerMask);
            
            hit = Hits[0];
            
            return hitCount > 0;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp() => 
            _currentAttackCooldown <= 0f;

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + 
                   transform.forward * effectiveDistance;
        }

        private bool CanAttack() => 
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            animator.PlayAttack();

            _isAttacking = true;
        }
        
    }
}