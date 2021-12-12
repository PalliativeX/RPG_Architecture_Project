using System;
using Logic;
using UI;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float current;
        [SerializeField] private float max;

        public float Current
        {
            get => current;
            set => current = value;
        }

        public float Max
        {
            get => max;
            set => max = value;
        }

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            Current -= damage;
            animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
        
    }
}