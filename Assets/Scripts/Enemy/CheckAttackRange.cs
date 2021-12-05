using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack attack;
        [SerializeField] private TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEntered += OnTriggerEntered;
            triggerObserver.TriggerExited += OnTriggerExited;

            attack.DisableAttack();
        }

        private void OnTriggerEntered(Collider obj)
        {
            attack.EnableAttack();
        }

        private void OnTriggerExited(Collider obj)
        {
            attack.DisableAttack();
        }
    }
}