using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimator animator;

        [SerializeField] private GameObject deathVfx;

        public event Action Happened;

        private void Start() => 
            health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (health.Current <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            health.HealthChanged -= OnHealthChanged;
            
            animator.PlayDeath();

            SpawnDeathFx();
            
            Happened?.Invoke();
            
            Destroy(gameObject, 3f);
        }

        private void SpawnDeathFx() => 
            Instantiate(deathVfx, transform.position, Quaternion.identity);
    }
}