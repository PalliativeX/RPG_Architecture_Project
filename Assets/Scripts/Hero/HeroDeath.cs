using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth health;

        [SerializeField] private HeroMove move;
        [SerializeField] private HeroAttack attack;
        [SerializeField] private HeroAnimator animator;

        [SerializeField] private GameObject deathFx;
        
        private bool isDead;

        private void Start() => 
            health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!isDead && health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            move.enabled = false;
            attack.enabled = false;
            animator.PlayDeath();

            Instantiate(deathFx, transform.position, Quaternion.identity);
        }
        
    }
}